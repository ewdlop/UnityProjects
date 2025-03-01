//------------------------------------------------------------------------------
// <copyright file="DoWorkEventArgs.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace System.ComponentModel 
{
    using System.Security.Permissions;

    [HostProtection(SharedState = true)]
    public class DoWorkEventArgs : CancelEventArgs
    {
        private object result;
        private object argument;

        public DoWorkEventArgs(object argument)
        {
            this.argument = argument;
        }

        [ SRDescription(SR.BackgroundWorker_DoWorkEventArgs_Argument) ]
        public object Argument
        {
            get { return argument; }
        }

        [ SRDescription(SR.BackgroundWorker_DoWorkEventArgs_Result) ]
        public object Result
        {
            get { return result; }
            set { result = value; }
        }
    }
}

                 0           0     0     644     18744     `
ELF          >                    8:          @     @ < ;    !   "      #   $      %   &      '   (      )   *      +   ,      -   .      0   1      2      3   4               W�G�     AWAVATSPH�    L�wL�gM9�tFI��L��f.�     @ ���    tƃ�    H�{H�CH9�t�    H�à   I9�u�M�wH��[A\A^A_�  UAWAVAUATSH���   I��H��L��$   H�    L�gL�oM9�t?L��f.�     ���    tƅ�    H�}H�EH9�t�    H�Š   I9�u�L�cA��D$�H�l$(H���    I�GH�D$A$H��L���    ����   H�CH�D$ E1�L�l$( �{ tA���L$(�����A9�u�fD  �D$(���CA��H�kH;ktzH�D$(H�E H�EH�EH�       H�E�|$8 tH�}H�t$0�    ��$�   ���   ��$�    t"H�D$0H�����   f���   ���   ���   H�C�   �f�H�|$ H��L���    I�GH�D$A$L��L���    ������A���$�    tƄ$�    H�|$0H�D$@H9�t�    9\$��H���   [A\A]A^A_]�          AWAVSH�_L�L9�t%I��f.�     �H��L���    H�à   I9�u�[A^A_�   �O�����H9�t9�v11��H�GH�OH9�tZf.�     D  90tJH�   H9�u�1��A��H�GH�H)�H��H���������H��H�L9�v)�H��H��H��1��1��� AVSPH��L�w�G    H�G    L