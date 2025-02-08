namespace System.Workflow.ComponentModel.Compiler
{
    using System;
    using System.Collections;
    using System.Globalization;

    #region Class ValidationError

    [Serializable()]
    public sealed class ValidationError
    {
        private string errorText = string.Empty;
        private int errorNumber = 0;
        private Hashtable userData = null;
        private bool isWarning = false;
        string propertyName = null;

        public ValidationError(string errorText, int errorNumber)
            : this(errorText, errorNumber, false, null)
        {
        }

        public ValidationError(string errorText, int errorNumber, bool isWarning)
            : this(errorText, errorNumber, isWarning, null)
        {
        }

        public ValidationError(string errorText, int errorNumber, bool isWarning, string propertyName)
        {
            this.errorText = errorText;
            this.errorNumber = errorNumber;
            this.isWarning = isWarning;
            this.propertyName = propertyName;
        }
        public string