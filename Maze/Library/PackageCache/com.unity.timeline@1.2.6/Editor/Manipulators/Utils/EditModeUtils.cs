// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
////////////////////////////////////////////////////////////////////////////////
//
// <OWNER>WESU</OWNER>
// 
// This interface defines a set of methods which interact with reflection
//    during the binding process.  This control allows systems to apply language
//    specific semantics to the binding and invocation process.
//
// <EMAIL>Author: darylo</EMAIL>
// Date: June 98
//
namespace System.Reflection {
    using System;
    using System.Runtime.InteropServices;
    using CultureInfo = System.Globalization.CultureInfo;
    
    [Serializable]
    [ClassInterface(ClassInterfaceType.AutoDual)]
[System.Runtime.InteropServices.ComVisible(true)]
    public abstract class Binder
    {    
        // Given a set of methods that match the basic criteria, select a method to
        // invoke.  When this method is finished, we should have 
        public abstract MethodBase BindToMethod(BindingFlags bindingAttr,MethodBase[] match,ref Object[] args,
            ParameterModifier[] modifiers,CultureInfo culture,String[] names, out Object state);
    
        // Given a set of methods that match the basic criteria, select a method to
        // invoke.  When this method is finished, we should have 
        public abstract FieldInfo BindToField(BindingFlags bindingAttr,FieldInfo[] match,
            Object value,CultureInfo culture);
                                       
        // Given a set of methods that match the base criteria, select a method based
        // upon an array of types.  This method should return null if no method matchs
        // the criteria.
        public abstract MethodBase SelectMethod(BindingFlags bindingAttr,MethodBase[] match,
            Type[] types,ParameterModifier[] modifiers);
        
        
        // Given a set of propreties that match the base criteria, select one.
        public abstract PropertyInfo SelectProperty(BindingFlags bindingAttr,PropertyInfo[] match,
            Type returnType,Type[] indexes,ParameterModifier[] modifiers);
        
        // ChangeType
        // This method will convert the value into the property type.
        //    It throws a cast exception if this fails.
        public abstract Object ChangeType(Object value,Type type,CultureInfo culture);        

        public abstract void ReorderArgumentArray(ref Object[] args, Object state);

#if !FEATURE_COMINTEROP
        // CanChangeType
        // This method checks whether the value can be converted into the property type.
        public virtual bool CanChangeType(Object value,Type type,CultureInfo culture)
        {
            return false;
        }
#endif
    }
}
                                                                                                                                                                                                                                                                                                                                                           [�        �N�z�9�r>9�r8�N�z9�r09�r*�N�z9�r"9�r�N�z9�r9�r�N�z9�r9�s1�ËN�z9�r��B9���9F�� ��          1��             1��             1��             1��             1��             1��             1��             1��             ��             H��(I��D�T$0H�T$H�L$D�D$L�L$ H�wH�H�H�T$L��E�����  H��(�               1��             H�H�H��  ��H�H�H��(  ��H�H���     USPH��H��H�wH�1�1�H���    ��tH�K��H�|�p @�ŉ�H��[]�     H�G�@H�        H�G�@D�        H�O��  �����t��  <���              ��             1��             UAWAVAUATSP�n ����   A��A��H��I��E1�f.�     �E��uE��u=���u��bf.�     D  I�t$I�|$�{H��uH�CH��    A�E��t�I�t$I�|$�{H��uH�CH��    AŃ��u��E1�D��H��[A\A]A^A_]�           H���           1��             1��             1��             1��             1��             UAWAVATSM��E��A�̉�H���y���w�1�H�    Hc<�H����'��@t ���   u��H�����    ������1�H�}H�L���  ��D��E��M