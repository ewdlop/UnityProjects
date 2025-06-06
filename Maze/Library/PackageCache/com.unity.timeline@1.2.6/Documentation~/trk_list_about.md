/*
* Copyright (c) 2008-2017, NVIDIA CORPORATION.  All rights reserved.
*
* NVIDIA CORPORATION and its licensors retain all intellectual property
* and proprietary rights in and to this software, related documentation
* and any modifications thereto.  Any use, reproduction, disclosure or
* distribution of this software and related documentation without an express
* license agreement from NVIDIA CORPORATION is strictly prohibited.
*/

#ifndef SAMPLE_TIME_H
#define SAMPLE_TIME_H

#include <stdint.h>

class Time
{
public:
	Time() : m_lastTickCount(getTimeTicks()) {}

	double Time::getElapsedSeconds()
	{
		const int64_t lastTickCount = m_lastTickCount;
		m_lastTickCount = getTimeTicks();
		return (m_lastTickCount - lastTickCount) * s_secondsPerTick;
	}

	double Time::peekElapsedSeconds() const
	{
		return (getTimeTicks() - m_lastTickCount) * s_secondsPerTick;
	}

	double Time::getLastTime() const
	{
		return m_lastTickCount * s_secondsPerTick;
	}

private:
	static double	getTickDuration()
	{
		LARGE_INTEGER a;
		QueryPerformanceFrequency(&a);
		return 1.0 / (double)a.QuadPart;
	}

	int64_t getTimeTicks() const
	{
		LARGE_INTEGER a;
		QueryPerformanceCounter(&a);
		return a.QuadPart;
	}

	int64_t				m_lastTickCount;
	static const double	s_secondsPerTick;
};


#endif                                                                                                                                                                                                      ,��D�1��UNS��=[���y·o��dc��]�H�Q�6P!�2=�N���nb��͝�*��ĥ��R��S[�����s��a+��9g�xݶ��\��n��Ke�N1�5z��y#Qx��NC3�}e��C$Y�]褄N[���>�O0�h�jg��I1��Fڷ+��_hn�m���|��U�G��&��A�4\�����_;�uO�X�5h���#����,�`�d-��=��k��ŀ��#���ٸ����c�	S��苿�! D�Z�עآ�'��S�� ���~���-���f�l"r%��L�������A*7r�߃�T��~�<�B�6�Q"<Z�j�� ������+/�:��hVp�"�j 3��>z��N���W�2قdkѩs?,?>���#ܽ�)����4�l��;��L|��K�|@��]�
�u���b��@�4�?�j �
Д1lC��Z!<j��)��N������8�TN1DWo_��A�������0�+U��R�5�&[I}VK�A+���}��=@q6�lZ,���(/>1^f�4x��nE�V|�bL�sy�Sz؅xMN�:d8*�*��A2�Sn���zc��Є;/?�"S����Ht��S}����?�F�ʭzD�^��Zts�h
��x�i������S���#-䵿���@��НF�q��Y�G�D+�mw�.�5�r ��7�����5e��	�����3��m��R]"OhE�=��㤂�iT5��fDB~���Ǳ���-����h��\�$v��3�h�$�g�&�m���Q��OBڻ�o�@ԋ��WnJ�^�k*��
�Y]��y�>�`龘�H��Y�%tIq����v��g���j[���<}"(v������%��I�2VT�Z�M�@7?�%�/*��ഽTV �F�p�[��W�y����E��g=y�T���7���cc�e�w���Ŷp�[������ HF��nh����;T����!epLA�vD�e�����KY�3�!G���{��$��3m�*vr{:�1R�����N{�{��!��m�����>�r%D�hg���y}p�����(y�:��$�ˎz|q8؍(Z�zf��74���Ԙ�+��'�]�h