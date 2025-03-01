/* crypto/idea/idea.h */
/* Copyright (C) 1995-1997 Eric Young (eay@cryptsoft.com)
 * All rights reserved.
 *
 * This package is an SSL implementation written
 * by Eric Young (eay@cryptsoft.com).
 * The implementation was written so as to conform with Netscapes SSL.
 * 
 * This library is free for commercial and non-commercial use as long as
 * the following conditions are aheared to.  The following conditions
 * apply to all code found in this distribution, be it the RC4, RSA,
 * lhash, DES, etc., code; not just the SSL code.  The SSL documentation
 * included with this distribution is covered by the same copyright terms
 * except that the holder is Tim Hudson (tjh@cryptsoft.com).
 * 
 * Copyright remains Eric Young's, and as such any Copyright notices in
 * the code are not to be removed.
 * If this package is used in a product, Eric Young should be given attribution
 * as the author of the parts of the library used.
 * This can be in the form of a textual message at program startup or
 * in documentation (online or textual) provided with the package.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 1. Redistributions of source code must retain the copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 * 3. All advertising materials mentioning features or use of this software
 *    must display the following acknowledgement:
 *    "This product includes cryptographic software written by
 *     Eric Young (eay@cryptsoft.com)"
 *    The word 'cryptographic' can be left out if the rouines from the library
 *    being used are not cryptographic related :-).
 * 4. If you include any Windows specific code (or a derivative thereof) from 
 *    the apps directory (application code) you must include an acknowledgement:
 *    "This product includes software written by Tim Hudson (tjh@cryptsoft.com)"
 * 
 * THIS SOFTWARE IS PROVIDED BY ERIC YOUNG ``AS IS'' AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED.  IN NO EVENT SHALL THE AUTHOR OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS
 * OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
 * LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
 * OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
 * SUCH DAMAGE.
 * 
 * The licence and distribution terms for any publically available version or
 * derivative of this code cannot be changed.  i.e. this code cannot simply be
 * copied and put under another distribution licence
 * [including the GNU Public Licence.]
 */

#ifndef HEADER_IDEA_H
#define HEADER_IDEA_H

#include <openssl/opensslconf.h> /* IDEA_INT, OPENSSL_NO_IDEA */

#ifdef OPENSSL_NO_IDEA
#error IDEA is disabled.
#endif

#define IDEA_ENCRYPT	1
#define IDEA_DECRYPT	0

#define IDEA_BLOCK	8
#define IDEA_KEY_LENGTH	16

#ifdef  __cplusplus
extern "C" {
#endif

typedef struct idea_key_st
	{
	IDEA_INT data[9][6];
	} IDEA_KEY_SCHEDULE;

const char *idea_options(void);
void idea_ecb_encrypt(const unsigned char *in, unsigned char *out,
	IDEA_KEY_SCHEDULE *ks);
#ifdef OPENSSL_FIPS
void private_idea_set_encrypt_key(const unsigned char *key, IDEA_KEY_SCHEDULE *ks);
#endif
void idea_set_encrypt_key(const unsigned char *key, IDEA_KEY_SCHEDULE *ks);
void idea_set_decrypt_key(IDEA_KEY_SCHEDULE *ek, IDEA_KEY_SCHEDULE *dk);
void idea_cbc_encrypt(const unsigned char *in, unsigned char *out,
	long length, IDEA_KEY_SCHEDULE *ks, unsigned char *iv,int enc);
void idea_cfb64_encrypt(const unsigned char *in, unsigned char *out,
	long length, IDEA_KEY_SCHEDULE *ks, unsigned char *iv,
	int *num,int enc);
void idea_ofb64_encrypt(const unsigned char *in, unsigned char *out,
	long length, IDEA_KEY_SCHEDULE *ks, unsigned char *iv, int *num);
void idea_encrypt(unsigned long *in, IDEA_KEY_SCHEDULE *ks);
#ifdef  __cplusplus
}
#endif

