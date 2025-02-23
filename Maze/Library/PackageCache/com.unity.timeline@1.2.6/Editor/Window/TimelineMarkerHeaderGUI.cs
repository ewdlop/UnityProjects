//////////////////////////////////////////////////////////////////////////
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

#include <ImfVersion.h>
#include <ImfTestFile.h>
#include <ImfStdIO.h>
#include <iostream>
#include <exception>
#include <stdio.h>
#include <assert.h>

#ifndef ILM_IMF_TEST_IMAGEDIR
    #define ILM_IMF_TEST_IMAGEDIR
#endif


using namespace Imf;
using namespace std;

namespace {

void
testFile1 (const char fileName[], bool isImfFile)
{
    cout << fileName << " " << flush;

    ifstream f (fileName, ios_base::binary);
    assert (!!f);

    char bytes[4];
    f.read (bytes, sizeof (bytes));

    assert (!!f && isImfFile == isImfMagic (bytes));

    cout << "is " << (isImfMagic (bytes)? "": "not ") << "an OpenEXR file\n";
}


void
testFile2 (const char fileName[], bool exists, bool exrFile, bool tiledFile)
{
    cout << fileName << " " << flush;

    bool exr, tiled;

    exr = isOpenExrFile (fileName, tiled);
    assert (exr == exrFile && tiled == tiledFile);

    exr = isOpenExrFile (fileName);
    assert (exr == exrFile);

    tiled = isTiledOpenExrFile (fileName);
    assert (tiled == tiledFile);

    if (exists)
    {
	StdIFStream is (fileName);

	exr = isOpenExrFile (is, tiled);
	assert (exr == exrFile && tiled == tiledFile);

	if (exr)
	    assert (is.tellg() == 0);

	exr = isOpenExrFile (is);
	assert (exr == exrFile);

	if (exr)
	    assert (is.tellg() == 0);

	tiled = isTiledOpenExrFile (is);
	assert (tiled == tiledFile);

	if (tiled)
	    assert (is.tellg() == 0);
    }

    cout << (exists? "exists": "does not exist") << ", " <<
	    (exrFile? "is an OpenEXR file": "is not an OpenEXR file") << ", " <<
	    (tiledFile? "is tiled": "is not tiled") << endl;
}

} // namespace


