        // Delete all temporary files 
        public void Exit()
        {
            if (this.temporaryFiles != null)
            {
                foreach (string temporaryFile in this.temporaryFiles)
                {
                    // Clean up published source.
                    try
                    {
                        File.Delete(temporaryFile);
                    }
                    catch (IOException)
                    {
                        // ---- IOException silently.
                    }
                    this.temporaryFiles = null;
                }
            }
            this.stateManager.ExitThreads(); // State manager is still keep for the session in SessionStateManager
            this.stateManager = null;
        }

        void Instrument(Activity activity, SourceLocation sourceLocation, string name)
        {
            Fx.Assert(activity != null, "activity can't be null");
            Fx.Assert(sourceLocation != null, "sourceLocation can't be null");
            if (this.states.ContainsK