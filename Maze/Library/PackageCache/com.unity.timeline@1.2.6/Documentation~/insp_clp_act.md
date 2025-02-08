namespace System.Workflow.ComponentModel.Serialization
{
    using System;
    using System.CodeDom;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Workflow.ComponentModel.Compiler;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Collections.Generic;

    // This serializer will serialize any ICollection<String> object into code statements that constructs the collection.
    // The default serializer serializes the collection as resource because ICollection<String> is serializable.
    // We originally add this serializer to deal with the SynchronizationHandles property on SynchronizationScopeActivity.
    // It was a problem because this serializer will be invoked for any property of ICollection<String> type.  Now I've 
    // made this generic enough to be used by any such properties.
    internal sealed class SynchronizationHandlesCodeDomSerializer : CodeDomSerializer
    {
        public override object Serialize(IDesignerSerializationManager manager, object obj)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");
            if (obj == null)
                throw new ArgumentNullException("obj");

            CodeExpression retVal = null;
            CodeStatementCollection statements = manager.Context[typeof(CodeStatementCollection)] as CodeStatementCollection;
            System.Diagnostics.Debug.Assert(statements != null);
            if (statements != null)
            {
                Activity activity = (Activity)manager.Context[typeof(Activity)];
                CodeExpressio