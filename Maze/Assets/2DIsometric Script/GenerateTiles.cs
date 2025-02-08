//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------
namespace System.Activities.Debugger
{
    using System;
    using System.Activities.Expressions;
    using System.Activities.Hosting;
    using System.Activities.Runtime;
    using System.Activities.Statements;
    using System.Activities.XamlIntegration;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime;

    [DebuggerNonUserCode]
    class DebugManager
    {
        static StateManager.DynamicModuleManager dynamicModuleManager;
        WorkflowInstance host;
        StateManager stateManager;
        Dictionary<object, State> states;
        Dictionary<int, Stack<Activity>> runningThreads;
        InstrumentationTracker instrumentationTracker;
        List<string> temporaryFiles;

        public DebugManager(Activity root, string moduleNamePrefix, string typeNamePrefix, string auxiliaryThreadName, bool breakOnStartup,
            WorkflowInstance host, bool debugStartedAtRoot) :
            this(root, moduleNamePrefix, typeNamePrefix, auxiliaryThreadName, breakOnStartup, host, debugStartedAtRoot, false)
        {
        }

        internal DebugManager(Activity root, string moduleNamePrefix, string typeNamePrefix, string auxiliaryThreadName, bool breakOnStartup, 
            WorkflowInstance host, bool debugStartedAtRoot, bool resetDynamicModule)
        {
            if (resetDynamicModule)
            {
                dynamicModuleManager = null;
            }

            if (dynamicModuleManager == null)
            {
                dynamicModuleManager = new StateManager.DynamicModuleManager(moduleNamePrefix);
            }

            this.stateManager = new StateManager(
                new StateManager.Properties
                    {
                        ModuleNamePrefix = moduleNamePrefix,
                        TypeNamePrefix = typeNamePrefix,
                        AuxiliaryThreadName = auxiliaryThreadName,
                        BreakOnStartup = breakOnStartup
                    },
                    debugStartedAtRoot, dynamicModuleManager);
            
            this.states = new Dictionary<object, State>();
            this.runningThreads = new Dictionary<int, Stack<Activity>>();
            this.instrumentationTracker = new InstrumentationTracker(root);
            this.host = host;
        }

        // Whether we're priming the background thread (in Attach To Process case).
        public bool IsPriming
        {
            set { this.stateManager.IsPriming = value; }
        }

        // Whether debugging is done from the start of the root workflow,
        // contrast to attaching into the middle of a running workflow.
        bool DebugStartedAtRoot
        {
            get
            {
                return this.stateManager.DebugStartedAtRoot;
            }
        }

        internal void Instrument(Activity activity)
        {
            bool isTemporaryFile = false;
            string sourcePath = null;
            bool instrumentationFailed = false;
            Dictionary<string, byte[]> checksumCache = null;
            try
            {
                byte[] checksum;
                Dictionary<object, SourceLocation> sourceLocations = SourceLocationProvider.GetSourceLocations(activity, out sourcePath, out isTemporaryFile, out checksum);
                if (checksum != null)
                {
                    checksumCache = new Dictionary<string, byte[]>();
                    checksumCache.Add(sourcePath.ToUpperInvariant(), checksum);
                }
                Instrument(activity, sourceLocations, Path.GetFileNameWithoutExtension(sourcePath), checksumCache);
            }
            catch (Exception ex)
            {
                instrumentationFailed = true;
                Trace.WriteLine(SR.DebugInstrumentationFailed(ex.Message));

                if (Fx.IsFatal(ex))
                {
                    throw;
                }
            }

            List<Activity> sameSourceActivities = this.instrumentationTracker.GetSameSourceSubRoots(activity);
            this.instrumentationTracker.MarkInstrumented(activity);

            foreach (Activity sameSourceActivity in sameSourceActivities)
            {
                if (!instrumentationFailed)
                {
                    MapInstrumentationStates(activity, sameSourceActivity);
                }
                // Mark it as instrumentated, even though it fails so it won't be
                // retried.
                this.instrumentationTracker.MarkInstrumented(sameSourceActivity);
            }

            if (isTemporaryFile)
            {
                if (this.temporaryFiles == null)
                {
                    this.temporaryFiles = new List<string>();
                }
                Fx.Assert(!string.IsNullOrEmpty(sourcePath), "SourcePath cannot be null for temporary file");
                this.temporaryFiles.Add(sourcePath);
            }
        }

        // Workflow rooted at rootActivity1 and rootActivity2 have same source file, but they
        // are two different instantiation.
        // rootActivity1 has been instrumented and its instrumentation states can be
        // re-used by rootActivity2.
        //
        // MapInstrumentationStates will walk both Workflow trees in parallel and map every 
        // state for activities in rootActivity1 to corresponding activities in rootActivity2.
        void MapInstrumentationStates(Activity rootActivity1, Activity rootActivity2)
        {
            Queue<KeyValuePair<Activity, Activity>> pairsRemaining = new Queue<KeyValuePair<Activity, Activity>>();

            pairsRemaining.Enqueue(new KeyValuePair<Activity, Activity>(rootActivity1, rootActivity2));
            HashSet<Activity> visited = new HashSet<Activity>();
            KeyValuePair<Activity, Activity> cur