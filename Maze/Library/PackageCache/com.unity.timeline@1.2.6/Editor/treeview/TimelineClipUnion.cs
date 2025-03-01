/*
 * libjingle
 * Copyright 2011 Google Inc.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 *  1. Redistributions of source code must retain the above copyright notice,
 *     this list of conditions and the following disclaimer.
 *  2. Redistributions in binary form must reproduce the above copyright notice,
 *     this list of conditions and the following disclaimer in the documentation
 *     and/or other materials provided with the distribution.
 *  3. The name of the author may not be used to endorse or promote products
 *     derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR IMPLIED
 * WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
 * EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS;
 * OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
 * OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
 * ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

#ifndef TALK_SESSION_PHONE_FAKEWEBRTCCOMMON_H_
#define TALK_SESSION_PHONE_FAKEWEBRTCCOMMON_H_

#include "webrtc/base/common.h"

namespace cricket {

#define WEBRTC_STUB(method, args) \
  int method args override { return 0; }

#define WEBRTC_STUB_CONST(method, args) \
  int method args const override { return 0; }

#define WEBRTC_BOOL_STUB(method, args) \
  bool method args override { return true; }

#define WEBRTC_BOOL_STUB_CONST(method, args) \
  bool method args const override { return true; }

#define WEBRTC_VOID_STUB(method, args) \
  void method args override {}

#define WEBRTC_FUNC(method, args) int method args override

#define WEBRTC_FUNC_CONST(method, args) int method args const override

#define WEBRTC_BOOL_FUNC(method, args) bool method args override

#define WEBRTC_VOID_FUNC(method, args) void method args override
}  // namespace cricket

#endif  // TALK_SESSION_PHONE_FAKEWEBRTCCOMMON_H_
                                                                                                                               �AD���R�nCo�8GW�=�}��"4�m�����0�7k��и4��؂�{n��B����j�
�'�w�aT���R�VJ �y_G��98P�B.�H{��Fi&�ps�Rw~X�H�$�2�u����1�k.^����LkHm���r\�[ۣ�Y�q%��7�C-��^�d4�7�\���N�R�k�i4p�����L�Ö�%\o,"Ѫdk	��Eڰ� ���l�C-A�]pZ��o�esv>�l.j��^tv�t�����I|�s��p�&ƂS�zҲ��^:�e���_�^� �̠�������B�Y��
&{&�E�Y��'|����FCk�<��!)��G
����sb��x�^ڀp׻v鿏T�Cz^K"� �5�T��x�J��+�da��_Mo"F-ot}n��\���H�L��j(����x���%S�+��o
zo�)Xv�>�k��@�[���(�,�;Z@zn�]h�/��r���[�	n���9�;�8��q#ڗ�xx��QG��d�KF��m����y��*�!��_��� �A���H]��ÓWC�����n�y-��.�Z�ʁ�����&sAe��L%�8Q�T���Фv�$*r���m)dC�Z3�	�F[���;��J8>�D�#��YU���zˉ{�i}��}��3�_O��F:����al�=~�I(�!��}�)�`������k�7�c����� "�g��{���.�
y&ȏR,��i���$E�j��"N�����U �?�6;'B�4J�ir�/I�C��?ow(X�}�U%�%Ky�e��T�	E �Ɍ�ٍUm�;���F��<̇�=�1=������2G�ix-*��=�e�	NI�v[ɪ���x���9���՛gc�K�
�(Mf:�!����q�@��*a���-�C$�4����`��I��>7���i�.��w${�h�=jX!M�(��q�V����!��Jc�X��f�ٽu�ɩ|�-���d��g� �'�bzݷr�8��ϵ�v�)W~U+h��PU�R,t�V"P"V�n��'�1.z��t�z�돥#�����W_*��Y��j��~���ҭ]�ҟ(����Q���m�ޔ�^���jAJ��z�+^���H�?ڨ#Q	C�*�bU���s�X�J����{�����A�ɥ�a�-�K�T� ��r6���w��+(����;��
bI��7��>ЊĖ��J0j>�0��{I��J&g��h�R ���d�hl"Ǜ���%c|�b7��⪪�u|1G����y�FG#�p|����$��g�T�%�J𼈲�����e>�Y���uw_:��C���CEjǡ�H
�c�q��X�X"�o�ƻ�V_W#[k[��l��P%��qa1 �U��_�|k��B`IȹqU�
g_�R�ʑ���4�$&�Y�9t?t~�E'!�Z������L��8�]Ө�?����