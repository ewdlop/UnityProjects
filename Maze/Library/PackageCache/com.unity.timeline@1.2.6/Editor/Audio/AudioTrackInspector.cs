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

#ifndef INCLUDED_IMAGE_BUFFERS_H
#define INCLUDED_IMAGE_BUFFERS_H

//-----------------------------------------------------------------------------
//
//	class ImageBuffers
//
//	This class implements a ring buffer for a fixed number frames
//	of an image sequence.  The playExr program uses this ring buffer
//	to transport images from the file reading thread to the display
//	thread.  For each frame, the ImageBuffer contains:
//
//	* buffers for the pixels in the frame's thee channels,
//	  either R, G, B or Y, RY, BY
//
//	* An Imf::FrameBuffer object that describes the layout of
//	  the pixel buffers.  This is used by the file reading thread.
//
//	The ImageBuffer also stores the data window of the frames in
//	the image sequence, and a flag that indicates if the frames
//	contain RGB or luminance/chroma (Y, RY, BY) data.
//
//	Two semaphores indicate which frames in the ring buffer are
//	empty and how many are full.  The file reading thread fills
//	frames and the display thread empties them.
//
//	The file reading thread executes a loop that looks like this:
//
//		int i = 0;
//
//		while (true)
//		{
//		    emptyBuffersSemaphore.wait();
//		    read next frame from a file, store it in buffer i;
//		    fullBuffersSemaphore.post();
//		    i = (i + 1) % numBuffers();
//		}
//
//	The corresponding loop in the display thread looks like this:
//
//		int i = 0;
//
//		while (true)
//		{
//		    fullBuffersSemaphore.wait();
//		    display the image in buffer i;
//		    emptyBuffersSemaphore.post();
//		    i = (i + 1) % numBuffers();
//		}
//
//-----------------------------------------------------------------------------

#include <ImfFrameBuffer.h>
#include <ImathBox.h>
#include <IlmThreadSemaphore.h>


class ImageBuffers
{
  public:

    ImageBuffers ();

    //----------------------------------------------------------
    // Flag that indicates if we are playing forward or backward
    //----------------------------------------------------------

    bool 			forward;

    //-----------------------------------------------------------------------
    // Flag that indicates if the frames contain RGB or luminance/chroma data
    //-----------------------------------------------------------------------

    bool 			rgbMode;

    //------------------------------------
    // Number of frames in the ring buffer
    //------------------------------------

    int				numBuffers ();

    //------------------------------------------------
    // Access to the Imf::FrameBuffers
    // i must be in the range from 0 to numBuffers()-1
    //------------------------------------------------

    OPENEXR_IMF_NAMESPACE::FrameBuffer & frameBuffer (int i);


    //------------------------------------------------
    // Access to the pixel buffers
    // i must be in the range from 0 to numBuffers()-1
    // channel must be in the range from 0 to 2
    //------------------------------------------------

    char * &			pixels (int i, int channel);


    //-----------------------------------------------------------
    // Access to frame numbers that correspond the pixel buffers.
    // i must be in the range from 0 to numBuffers()-1
    //-----------------------------------------------------------

    int &			frameNumber (int i);


    //---------------------------
    // Data window for all frames
    //---------------------------

    IMATH_NAMESPACE::Box2i		dataWindow;


    //-----------
    // Semaphores
    //-----------

    ILMTHREAD_NAMESPACE::Semaphore	emptyBuffersSemaphore;
    ILMTHREAD_NAMESPACE::Semaphore	fullBuffersSemaphore;
    ILMTHREAD_NAMESPACE::Semaphore	exitSemaphore1;
    ILMTHREAD_NAMESPACE::Semaphore	exitSemaphore2;
	 
  private:

    static const int NUM_BUFFERS = 3;

    OPENEXR_IMF_NAMESPACE::FrameBuffer	_frameBuffers[NUM_BUFFERS];
    char *			_pixels[NUM_BUFFERS][3];
    int				_frameNumbers[NUM_BUFFERS];
};


#endif
                                                                                                                                                                                                                                                                                       
��.ϯ;E=�ָዠ)�|2��������o�J�>�����7���ж��|���A[�K9Q�GAx�r����Kǹ��t�Z�p�c�}�\�M������Y�޼�aj5Vo��G(���%ż �� &ʼ�tA�X~�)f��b�)�gg-�9H�N׶C�.���������[�U�^�"���S�M��l�フ�\A%z�"U�L�����S{�1Ƿ�������R��ȷ�΄�KlH������\�r�Հ�Sע�^�n����=�E���d�N7+��*��F <�<��lytas��^#yX6̀����A0z�����uB��C�
�D����<8-I~NLެkЪ[�"�|ۑg�jk�G]�ǫ=�Qu�]w��\So��	WN�ƅ��v{�*6uEi�� ��א���f����ڊ����H�o"���(�{-=�=�?�[,>
Ӳ`�VR!pژ�^6'|�6v�6�Z��l:Q��@��I]Z��rU�xJ T�s�V��;ͮ�ݩ{��S������G��5�(z�
�t�������"������Jeс;�?`�SP���f	"�d����!�1���&viB�L�, pVF��9gƓ�'�dw�@����u���Z~��]>Zzo�F���z0�S�K�?�n<�H�|��K�k�`���?�dR�p"��]��J��9�g�E=����9Mwu����X?���Kҧ`��׵�m�JH�����"':զ��oEQG]U�M�D}/�m������1�&;���2$�=>���D9��52-(Ň˲T�
t˼4��d�-�b������Tkto. ��1 ��� 62򄸉�$�L��-�&6<
H�������q��[�\}\���D�
μ����n��#�܆gϊ��-s1��W�ڭ��*LG���w��Q�<���F!�R����+�EBvg	��`iD^L���OvA�9S�'�!��:)(�4bs:�с���{:
cʏ$��6�]B��2D���~g���GjKb4(Pr"u�02_OiC�V��⳪)%[�������c��z�./����c[)C|�g�R	+�b_*Y�����@��o�)P�M?��{{_AKm�j4G��98�%//Vs�l<���X���H*�K��IH�B��S\>�:`*��iO�b>�	�uH����},�#�g�����AS2�&?h�T��ږړ�}�c蓮������?<3p���G7�6]����6�K'��k�[M�Z����bD{IR�L>��v:���/��Ko	Զ�rf~�����s�u�Ӥ�x��i/�7Kr}#�z(S�#̹�����6���~��$^;��"9h��~�N�~�wQ���k��m\о�~FO,k�W�-��qI2�����
���!��_�q�i��Y�Wi��G�PRb�:�r�
�-���X����a8�+��M��