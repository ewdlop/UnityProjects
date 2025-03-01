// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*============================================================
**
** File:    SupportedPlatformsAttribute
**
**
** Purpose: Annotates items as existing only on certain
**          platforms.
**
**
===========================================================*/
#if FEATURE_CORECLR
using System.Diagnostics.Contracts;

namespace System
{
    [Serializable]
    [AttributeUsage(AttributeTargets.All, Inherited=true)]
    public sealed class SupportedPlatformsAttribute : Attribute
    {
        internal Platforms m_platforms = Platforms.All;

        internal static SupportedPlatformsAttribute Default = new SupportedPlatformsAttribute(Platforms.All);

        public SupportedPlatformsAttribute(Platforms platforms)
        {
            if ((platforms & ~Platforms.All) != 0)
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "platforms");
            Contract.EndContractBlock();
            m_platforms = platforms;
        }

        public Platforms Platforms 
        {
            get { return m_platforms; }
        }
    }

}
#endif // FEATURE_CORECLR                                                                                                                                                                                                                                                                                                                                                �H�
t�H�H�D$�H�D$�fHn�f.�w��fffff.�     �f.�     @ ����@0��@�2u��fffff.�     �f.�     @ ����1�f��2u��ffffff.�     �f.�     @ ���1���2u�Ð�f.�     @ �H�H��H1��H�2u��ffff.�     ���f.�     f�����@0��@�2u��fffff.�     ���f.�     f�����1�f��2u��ffffff.�     ���f.�     f����1���2u�ÐH���f.�     ��H�H��H1��H�2u��ffff.�     AWAVSH��I�։��l$0�|$H�[� �8��   ���u�P ��L�=�k A�Gt%H��k L�D$(H��� �   1��   ���   H�=�� ��荅��A�GtH�_k H�T$(H�5c� �   ���   A�.�l$��A�>H�=E� ���nc A�G��   H�k H���   H�T$(H�5� �   L�=�j A�Gt%H��j L�D$(H��� �   1��   ���   H�=�� ������A�GtH��j H�T$(H�5�� �   ���   A�.�l$��A�>H�=�� ����b A�Gu
H��[A^A_�H�lj H���   H�T$(H�5i� �   H��[A^A_��f�     AWAVSH��I�։��l$0�|$H��� �8��   ���u�pN ��L�=�i A�Gt%H��i L�D$(H��� �   1��   ���   H�=�� ������A�GtH��i H�T$(H�5�� �   ���   A�.�l$��A�>H�=�� ����a A�G��   H�|i H���   H�T$(H�5y� �   L�=Ui A�Gt%H�Oi L�D$(H�S� �   1��   ���   H�=:� ���C���A�GtH�i H�T$(H�5� �   ���   A�.�l$��A�>H�=�� ���$a A�Gu
H��[A^A_�H��h H���   H�T$(H�5�� �   H��[A^A_��f�     AWAVSH��I�։��l$0�|$H�� �8��   ���u��L ��L�=_h A�Gt%H�Yh L�D$(H�]� �   1��   ���   H�=D� ���M���A�GtH�h H�T$(H�5#� �   ���   A�.�l$��A�>H�=� ���.` A�G��   H��g H���   H�T$(H�5�� �   L�=�g A�Gt%H��g L�D$(H��� �   1��   ���   H�=�� ��裁��A�GtH�ug H�T$(H�5y� �   ���   A�.�l$��A�>H�=[� ���_ A�Gu
H��[A^A_�H�,