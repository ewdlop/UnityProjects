// ===========================================================================
//	RefNumIO.cpp								Part of OpenEXR
// ===========================================================================

#include "RefNumIO.h"

#include <IexBaseExc.h>
#include <PITypes.h>		// for Macintosh and MSWindows defines


// ===========================================================================
//	Macintosh IO Abstraction 
//
//  use 64-bit HFS+ APIs if the system supports them,
//	fall back to 32-bit classic File Manager APIs otherwise
// ===========================================================================

#pragma mark ===== Macintosh =====

#if Macintosh

#include <Gestalt.h>
#include <Files.h>


//-------------------------------------------------------------------------------
// HaveHFSPlusAPIs
//-------------------------------------------------------------------------------

static bool HaveHFSPlusAPIs ()
{
	static bool sCheckedForHFSPlusAPIs 	= 	false;
	static bool sHaveHFSPlusAPIs		=	false;

	if (!sCheckedForHFSPlusAPIs)
	{
		long 	response 	=	0;
		OSErr	err			=	noErr;
	
		err = Gestalt (gestaltFSAttr, &response);
		
		if (err == noErr && (response & (1 << gestaltHasHFSPlusAPIs)))
		{
			sHaveHFSPlusAPIs = true;	
		}
	
		sCheckedForHFSPlusAPIs = true;
	}

	return sHaveHFSPlusAPIs;
}


//-------------------------------------------------------------------------------
// Read
//-------------------------------------------------------------------------------

static bool Read (short refNum, int n, void* c)
{
	OSErr err = noErr;
	
	if (HaveHFSPlusAPIs())
	{
		ByteCount 	actual;
		
		err = FSReadFork (refNum, fsFromMark, 0, n, c, &actual);
	}
	else
	{
		long count = n;
		
		err = FSRead (refNum, &count, c);
	}
	
	return (err == noErr);
}


//-------------------------------------------------------------------------------
// Write
//-------------------------------------------------------------------------------

static bool Write (short refNum, int n, const void* c)
{
	OSErr err = noErr;
	
	if (HaveHFSPlusAPIs())
	{
		ByteCount 	actual;
		
		err = FSWriteFork (refNum, fsFromMark, 0, n, c, &actual);
	}
	else
	{
		long count = n;
		
		err = FSWrite (refNum, &count, c);
	}
	
	return (err == noErr);
}


//-------------------------------------------------------------------------------
// Tell
//-------------------------------------------------------------------------------

static bool Tell (short refNum, Imf::Int64& pos)
{
	OSErr err = noErr;
	
	if (HaveHFSPlusAPIs())
	{
		SInt64 p;
		
		err = FSGetForkPosition (refNum, &p);
		pos = p;
	}
	else
	{
		long p;
		
		err = GetFPos (refNum, &p);
		pos = p;
	}
	
	return (err == noErr);
}


//-------------------------------------------------------------------------------
// Seek
//-------------------------------------------------------------------------------

static bool Seek (short refNum, const Imf::Int64& pos)
{
	OSErr err = noErr;
	
	if (HaveHFSPlusAPIs())
	{
		err = FSSetForkPosition (refNum, fsFromStart, pos);
	}
	else
	{
		err = SetFPos (refNum, fsFromStart, pos);
	}
	
	return (err == noErr);
}


//-------------------------------------------------------------------------------
// GetSize
//-------------------------------------------------------------------------------

static bool GetSize (short refNum, Imf::Int64& size)
{
	OSErr err = noErr;
	
	if (HaveHFSPlusAPIs())
	{
		SInt64 fileSize;
		
		err  = FSGetForkSize (refNum, &fileSize);
		size = fileSize;
	}
	else
	{
		long fileSize;
		
		err  = GetEOF (refNum, &fileSize);
		size = fileSize;
	}
	
	return (err == noErr);
}

#endif

#pragma mark-
#pragma mark ===== Windows =====

// ===========================================================================
//	Windows IO Abstraction 
// ===========================================================================

#if MSWindows

//-------------------------------------------------------------------------------
// Read
//-------------------------------------------------------------------------------

static bool Read (short