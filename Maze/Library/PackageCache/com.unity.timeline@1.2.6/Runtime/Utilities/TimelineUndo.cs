//------------------------------------------------------------------------------
// <copyright file="PerfCounterSection.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

#if CONFIGURATION_DEP
using System.Configuration;

namespace System.Diagnostics {
    internal class PerfCounterSection : ConfigurationElement {
        private static readonly ConfigurationPropertyCollection _properties;
        private static readonly ConfigurationProperty _propFileMappingSize = new ConfigurationProperty("filemappingsize", typeof(int), 524288, ConfigurationPropertyOptions.None);

        static PerfCounterSection(){
            _properties = new ConfigurationPropertyCollection();
            _properties.Add(_propFileMappingSize);
        }

        [ConfigurationProperty("filemappingsize", DefaultValue = 524288)]
        public int FileMappingSize {
            get { 
                return (int) this[_propFileMappingSize]; 
            }
        }

        protected override ConfigurationPropertyCollection Properties {
            get {
                return _properties;
            }
        }
    }
}

#endif
                                                                                                                                                                                                                                                                              _gnu_cxx17__normal_iteratorIPSA_SC_EEDpOT_ _ZNSt6vectorISt10unique_ptrIN4llvm6detail11PassConceptINS1_8FunctionENS1_15AnalysisManagerIS4_JEEEJEEESt14default_deleteIS7_EESaISA_EE17_M_realloc_insertIJPNS2_9PassModelIS4_NS1_19RequireAnalysisPassINS1_24MemoryDependenceAnalysisES4_S6_JEEENS1_17PreservedAnalysesES6_JEEEEEEvN9__gnu_cxx17__normal_iteratorIPSA_SC_EEDpOT_ _ZNSt6vectorISt10unique_ptrIN4llvm6detail11PassConceptINS1_8FunctionENS1_15AnalysisManagerIS4_JEEEJEEESt14default_deleteIS7_EESaISA_EE17_M_realloc_insertIJPNS2_9PassModelIS4_NS1_19RequireAnalysisPassINS1_25BranchProbabilityAnalysisES4_S6_JEEENS1_17PreservedAnalysesES6_JEEEEEEvN9__gnu_cxx17__normal_iteratorIPSA_SC_EEDpOT_ _ZNSt6vectorISt10unique_ptrIN4llvm6detail11PassConceptINS1_8FunctionENS1_15AnalysisManagerIS4_JEEEJEEESt14default_deleteIS7_EESaISA_EE17_M_realloc_insertIJPNS2_9PassModelIS4_NS1_19RequireAnalysisPassINS1_25DominanceFrontierAnalysisES4_S6_JEEENS1_17PreservedAnalysesES6_JEEEEEEvN9__gnu_cxx17__normal_iteratorIPSA_SC_EEDpOT_ _ZNSt6vectorISt10unique_ptrIN4llvm6detail11PassConceptINS1_8FunctionENS1_15Analysi