void
testMagic ()
{
    try
    {
	cout << "Testing magic number" << endl;

	testFile1 (ILM_IMF_TEST_IMAGEDIR "comp_none.exr", true);
	testFile1 (ILM_IMF_TEST_IMAGEDIR "invalid.exr", false);

	testFile2 (ILM_IMF_TEST_IMAGEDIR "tiled.exr", true, true, true);
	testFile2 (ILM_IMF_TEST_IMAGEDIR "comp_none.exr", true, true, false);
	testFile2 (ILM_IMF_TEST_IMAGEDIR "invalid.exr", true, false, false);
	testFile2 (ILM_IMF_TEST_IMAGEDIR "does_not_exist.exr", false, false, false);

	cout << "ok\n" << endl;
    }
    catch (const std::exception &e)
    {
	cerr << "ERROR -- caught exception: " << e.what() << endl;
	assert (false);
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                           �c�����~����龦��X��U��g<M ��� "o�1����so�19	֬���ϙWu~*����������Ź�É�&Z�U�A�W]u0�X���������/���
��ȿ Ä�R�$ʧ��ܽB*��D=�����fݣ>� ����GD	�m|�����@q�s^�q�n��Eԏ��@?5�E�pi���0@�F��!�?��Ψv� f�J��OJP��ߕ�)�_����+����É͎�h��!R��W��*㹢Ɉ<t�DDYcCk��Q�]tm)� ��8ҩ_{ǿ�GzY���4D��f-l�l�%kWI���pe}��)A
E�v���B^���*�t�R�X1��/R�-a5B�/��<��pf*�˽	�r�{c�k�z'Q� ��H��<��χ�&q�6�d�|�j_6�)�!^Yg�ht
�uz8�K��M5b��;˺��o�s����0~T풡��T�5�;���:h����9��c�������r�a���ږfRRfWlￅ�H�6Z��R�IC��\���>�/�<��s0��2K�e�w����!����%�����y�~�v�r����u��#��՘*�y��~��������E4U�R�^�]�Q�������AF������"���f�<�7��'�����/֢��BB���{�y�t�s;�d���g�?eQ�DB!�¬I$C������ГLѬ)��Ali5Um]�P�`�*�(=�8����	�×(��c�[KQ��/d�X�9魗X�%���~�n�:Q#u��ASb�7�=�!QԶ~r�HVA����-��2`񄓒���39�=������l���3��l�l]�f����[޾v�M�+�^u��<ONX��%䅾�#�ԃ��V�p���
��"�"8���E���K�`��z0_Yr���.�{��V8�M�	��g�CA9�O��Q��9�>��E�%6��3�������ofYpݍ�[��,M�8C65�55\��w���wTp=�v
���m?~���TH�@h쬭Զ��f�e�D��·gM�kB��4��9� &e�Ԃ]v~}�g��/<w�H< |ᚢTǯZ��[*P����'��M���7�#ٔ9/�Ͳ
{-���oXeV����b�(8'���9S��+ڞ�ȩ��d�Q��Q}�k���	�r�A@f���K� �V�N�I��s�4wB��'0�['�O��B�K�5ď�y���¯OՃ.Y�r���B4߭uCah6p8v^*ڝ�t������3����DM�4��HeY'����΃���G�.����Q4v}d�Q��̳�T�YmJ���m\�Pr&���(Zv��K����rUz��f�1��:%�<X�Sȍ��"�72��5�Y,�$Ъy��_*�S��)�o�s��U�l�	wˆ;��)��!�J.s��[�3�!s�sq�:�L�����a��|�<�U�fc��0�n}�<i1�����cF�*� �� ��	fP��0����<8_�{׌��$�B�� r���k�6�U�H���RxKra�������:I���3:T.L���ي��h�F+g=�N��T{�O���b!bK��=���/� rdx6�%P��G}���Vu��,"G�a��U�f�ӂЪP_����2/I,ePt��/��K��7�[�z��O�K(ɪ.�gL�޳��\���	��<t
����Dug^f&6t���E��;�ļ�;�3��x~.	�����tv?��6�4_���c��fzT(�R�B7U|(���7�lS�F�ې���3�9��/�DN.�B�Cߤ�f�s�d�R��g���^��͵�/m�X�����M�)���3����Mm��eR>�%^��}qm�6�4+��� :̘�hz+�cH�}"R���:�.��U��c��ʷ� 1�"	w�,.G�r�'��S�Eo��rM}��^Kl>�@p�6���D���E�-T�HX<v�E,�)¡�ΖC&F�PA�M�[�ƀA2��>3��D���4&�C;ۢ��T]М*���3�7���[+i )��,�vt	��i�C���[��)��{�M�����Z�#}5D��<������J<�#9���{nj��_t��<k?�M�b���1�d�k��h�Ȼ�l�ft�jfes����g�x�]ӕO�7��e�������xͰ��|����hb��]�3x�ܻy�ۘx`NeF��H� 8/َ�Kwd�240���VIU]zꠑ(�N诺hP����xn���#;2�l��S���ȎJ��� �(� ��KLq�i���:�H �3����->J	
x�JT��ԫyx��1�d���~z6�T1e,��C�HQK��a����)|g�3&���}�f�w��X!���1��W�S@,�H�>4�ކ����j�XD��{����'�cl7@3����D�0��:�_�?'+˨���9������t��cB$�sҶ���_��5��b����e���=	��j�t{�A�Y)*�;�e�^8Nt	D֒4�cV���rs؂0��^�^�ڙ(��3����+}��o������S��4:�����1��EI�_��cBHw�Ʈ�4�Q����:�6�(�Oِ\��<