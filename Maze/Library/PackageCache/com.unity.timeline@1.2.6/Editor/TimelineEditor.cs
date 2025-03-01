/* crypto/stack/stack.h */
/* Copyright (C) 1995-1998 Eric Young (eay@cryptsoft.com)
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

#ifndef HEADER_STACK_H
# define HEADER_STACK_H

#ifdef  __cplusplus
extern "C" {
#endif

typedef struct stack_st {
    int num;
    char **data;
    int sorted;
    int num_alloc;
    int (*comp) (const void *, const void *);
} _STACK;                       /* Use STACK_OF(...) instead */

# define M_sk_num(sk)            ((sk) ? (sk)->num:-1)
# define M_sk_value(sk,n)        ((sk) ? (sk)->data[n] : NULL)

int sk_num(const _STACK *);
void *sk_value(const _STACK *, int);

void *sk_set(_STACK *, int, void *);

_STACK *sk_new(int (*cmp) (const void *, const void *));
_STACK *sk_new_null(void);
void sk_free(_STACK *);
void sk_pop_free(_STACK *st, void (*func) (void *));
_STACK *sk_deep_copy(_STACK *, void *(*)(void *), void (*)(void *));
int sk_insert(_STACK *sk, void *data, int where);
void *sk_delete(_STACK *st, int loc);
void *sk_delete_ptr(_STACK *st, void *p);
int sk_find(_STACK *st, void *data);
int sk_find_ex(_STACK *st, void *data);
int sk_push(_STACK *st, void *data);
int sk_unshift(_STACK *st, void *data);
void *sk_shift(_STACK *st);
void *sk_pop(_STACK *st);
void sk_zero(_STACK *st);
int (*sk_set_cmp_func(_STACK *sk, int (*c) (const void *, const void *)))
 (const void *, const void *);
_STACK *sk_dup(_STACK *st);
void sk_sort(_STACK *st);
int sk_is_sorted(const _STACK *st);

#ifdef  __cplusplus
}
#endif

#endif
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 F�	w�A��G(L��~����ND�b�h0�(L��.P1)�Uڲs���P���
�}P��M��$/%���ne5^7UoC���� ���2���~
�5��JMdC�B�ǒ����@d�.-�#Gی}���x�9�����x��Z��������}�[{b�	xܟ�JK��ͅB���T��c�R7x#Sp�B�s��6��M�>�^P�<*�n-�'MLɻ;��|SB�6�
/�� �����p�:����*	q��ټ��@��ӕ,��"8�a��k�������tz��zԡ%�P[Z��u��ybF��[J��S�����yʊ㘘߮� "L��R��N�{>@��� b�VX{��b���U,p4��,J���m���2
�?��'�>l't�O�c�2����� ��������!rꙜ-��yZ`�MZ��t*����f��,-`�7[�|ڳ���=�6��u�,;���o�8�"�닌x���� ��ם"ړ��������j�ג�j�C���EkTSk�ՕS^ʂL&�eS����=��#���x�U�{�j�G^�F����;sC����(W?j����ZMr"_�k1�9kO>NC�/$a�v0r�Jn��Vqy� �2�bSy����XX��xj��T��iqJCǅu~:�H@Kzn7�����lN���`2�~�����9Al쀪��k����޲��TĄMU�L�^��0���h�(r[���v���\͙�j�l&p�=�Gi�ަ^�B���J��@�}S�X,��I���ƲSJ�$� ��1�i�m��D�Qq�σإ X�W����_>�%T`�Z<��%�D C���b<�ߌ���qN���s�J��j�-7�6�`���ݛ\S�������f;,G�ô5�SC�]$▷��$�#s�1��)�^�>n��:Bц���>�P��IaV��Ȣ	�<���J��"��n�a7h	L4�,��'��҂I'�7�U�;�
�HMV98�Cё׵�P2,5����:�!T�ά���rUL���$�w��9+K�����ɠjԆ��Y���W^5����tq��͗}3Ha����?��&�N�K��7|�	��|�	���� ]��D��)�0��$d$L}���y�DM�\5����+�V�n_O���*���q�#�3��52L�S_�� �����|��/��7����[XxC�/G��|G�*׋]������k�[�r��^� �'e�	�K��#������'��7h��`H�{-_0?q[H��`�[0�7
��eY&����;�>�������Z�5R�?V��6�΢�\�D�����2�`
D9�O��"^�|�%mo���á�^+SGE$@��>�O+LF�(�a��=;�6�Js�l��bx�aJ:���Ns��$�K�
�4���.7Q�-��y �5�8��l���