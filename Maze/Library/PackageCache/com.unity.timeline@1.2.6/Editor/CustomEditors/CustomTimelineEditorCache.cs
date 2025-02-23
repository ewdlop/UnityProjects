///////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2004, Industrial Light & Magic, a division of Lucas
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


//-----------------------------------------------------------------------------
//
//	Environment maps
//
//-----------------------------------------------------------------------------

#include "ImfEnvmap.h"
#include "ImathFun.h"
#include "ImfNamespace.h"

#include <algorithm>
#include <math.h>

using namespace std;
using namespace IMATH_NAMESPACE;


OPENEXR_IMF_INTERNAL_NAMESPACE_SOURCE_ENTER


namespace LatLongMap {

V2f	
latLong (const V3f &dir)
{
    float r = sqrt (dir.z * dir.z + dir.x * dir.x);

    float latitude = (r < abs (dir.y))?
			 acos (r / dir.length()) * sign (dir.y):
			 asin (dir.y / dir.length());

    float longitude = (dir.z == 0 && dir.x == 0)? 0: atan2 (dir.x, dir.z);

    return V2f (latitude, longitude);
}


V2f
latLong (const Box2i &dataWindow, const V2f &pixelPosition)
{
    float latitude, longitude;

    if (dataWindow.max.y > dataWindow.min.y)
    {
        latitude = -1 * float(M_PI) *
            ((pixelPosition.y  - dataWindow.min.y) /
            (dataWindow.max.y - dataWindow.min.y) - 0.5f);
    }
    else
    {
	latitude = 0;
    }

    if (dataWindow.max.x > dataWindow.min.x)
    {
	longitude = -2 * float(M_PI) *
		   ((pixelPosition.x  - dataWindow.min.x) /
		    (dataWindow.max.x - dataWindow.min.x) - 0.5f);
    }
    else
    {
	longitude = 0;
    }

    return V2f (latitude, longitude);
}


V2f
pixelPosition (const Box2i &dataWindow, const V2f &latLong)
{
    float x = latLong.y / (-2 * float(M_PI)) + 0.5f;
    float y = latLong.x / (-1 * float(M_PI)) + 0.5f;

    return V2f (x * (dataWindow.max.x - dataWindow.min.x) + dataWindow.min.x,
		y * (dataWindow.max.y - dataWindow.min.y) + dataWindow.min.y);
}


V2f
pixelPosition (const Box2i &dataWindow, const V3f &direction)
{
    return pixelPosition (dataWindow, latLong (direction));
}


V3f
direction (const Box2i &dataWindow, const V2f &pixelPosition)
{
    V2f ll = latLong (dataWindow, pixelPosition);

    return V3f (sin (ll.y) * cos (ll.x),
		sin (ll.x),
		cos (ll.y) * cos (ll.x));
}

} // namespace LatLongMap


namespace CubeMap {

int
sizeOfFace (const Box2i &dataWindow)
{
    return min ((dataWindow.max.x - dataWindow.min.x + 1),
		(dataWindow.max.y - dataWindow.min.y + 1) / 6);
}


Box2i
dataWindowForFace (CubeMapFace face, const Box2i &dataWindow)
{
    int sof = sizeOfFace (dataWindow);
    Box2i dwf;

    dwf.min.x = 0;
    dwf.min.y = int (face) * sof;

    dwf.max.x = dwf.min.x + sof - 1;
    dwf.max.y = dwf.min.y + sof - 1;

    return dwf;
}


V2f
pixelPosition (CubeMapFace face, const Box2i &dataWindow, V2f positionInFace)
{
    Box2i dwf = dataWindowForFace (face, dataWindow);
    V2f pos (0, 0);

    switch (face)
    {
      case CUBEFACE_POS_X:

	pos.x = dwf.min.x + positionInFace.y;
	pos.y = dwf.max.y - positionInFace.x;
	break;

      case CUBEFACE_NEG_X:

	pos.x = dwf.max.x - positionInFace.y;
	pos.y = dwf.max.y - positionInFace.x;
	break;

      case CUBEFACE_POS_Y:

	pos.x = dwf.min.x + positionInFace.x;
	pos.y = dwf.max.y - positionInFace.y;
	break;

      case CUBEFACE_NEG_Y:

	pos.x = dwf.min.x + positionInFace.x;
	pos.y = dwf.min.y + positionInFace.y;
	break;

      case CUBEFACE_POS_Z:

	pos.x = dwf.max.x - positionInFace.x;
	pos.y = dwf.max.y - positionInFace.y;
	break;

      case CUBEFACE_NEG_Z:

	pos.x = dwf.min.x + positionInFace.x;
	pos.y = dwf.max.y - positionInFace.y;
	break;
    }

    return pos;
}


void
faceAndPixelPosition (const V3f &direction,
		      const Box2i &dataWindow,
		      CubeMapFace &face,
		      V2f &pif)
{
    int sof = sizeOfFace (dataWin