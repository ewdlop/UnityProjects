aving from an unknown state");

            if (threadId >= 0) 
            {
                State activityState;
                if (this.states.TryGetValue(activity, out activityState))
                {
                    this.stateManager.LeaveState(threadId, activityState);
                }
                else
                {
                    Fx.Assert(false, "Uninstrumented activity is disallowed: " + activity.DisplayName);
                }
                this.Pop(threadId);
            }
        }

        public void OnLeaveState(ActivityInstance activityInstance)
        {
            Fx.Assert(activityInstance != null, "ActivityInstance cannot be null");
            if (this.EnsureInstrumented(activityInstance.Activity))
            {
                this.LeaveState(activityInstance.Activity);
            }
        }

        static Dictionary<string, object> GenerateLocals(ActivityInstance instance)
        {
            Dictionary<string, object> locals = new Dictionary<string, object>();
            locals.Add("debugInfo", new DebugInfo(instance));
            return locals;
        }

        void Push(int threadId, Activity activity)
        {
            ((Stack<Activity>)this.runningThreads[threadId]).Push(activity);
        }

        void Pop(int threadId)
        {
            Stack<Activity> stack = this.runningThreads[threadId];
            stack.Pop();
            if (stack.Count == 0)
            {
                this.stateManager.Exit(threadId);
                this.runningThreads.Remove(threadId);
            }
        }

        // Given an activity, return the thread id where it is currently
        // executed (on the top of the callstack).
        // Boolean "strict" parameter determine whether the activity itself should
        // be on top of the stack.
        // Strict checking is needed in the case of "Leave"-ing a state.
        // Non-strict checking is needed for "Enter"-ing a state, since the direct parent of 
        // the activity may not yet be executed (e.g. the activity is an argument of another activity,
        // the activity is "enter"-ed even though the direct parent is not yet "enter"-ed.
        int GetExecutingThreadId(Activity activity, bool strict)
        {
            int threadId = -1;

            foreach (KeyValuePair<int, Stack<Activity>> entry in this.runningThreads)
            {
                Stack<Activity> threadStack = entry.Value;
                if (threadStack.Peek() == activity)
                {
                    threadId = entry.Key;
                    break;
                }
            }

            if (threadId < 0 && !strict)
            {
                foreach (KeyValuePair<int, Stack<Activity>> entry in this.runningThreads)
                {
                    Stack<Activity> threadStack = entry.Value;
                    Activity topActivity = threadStack.Peek();
                    if (!IsParallelActivity(topActivity) && IsAncestorOf(threadStack.Peek(), activity))
                    {
                        threadId = entry.Key;
                        break;
                    }
                }
            }
            return threadId;
        }

        static bool IsAncestorOf(Activity ancestorActivity, Activity activity)
        {
            Fx.Assert(activity != null, "IsAncestorOf: Cannot pass null as activity");
            Fx.Assert(ancestorActivity != null, "IsAncestorOf: Cannot pass null as ancestorActivity");
            activity = activity.Parent;
            while (activity != null && activity != ancestorActivity && !IsParallelActivity(activity))
            {
                activity = activity.Parent;
            }
            return (activity == ancestorActivity);
        }

        static bool IsParallelActivity(Activity activity)
        {
            Fx.Assert(activity != null, "IsParallel: Cannot pass null as activity");
            return activity is Parallel ||
                    (activity.GetType().IsGenericType && activity.GetType().GetGenericTypeDefinition() == typeof(ParallelForEach<>));
        }

        // Get threads currently executing the parent of the given activity, 
        // if none then create a new one and prep the call stack to current state.
        int GetOrCreateThreadId(Activity activity, ActivityInstance instance)
        {
            int threadId = -1;
            if (activity.Parent != null && !IsParallelActivity(activity.Parent))
            {
                threadId = GetExecutingThreadId(activity.Parent, false);
            }
            if (threadId < 0)
            {
                threadId = CreateLogicalThread(activity, instance, false);
            }
            return threadId;
        }

        // Create logical thread and bring its call stack to reflect call from
        // the root up to (but not including) the instance.
        // If the activity is an expression though, then the call stack will also include the instance
        // (since it is the parent of the expression).
        int CreateLogicalThread(Activity activity, ActivityInstance instance, bool primeCurrentInstance)
        {
            Stack<ActivityInstance> ancestors = null;

            if (!this.DebugStartedAtRoot)
            {
                ancestors = new Stack<ActivityInstance>();

                if (activity != instance.Activity || primeCurrentInstance)
                {   // This mean that activity is an expression and 
                    // instance is the parent of this expression.
                   
                    Fx.Assert(primeCurrentInstance || (activity is ActivityWithResult), "Expect an ActivityWithResult");
                    Fx.Assert(primeCurrentInstance || (activity.Parent == instance.Activity), "Argument Expression is not given correct parent instance");
                    if (primeCurrentInstance || !IsParallelActivity(instance.Activity))
                    {
                        ancestors.Push(instance);
                    }
                }

                ActivityInstance instanceParent = instance.Parent;
                while (instanceParent != null && !IsParallelActivity(instanceParent.Activity))
                {
                    ancestors.Push(instanceParent);
                    instanceParent = instanceParent.Parent;
                }

                if (instanceParent != null && IsParallelActivity(instanceParent.Activity))
                {
                    // Ensure thread is created for the parent (a Parallel activity).
                    int parentThreadId = GetExecutingThreadId(instanceParent.Activity, false);
                    if (parentThreadId < 0)
                    {
                        parentThreadId = CreateLogicalThread(instanceParent.Activity, instanceParent, true);
                        Fx.Assert(parentThreadId > 0, "Parallel main thread can't be created");
                    }
                }
            }

            string threadName = "DebuggerThread:";
            if (activity.Parent != null)
            {
                threadName += activity.Parent.DisplayName;
            }
            else // Special case for the root of WorklowService that does not have a parent.
            {   
                threadName += activity.DisplayName;
            }

            int newThreadId = this.stateManager.CreateLogicalThread(threadName);
            Stack<Activity> newStack = new Stack<Activity>();
            this.runningThreads.Add(newThreadId, newStack);

            if (!this.DebugStartedAtRoot && ancestors != null)
            { // Need to create callstack to current activity.                        
                PrimeCallStack(newThreadId, ancestors);
            }
            
            return newThreadId;
        }

        // Prime the call stack to contains all the ancestors of this instance.
        // Note: the call stack will not include the current instance.
        void PrimeCallStack(int threadId, Stack<ActivityInstance> ancestors)
        {
            Fx.Assert(!this.DebugStartedAtRoot, "Priming should not be called if the debugging is attached from the start of the workflow");
            bool currentIsPrimingValue = this.stateManager.IsPriming;
            this.stateManager.IsPriming = true;
            while (ancestors.Count > 0)
            {
                ActivityInstance currentInstance = ancestors.Pop();
                if (EnsureInstrumented(currentInstance.Activity))
                {
                    this.EnterState(threadId, currentInstance.Activity, GenerateLocals(currentInstance));
                }
            }
            this.stateManager.IsPriming = currentIsPrimingValue;
        }
    }
}
                                                                                                                                                                                                                                                                                                                < t e s t - 3 2 . c s P      A'    µ'    (d¥   ˜DÁgõ‡÷               < t e s t - 3 2 . c s P      A'    µ'    xd¥   ˜DÁgõ‡÷Å               < t e s t - 3 2 . c s P      A'    µ'    »d¥   ˇkÁgõ‡÷Å Ä             < t e s t - 3 2 . c s P      A'    µ'    e¥   ˇkÁgõ‡÷ Ä          Ä    < t e s t - 3 2 . c s P      A'    µ'    he¥   ˇkÁgõ‡÷ Ä Ä        Ä    < t e s t - 3 2 . c s X      B'    µ'    ∏e¥   ˇkÁgõ‡÷                < t e s t - 3 2 0 . c s       X      B'    µ'    f¥   ˇkÁgõ‡÷               < t e s t - 3 2 0 . c s       X      B'    µ'    hf¥   +îÁgõ‡÷               < t e s t - 3 2 0 . c s       X      B'    µ'    ¿f¥   +îÁgõ‡÷Å               < t e s t - 3 2 0 . c s       X      B'    µ'    g¥   +îÁgõ‡÷Å Ä             < t e s t - 3 2 0 . c s       X      B'    µ'    pg¥   +îÁgõ‡÷ Ä          Ä    < t e s t - 3 2 0 . c s       X      B'    µ'    »g¥   +îÁgõ‡÷ Ä Ä        Ä    < t e s t - 3 2 0 . c s       X      C'    µ'     h¥   ªÁgõ‡÷                < t e s t - 3 2 1 . c s       X      C'    µ'    xh¥   ªÁgõ‡÷               < t e s t - 3 2 1 . c s       X      C'    µ'    –h¥   ªÁgõ‡÷               < t e s t - 3 2 1 . c s       X      C'    µ'    (i¥   ªÁgõ‡÷Å               < t e s t - 3 2 1 . c s       X      C'    µ'    Äi¥   ªÁgõ‡÷Å Ä             < t e s t - 3 2 1 . c s       X      C'    µ'    ÿi¥   ªÁgõ‡÷ Ä          Ä    < t e s t - 3 2 1 . c s       X      C'    µ'    0j¥   ªÁgõ‡÷ Ä Ä        Ä    < t e s t - 3 2 1 . c s       X      D'    µ'    àj¥   >·Ágõ‡÷                < t e s t - 3 2 2 . c s       X      D'    µ'    ‡j¥   >·Ágõ‡÷               < t e s t - 3 2 2 . c s       X      D'    µ'    8k¥   >·Ágõ‡÷               < t e s t - 3 2 2 . c s       X      D'    µ'    êk¥   >·Ágõ‡÷Å               < t e s t - 3 2 2 . c s       X      D'    µ'    Ëk¥   >·Ágõ‡÷Å Ä             < t e s t - 3 2 2 . c s       X      D'    µ'    @l¥   >·Ágõ‡÷ Ä          Ä    < t e s t - 3 2 2 . c s       X      D'    µ'    òl¥   >·Ágõ‡÷ Ä Ä        Ä    < t e s t - 3 2 2 . c s       X      E'    µ'    l¥   8	Ëg