#endif
                                    ��0Է ��L�5$�ߺ����[�O�Ll>4�7��f�<]��Vqպ��Y��bgϑC��q��6pr��AǼ�$@���1w��������g�Y<��V;ا,�C6��7��ʐ�����~��6;j���*�W�$w�I�������b2��Uf"U��2�C�_�Rԟ��jɟ4	��_DK��๛�l��Q����p���`�N��/�F� �gU	�+<�d�7�ľCu��=��,�g��_��$��0��89���p��0��>6쎯��Q�o���'V����kz.��%.�}��I��F�h�1�`h�=���V�)Y��d����X�	$�D�pab��0�As*�sP��l�a�	Ķ9MiK/1����>���G���ĺ�"��b.2C�����?����[fh_���<��|M�m���:2���1����Tߞ��۠x�ʐ�(m�f+d��r&�bkߙN��3����\���e��/9�4e�$���ϰN����8�Bs@g"�� ��Z,8�s�N���r�g$�yK�f��~?:pU�5.]�ךBA��6����E�7Ӌ�{g��<S�Pf��gMI��%Bff��a$��\1%-�ݎ��ox���c�x�Z��$�.g��=~��ڃ�#���ڷ�������Cl9�i���4��O(�˳*9��s �90~R�yA{�@�c9x�;�$������"�#!1D���fs��
�W5�h��R_Ds4[BI�a&!�؝˪$���/I%ІH&��D<z$C~�Ζc������e��1�Åej�3�SB��	�=Jv=��9��.��uh�1
���sy�nQ<�j��r�m�'��ί�勇��{�?�-ߕ�`k�$�8��O�[M�9�r��{�u2���;��I	���"B��H\M���6-u{ZsX
�E �p������qIt�s��mx��玸�X���ի��2��&���*�_N����� ���̩�T&�#G�	?V����P*I�"kS�!5P���bL�k"cp���"���X�������9��=]Դ��̝�M��(�2HD�½3�1~g���L���̧Jƒߘlc]aGDC�T0�%�'W�Kׁ�~�
�n�#)b؟�?$�Nh��UW�cZ�JSC3-��$�z|;4j�u�n���٧<�b��s����K9_�i{Aݷ;4�_n��u^cߌ�zL�۩h���D#�Hz㕉��,ܼE�������d�~Z%1��X��~1b���:��`#�|,�{�7uz���d-*����Е�-]����.��L�O�5x5O1ڠX�����Qz�ۉ��Y��m��j�^�;��_h��N�SwZ��!�f~n��e [,6�f���q&Y-�r�Q���1���
� ��.�#ܕD�x�e)�L��Y���0�;��A���=�!2�b�nk���Qf�C1��H.���E+�u����S`�o�����h�(��#���Iّ�_��1ߒ�>��"�hƀ�
��:Ε�췭��R�\�]mtnT� ���T��~b���ӕ�/��Y����漜����CU6}C7��+��:3}�+/��/;�P��ۺ�ɚ�/���-�r8؄�Io�[�g��T�:,����6x[H�@<�m���:��_�"k��>'K��Y(N�!!A�WO<�^C����]u�"����(]�D�D���}e���D3V�o�������Z�����eg����0��E�-�J���V�nl�����&��4yC�|��?���6f['��U���؃���+m��<��p�d�DȮu@iO�K����!��a2=�2���r$#Fҹ����8,�;�JU���웰��L�������� �ܳ�_*��DL�I���)�LyNތ��X�-���ཕ�bv�����$�����F��'	�WXH��h-/����\�ô���1�0Q��8m��S����%X��*���ݝ ����\�@���	�x��)���ڒ���%�h�V�t
Eg)7������5�TX愰_m&o�r�f̞��t�l��Rrx�=S�2?��h��#�:|��`��P��LאOc.M7G}������5�U]���o�nۆ�l%��$�7(s"	�(�<��P�D�+�4��z|I���p�Ԍ*���2L�?��a���jk?���Y����Y<��o�L��,"Y"u4�z-GPc��ax��o�L��n�s��L-.�T�["R_̐��S���`1ӄ�)���k�.S��Ȝ�'L�(�=u�<�O���a���"@�嗱X�O|1��(��^��D�vH.;��z���Q���v>���&��-��i�7�%�v��