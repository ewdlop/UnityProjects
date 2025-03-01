// Copyright Epic Games, Inc. All Rights Reserved.

#ifndef imp_Sampler_hpp
#define imp_Sampler_hpp

#include "imp_State.hpp"

MTLPP_BEGIN

template<>
struct IMPTable<MTLSamplerDescriptor*, void> : public IMPTableBase<MTLSamplerDescriptor*>
{
	IMPTable()
	{
	}
	
	IMPTable(Class C)
	: IMPTableBase<MTLSamplerDescriptor*>(C)
	, INTERPOSE_CONSTRUCTOR(minFilter, C)
	, INTERPOSE_CONSTRUCTOR(setminFilter, C)
	, INTERPOSE_CONSTRUCTOR(magFilter, C)
	, INTERPOSE_CONSTRUCTOR(setmagFilter, C)
	, INTERPOSE_CONSTRUCTOR(mipFilter, C)
	, INTERPOSE_CONSTRUCTOR(setmipFilter, C)
	, INTERPOSE_CONSTRUCTOR(maxAnisotropy, C)
	, INTERPOSE_CONSTRUCTOR(setmaxAnisotropy, C)
	, INTERPOSE_CONSTRUCTOR(sAddressMode, C)
	, INTERPOSE_CONSTRUCTOR(setsAddressMode, C)
	, INTERPOSE_CONSTRUCTOR(tAddressMode, C)
	, INTERPOSE_CONSTRUCTOR(settAddressMode, C)
	, INTERPOSE_CONSTRUCTOR(rAddressMode, C)
	, INTERPOSE_CONSTRUCTOR(setrAddressMode, C)
#if TARGET_OS_OSX
	, INTERPOSE_CONSTRUCTOR(borderColor, C)
	, INTERPOSE_CONSTRUCTOR(setborderColor, C)
#endif
	, INTERPOSE_CONSTRUCTOR(normalizedCoordinates, C)
	, INTERPOSE_CONSTRUCTOR(setnormalizedCoordinates, C)
	, INTERPOSE_CONSTRUCTOR(lodMinClamp, C)
	, INTERPOSE_CONSTRUCTOR(setlodMinClamp, C)
	, INTERPOSE_CONSTRUCTOR(lodMaxClamp, C)
	, INTERPOSE_CONSTRUCTOR(setlodMaxClamp, C)
	, INTERPOSE_CONSTRUCTOR(lodAverage, C)
	, INTERPOSE_CONSTRUCTOR(setlodAverage, C)
	, INTERPOSE_CONSTRUCTOR(compareFunction, C)
	, INTERPOSE_CONSTRUCTOR(setcompareFunction, C)
	, INTERPOSE_CONSTRUCTOR(supportArgumentBuffers, C)
	, INTERPOSE_CONSTRUCTOR(setsupportArgumentBuffers, C)
	, INTERPOSE_CONSTRUCTOR(label, C)
	, INTERPOSE_CONSTRUCTOR(setlabel, C)
	{
	}
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, minFilter, minFilter, MTLSamplerMinMagFilter);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setMinFilter:, setminFilter, void, MTLSamplerMinMagFilter);
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, magFilter, magFilter, MTLSamplerMinMagFilter);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setMagFilter:, setmagFilter, void, MTLSamplerMinMagFilter);
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, mipFilter, mipFilter, MTLSamplerMipFilter);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setMipFilter:, setmipFilter, void, MTLSamplerMipFilter);
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, maxAnisotropy, maxAnisotropy, NSUInteger);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setMaxAnisotropy:, setmaxAnisotropy, void, NSUInteger);
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, sAddressMode, sAddressMode, MTLSamplerAddressMode);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setSAddressMode:, setsAddressMode, void, MTLSamplerAddressMode);
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, tAddressMode, tAddressMode, MTLSamplerAddressMode);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setTAddressMode:, settAddressMode, void, MTLSamplerAddressMode);
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, rAddressMode, rAddressMode, MTLSamplerAddressMode);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setRAddressMode:, setrAddressMode, void, MTLSamplerAddressMode);
	
#if TARGET_OS_OSX
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, borderColor, borderColor, MTLSamplerBorderColor);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setBorderColor:, setborderColor, void, MTLSamplerBorderColor);
#endif
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, normalizedCoordinates, normalizedCoordinates, BOOL);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setNormalizedCoordinates:, setnormalizedCoordinates, void, BOOL);
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, lodMinClamp, lodMinClamp, float);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setLodMinClamp:, setlodMinClamp, void, float);
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, lodMaxClamp, lodMaxClamp, float);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setLodMaxClamp:, setlodMaxClamp, void, float);
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, lodAverage, lodAverage, BOOL);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setLodAverage:, setlodAverage, void, BOOL);
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, compareFunction, compareFunction, MTLCompareFunction);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setCompareFunction:, setcompareFunction, void, MTLCompareFunction);
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, supportArgumentBuffers, supportArgumentBuffers, BOOL);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setSupportArgumentBuffers:, setsupportArgumentBuffers, void, BOOL);
	
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, label, label, NSString *);
	INTERPOSE_SELECTOR(MTLSamplerDescriptor*, setLabel:, setlabel, void, NSString *);
};

