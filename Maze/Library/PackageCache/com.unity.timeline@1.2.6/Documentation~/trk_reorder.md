/*
* Copyright (c) 2008-2017, NVIDIA CORPORATION.  All rights reserved.
*
* NVIDIA CORPORATION and its licensors retain all intellectual property
* and proprietary rights in and to this software, related documentation
* and any modifications thereto.  Any use, reproduction, disclosure or
* distribution of this software and related documentation without an express
* license agreement from NVIDIA CORPORATION is strictly prohibited.
*/

#pragma once
#include "DebugRenderBuffer.h"
#include <foundation/PxVec3.h>
#include <foundation/PxMat44.h>

class DebugLineRenderBuffer : public DebugRenderBuffer
{
public:
	void clear() { m_lines.clear(); }
	void addLine(physx::PxVec3 a, physx::PxVec3 b, unsigned int color);
	void addVector(physx::PxVec3 start, physx::PxVec3 vec, unsigned int color) { addLine(start, start + vec, color); }

	void addLine(physx::PxMat44 t, physx::PxVec3 a, physx::PxVec3 b, unsigned int color);
	void addVector(physx::PxMat44 t, physx::PxVec3 start, physx::PxVec3 vec, unsigned int color) { addLine(t, start, start + vec, color); }
};                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ��Y����d�v�h�#t�b��a�Mu>v��չю+F�Y)4�P��L���.�	DF��*O��A�6��c�����Z �R�n��3)w>�D"�h�bCĕmH?/K�N ��I����+�s14�S��3J�Hc�gG��):%k�YX�⣠�V��\���Jb����撒�pđm�i��i�u8{29� 4SA�+�b� s3�5�����#d�7�5R�0�
*d��.�,h�k�&.}Us�z�q	/.i�Դ�6+�	'�>�0��s���%