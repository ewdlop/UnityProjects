/*
* Copyright (c) 2008-2017, NVIDIA CORPORATION.  All rights reserved.
*
* NVIDIA CORPORATION and its licensors retain all intellectual property
* and proprietary rights in and to this software, related documentation
* and any modifications thereto.  Any use, reproduction, disclosure or
* distribution of this software and related documentation without an express
* license agreement from NVIDIA CORPORATION is strictly prohibited.
*/

#include "JobManager.h"
#define _USE_MATH_DEFINES
#include <math.h>
#include <NvCloth/Solver.h>

void Job::Initialize(JobManager* parent, std::function<void(Job*)> function, int refcount)
{
	mFunction = function;
	mParent = parent;
	Reset(refcount);
}

Job::Job(const Job& job)
{
	mFunction = job.mFunction;
	mParent = job.mParent;
	mRefCount.store(job.mRefCount);
	mFinished = job.mFinished;
}

