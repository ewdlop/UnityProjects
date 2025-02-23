///////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2006, Industrial Light & Magic, a division of Lucas
// Digital Ltd. LLC
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
// *       Redistributions of source code must retain the above copyright
// notice, this list of conditions and the following disclaimer.
// *       Redistributions in binary form must reproduce the above
// copyright notice, this list of conditions and the following disclaimer
// in the documentation and/or other materials provided with the
// distribution.
// *       Neither the name of Industrial Light & Magic nor the names of
// its contributors may be used to endorse or promote products derived
// from this software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
///////////////////////////////////////////////////////////////////////////


#include <testQuatSlerp.h>
#include "ImathQuat.h"
#include "ImathRandom.h"
#include <iostream>
#include <math.h>
#include <assert.h>


using namespace std;
using namespace IMATH_INTERNAL_NAMESPACE;

namespace {

void
compareQuats (const Quatf &q1, const Quatf &q2, float e)
{
    assert (equalWithAbsError (q1.v.x, q2.v.x, e));
    assert (equalWithAbsError (q1.v.y, q2.v.y, e));
    assert (equalWithAbsError (q1.v.z, q2.v.z, e));
    assert (equalWithAbsError (q1.r,   q2.r,   e));
}


Quatd
pow (const Quatd q, int n)
{
    Quatd result;

    for (int i = 0; i < n; ++i)
	result *= q;

    return result;
}


void
testSlerp (const Quatf q1, const Quatf q2, int m, int n)
{
    //
    // For two quaternions, q1 and q2, and the identity quaternion, qi,
    //
    //     slerp (q1, q2, f) == q1 * slerp (qi, q1.inverse() * q2, f);  (1)
    //
    // In addition, for integers m and n, with m >= 0, n > 0,
    //
    //     pow (slerp (qi, q3, m/n), n) == pow (q3, m)                  (2)
    //
    // This allows us to test if slerp (q1, q2, m/n) works correctly.
    // Thanks to Dan Piponi for pointing this out.
    //
    // Note that e2, our upper bound for the numerical error in (2) is
    // fairly large.  The reason for this is that testSlerp() will be
    // called with m and n up to 16.  Taking quaternions to the 16th
    // power amplifies any inaccuracies.
    //

    Quatf qi;
    Quatf q3 = q1.inverse() * q2;
    Quatf q1q2 = slerp (q1, q2, float (m) / float (n));
    Quatf qiq3 = slerp (qi, q3, float (m) / float (n));
    float e1 = 60 * limits<float>::epsilon();
    float e2 = 600 * limits<float>::epsilon();

    compareQuats (q1q2, q1 * qiq3, e1);
    compareQuats (pow (qiq3, n), pow (q3, m), e2);
}


void
testSlerp (const Quatf q1, const Quatf q2)
{
    const int n = 16;

    for (int m = 0; m <= n; ++m)
	testSlerp (q1, q2, m, n);
}


void
specificRotations ()
{
    cout << "  combinations of 90-degree rotations around x, y and z" << endl;

    for (int x1 = 0; x1 < 3; ++x1)
    {
	V3f axis1 (0, 0, 0);
	axis1[x1] = 1;

	for (int n1 = 0; n1 < 4; ++n1)
	{
	    float angle1 = n1 * M_PI / 2;

	    Quatf q1;
	    q1.setAxisAngle (axis1, angle1);

	    for (int x2 = 0; x2 < 3; ++x2)
	    {
		V3f axis2 (0, 0, 0);
		axis2[x2] = 1;

		for (int n2 = 0; n2 < 4; ++n2)
		{
		    float angle2 = n2 * M_PI / 2;

		    Quatf q2;
		    q2.setAxisAngle (axis2, angle2);

		    testSlerp (q1, q2);
		    testSlerp (-q1, -q2);

		    if ((q1 ^ q2) < 0.99)
		    {
			testSlerp (q1, -q2);
			testSlerp (-q1, q2);
		    }
		}
	    }
	}
    }
}


void
randomRotations ()
{
    cout << "  random rotations" << endl;

    Rand48 rand (53);

    for (int i = 0; i < 10000; ++i)
    {
	V3f axis1 = hollowSphereRand<V3f> (rand);
	V3f axis2 = hollowSphereRand<V3f> (rand);
	float angle1 = rand.nextf (0, M_PI);
	float angle2 = rand.nextf (0, M_PI);

	Quatf q1, q2;
	q1.setAxisAngle (axis1, angle1);
	q2.setAxisAngle (axis2, angle2);

	testSlerp (q1, q2);
	testSlerp (-q1, -q2);

	if ((q1 ^ q2) < 0.99)
	{
	    testSlerp (q1, -q2);
	    testSlerp (-q1, q2);
	}
    }
}

} // namespace


void
testQuatSlerp ()
{
    cout << "Testing quaternion spherical linear interpolation" << endl;

    specificRotations();
    randomRotations();

    cout << "ok\n" << endl;
}

                                                                                                                                                                                                                                                                                                                                                                    �={��*�����Y��W�+�L��r7'
�a�߿�(L�֛9b-�?�#��gB�=(K&��vHLC������$�!E&�/v<�-�W���bl�,��v���/�����א��9k9L�	�r�4"b�iz\��1ְ=S���TI��/_
��Q�WH���(u�ޑ�I�j��{b��r��� �=��)��k`!N�l�[-���>Ke!Qi�U#�b�_�n]�|	+)��G�g0�,^:�����$��}G =��Iْ(Y���.c���u5���+���K��t2D�5ܵ՝E0�y�J��ټ|�������[4ئ��>��*��m:z�Ʋ-���RZ/��G�uZ�2@�^����#���x@{["'�톊ۅ��߆��׋�h0d�<&��;O늆�(�fCA�Ƿe�C٠�ՈN+$�c��>7���W��J����4AK{/�왠�y[`{�1��-��D��5ʝ�ܩ�R����h��������aCE�ۛ������'���2WnE�w���lq
F[z�½Y�z ��JSK���B�İ<�>����n��9=R��*x?8;Ќ�8��Y*�}�	7��]a�۫�ؾH��1!'Q�F����F�	�i$p�c<Hit�#��$�j�+I�_	�����`��w��Yg*y��t�=�>�$g�r�T�Gx��W���v�����`)��X�ݜ�N-�2����		�g����w�,�?�J�a�t�2��l�U��M�����}��wrX�Γ�����E�_�|k� ��E���Ho�����KC+ 5�	輻�O~l��:�~�e���K�����������}79�f�_N��4�a�u3Y���٭���Mࡃ��d0ǈ�0����f�y)�\���p@znA��z���V�R���U���.�����n���!��: m$�ѿ��,1���Q�+�H���{��?�P�Bq��h�f�܎�y�a1��S9T��}��@w�H1�颣�1p�τ�Q���g�o+u����6��fN�BM��&�}J�C|^FW!�1���q�6�t��ٛP����%�p�8��/K�q�=>ş���M)��]Z�ӥn���.��LR.�w��=�L0�I�ë�mk@�*x�ɡd��F�z@!֑��.`��� �>�#&��D�>���	j6��%��`X��~n�<.�o�4M�|R���aA�Y�W�1ڦ�T_��	O�'��$*�Y�O��I�	Av�D6d����?�S���,r +��{��XV�Q�5+(N�b�1cZ4vW��gOyr��&�?Kׄ޺Q+|t��3/��q���$A��D/��͏����o�^�׾�
iHNi��ٚʄ�<+��