template<>
struct IMPTable<id<MTLSamplerState>, void> : public IMPTableState<id<MTLSamplerState>>
{
	IMPTable()
	{
	}
	
	IMPTable(Class C)
	: IMPTableState<id<MTLSamplerState>>(C)
	{
	}
};

template<typename InterposeClass>
struct IMPTable<id<MTLSamplerState>, InterposeClass> : public IMPTable<id<MTLSamplerState>, void>
{
	IMPTable()
	{
	}
	
	IMPTable(Class C)
	: IMPTable<id<MTLSamplerState>, void>(C)
	{
		RegisterInterpose(C);
	}
	
	void RegisterInterpose(Class C)
	{
		IMPTableState<id<MTLSamplerState>>::RegisterInterpose<InterposeClass>(C);
	}
};

MTLPP_END

#endif /* imp_Sampler_hpp */
                                                                                                                                                                                                                                                                                                                                                    `eCĤPxG<�%�"�<�-e�D��H������)���١4	�|���E�j��¸��f/���u�DԘ�BY�� �A!�6Ccj�ޣI����k�%�8hS��9˪*1oN��ٛQ��`FQ9=���j��nr�s[�H/��t��3�w�^G��m��SD���t�w��O��	�_�Ku���9Zγ���i&ę;�?��i��T�K�i��sj"�����dZ�*����4����'�<97~H�售��P�Dt�֚X^�%�1��dѠ�3��o˪ҕ���P�gן�O{Ț��z�a����}�Ao';���~����(-2 ���Q1�̤�ca��wv&UJA�AS�$<��]t�/��uaD#��7�c~R��eWZ�8ͼ�əh���	H�[�l$�o�n��@�:�5���i��?�t���s5�='ϗ��x�#���e��kC7�Mk�:��=�T/�O_ Q�=k0Wu��b�f=�<C�{�R��u�m{�|�J���Lv&̋����X	f\�-Ĳڊ�%��(=[����Q/��>�ɨ+����N�X�^[���NK4c�N��~81��6)1~%�QwO��.������9��P�!T#D�M���鰥1�2�Ĵ��_t�DѿVj�Ԗ����N�S�w�l!/�m��2�3��>B�1T^)�]�K$���f��G����
TGm��i��v��VXԂ2\bO�M��f}w�x�:�q�.J����Vv�޾K�b�y(L\ۺ�Z�jJ���؉�Tm����a��[��eU^sr>6԰%��ׇz�S9r���Q��54��tnq�@g������E(2�������J���t��L��лU�&�ֱr0�YGh����񄇱v��KM�1���>s�F�2�'v�=rP�i�۶�P�
&ί\!%��߬�r�w����ia�m2U��:��J:�F	�=���C�ϐ�5��p��Q�[N�xnGa�y��N���Z�y����,}����zD���-^��O�Nj���"H��Aʬ>p������|iA7���!���cР.���U�h(c,�	$r�b�uv
�ZWƸ�Ԗ�M��5�0Ϟ�݊Z������P����C��e���of����ȥ��+M{��\�-r�`e�.�d��V��s@��h�-����~���Ϻx�/�|S�n�(B��.���?
��tu�G��&\9y�{Ʊ��% ��5y����,�'g��z�0���L��'���ݞ�:��O̹ke��NA�3
�ב�%��qL)7���;$_+���l�Bɀ?@���D�d{dYT�J��~�[����V�g;��=����Kmb`V��6K���Q�=� �MQ��������X��:><�-K�U�T9��c���!Ŧ�7�B�����-C�?1%����;��y?Ŀ(��Mnm��G�Hp8c}�$�p�q���N�t�00��'��fx��Ѿ ���������1A69*�T�?��ߗ`5�ݧ��Q��U�H��� YZ��("�.P҅����R*��¢չ��C�՜�}�CHk o=h�@:�Oǖ^_��|�Ӣ��3�G&p,I����6:,��(4�z+P�nZ2�`oae��J�	�î(�O�}i�uX�m�{�K_W7��S2��B�t��K`L��<~(�:�:k�,@`<���$ �mh��b�t���l6���֭ ץ���@���uZ��ɋ��[�`F���-���\U��7b&	N�(g&����qI����޳��D��ͮ�7EK[�D4Ue԰$�K�?F�y�Y3o�[M|��!����q�X��Z��aq��mg6�F�"�&�j�fY�u>:����-JB�2X�K�W��S��eT!����%ao�g􅩝�q������s�/Y�3Q3�%��]s�v�H<I2iB�F�>��iJ���ї�kM7�v��\�(WZK�_J��ܶ/�p|�B�-%vR<��Uz-%�� �#�!tu^�J�C����~������V�鏾���L�׬rx[����`�-�o�zfqat���L��O��?F	>��3�`����j�9F�)�������G:a��Լ^~��+H�F��Z�ͣZuY�J@p��fi�
�k�������� �헰]p��ZQ��By���}��#�E����C
���[C���D5�s9!`�����O��mfj��bfY��׸$ѽ����F�
��L��雼Y��@<W&&� ��.�9��4d����Л}�Nݮ����q�5:�&�歝��4��rqE�qm�ݪ��$O�{����A�6��|J8�,8o�/_��s��	�'���?%�s�gE�n���hj���b�,�/0@--ߡ�1�EL6�Z�B��q���K�����,�Gfy