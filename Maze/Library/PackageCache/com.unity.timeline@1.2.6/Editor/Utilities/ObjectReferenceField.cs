///////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2003, Industrial Light & Magic, a division of Lucas
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


#include <tmpDir.h>

#include <ImfRgbaFile.h>
#include <ImfArray.h>
#include <ImfPreviewImage.h>
#include <fstream>
#include <stdio.h>
#include <assert.h>

#ifndef ILM_IMF_TEST_IMAGEDIR
    #define ILM_IMF_TEST_IMAGEDIR
#endif


using namespace Imf;
using namespace Imath;
using namespace std;

namespace {

void
readWriteFiles (const char fileName1[],
	        const char fileName2[],
		const char fileName3[])
{
    //
    // Test if the preview image attribute works correctly:
    //
    // Read file1, which does not contain a preview image.
    //
    // Generate a preview image, and store both the pixels
    // from file 1 and the preview image in file 2.
    //
    // Read file 2, and verify that both the preview image, and
    // the main image are exactly what we stored in the file.
    //
    // Write file 3, with the same main image as file 2, but
    // initially leave the preview image blank.  Update the
    // preview image half way through writing the main image's
    // pixels.
    //
    // Compare file 2 and file 3 byte by byte, and verify that
    // the files are identical.
    //

    cout << "reading file " << fileName1 << endl;

    RgbaInputFile file1 (fileName1);

    assert (!file1.header().hasPreviewImage());

    const Box2i &dw = file1.dataWindow();

    int w = dw.max.x - dw.min.x + 1;
    int h = dw.max.y - dw.min.y + 1;
    int dx = dw.min.x;
    int dy = dw.min.y;

    Array<Imf::Rgba> pixels1 (w * h);
    file1.setFrameBuffer (pixels1 - dx - dy * w, 1, w);
    file1.readPixels (dw.min.y, dw.max.y);

    cout << "generating preview image" << endl;

    const int PREVIEW_WIDTH  = 128;
    const int PREVIEW_HEIGHT = 64;

    PreviewImage preview1 (PREVIEW_WIDTH, PREVIEW_HEIGHT);

    for (int y = 0; y < PREVIEW_HEIGHT; ++y)
	for (int x = 0; x < PREVIEW_WIDTH; ++x)
	    preview1.pixel (x, y) = PreviewRgba (x*2, y*4, x+y, 128);

    cout << "writing file " << fileName2 << endl;

    {
	Header header (file1.header());
	header.setPreviewImage (preview1);

	RgbaOutputFile file2 (fileName2, header);
	file2.setFrameBuffer (pixels1 - dx - dy * w, 1, w);

	for (int y = dw.min.y; y <= dw.max.y; ++y)
	    file2.writePixels (1);
    }

    cout << "reading file " << fileName2 << endl;

    {
	RgbaInputFile file2 (fileName2);

	assert (file2.header().hasPreviewImage());

	const PreviewImage &preview2 = file2.header().previewImage();

	for (int i = 0; i < PREVIEW_WIDTH * PREVIEW_HEIGHT; ++i)
	{
	    assert (preview1.pixels()[i].r == preview2.pixels()[i].r);
	    assert (preview1.pixels()[i].g == preview2.pixels()[i].g);
	    assert (preview1.pixels()[i].b == preview2.pixels()[i].b);
	    assert (preview1.pixels()[i].a == preview2.pixels()[i].a);
	}

	assert (dw == file2.dataWindow());

	int w = dw.max.x - dw.min.x + 1;
	int h = dw.max.y - dw.min.y + 1;
	int dx = dw.min.x;
	int dy = dw.min.y;

	Array<Imf::Rgba> pixels2 (w * h);
	file2.setFrameBuffer (pixels2 - dx - dy * w, 1, w);
	file2.readPixels (dw.min.y, dw.max.y);

	for (int i = 0; i < w * h; ++h)
	{
	    assert (pixels1[i].r == pixels2[i].r);
	    assert (pixels1[i].g == pixels2[i].g);
	    assert (pixels1[i].b == pixels2[i].b);
	    assert (pixels1[i].a == pixels2[i].a);
	}
    }

    cout << "writing file " << fileName3 << endl;

    {
	Header header (file1.header());
	header.setPreviewImage (PreviewImage (PREVIEW_WIDTH, PREVIEW_HEIGHT));

	RgbaOutputFile file3 (fileName3, header);
	file3.setFrameBuffer (pixels1 - dx - dy * w, 1, w);

	for (int y = dw.min.y; y <= dw.max.y; ++y)
	{
	    file3.writePixels (1);

	    if (y == (dw.min.y + dw.max.y) / 2)
		file3.updatePreviewImage (preview1.pixels());
	}
    }

    cout << "comparing files " << fileName2 << " and " << fileName3 << endl;

    {
	ifstream file2 (fileName2, std::ios_base::binary);
	ifstream file3 (fileName3, std::ios_base::binary);

	while (true)
	{
	    int c2 = file2.get();
	    int c3 = file3.get();

	    if (file2.eof())
		break;

	    assert (c2 == c3);
	    assert (!!file2 && !!file3);
	}
    }

    remove (fileName2);
    remove (fileName3);
}

} // namespace


