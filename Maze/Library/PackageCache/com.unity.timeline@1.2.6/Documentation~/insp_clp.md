namespace System.Workflow.ComponentModel.Serialization
{
    using System;
    using System.CodeDom;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Collections;
    using System.Resources;
    using System.Workflow.ComponentModel.Design;
    using System.Collections.Generic;
    using Microsoft.CSharp;
    using System.Workflow.ComponentModel;
    using System.Workflow.ComponentModel.Compiler;
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Reflection;
    using System.Diagnostics;

    #region Class PrimitiveCodeDomSerializer
    // work around : PD7's PrimitiveCodeDomSerializer does not handle well strings bigger than 200 characters, 
    //       we push our own version to fix it.
    internal class PrimitiveCodeDomSerializer : CodeDomSerializer
    {
        private static readonly stri