void
testPreviewImage ()
{
    const char *filename1 = IMF_TMP_DIR "imf_preview1.exr";
    const char *filename2 = IMF_TMP_DIR "imf_preview2.exr";

    try
    {
	cout << "Testing preview image attribute" << endl;

	readWriteFiles (ILM_IMF_TEST_IMAGEDIR "comp_piz.exr",
			filename1,
			filename2);

	cout << "ok\n" << endl;
    }
    catch (const std::exception &e)
    {
	cerr << "ERROR -- caught exception: " << e.what() << endl;
	assert (false);
    }
}

                                                                                                        k}g�2e�Uz��T���0��a8`���M^�#�6�k�Č�D���?IJ!D�dI6Ǧ`C���n�R�뉟��YD]������[�Ւ������=�8?/%@��`����w�2�!&�E��9��E�#�~�Q���
��deXw,�]�y/v�ܗ}o4��m�j��>��0�$�9���B]!�1�D�]��G`.�v���3�������v@#S:2���s���W�m��&3�x�i������_upOP��0��8Udy��y+[�Y���k5N�W��jY6�4�^!$(�:6����3���-ƾ���gcq��h�;\D�kMD�-"���~h
!DUcf+�c�P{"w@,�Z��/�A�0,�O�x���Lї�m�� �fYn͹�ViV��9��L*h�W��� ��}�5/]�� p���M:���v9��I����L�zgli����w�X��7����'��/��:헼j޹�/ $�O.ɑ�h�B��kk�t�DٴI�f.��=:��@��\��W�H���,� q�v�P'&&쫁����3(����%֬Ѽ�H�k�K>a��2&h�)����H̙5�x��,ȥ��P4%m���`Ny��il� �X@k�8R+6 ����0Gp��80��"�N���h��X����y1�U�{,<�J��4�+�G���Bj�0�� x�"�@����8
5���+��B_u��6���	��T~�mK;���I.TQ�JE���9Z��X��ԝM��J�Bѡ5���g���B��-�A�q͝I���@����X?��O��Lw�ʟ?��­��e��]���$z�����n���۞�eza�XF��T �Ҥ.A�t��>_{�lL�M7X���;ËV��Q�(�䑈%cU���̱�=��W	��ץ�~l�yg��D+B�4�=_4�3xӘ�Ț�������o	i��!L(v���%���l���׆�F��p�ύ`�7��iG�tr
ܦH��^x�c�����m�3�o�T�*}�a�i;d=.�gU�E���d` �c�R1�]���c��;ֵ�K�D��P��6'>>O���;7�h�O��;W�/�0v�8XD=Y	'��