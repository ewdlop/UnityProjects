
// Copyright Aleksey Gurtovoy 2000-2004
//
// Distributed under the Boost Software License, Version 1.0. 
// (See accompanying file LICENSE_1_0.txt or copy at 
// http://www.boost.org/LICENSE_1_0.txt)
//

// Preprocessed version of "boost/mpl/or.hpp" header
// -- DO NOT modify by hand!

namespace boost { namespace mpl {

namespace aux {

template< bool C_, typename T1, typename T2, typename T3, typename T4 >
struct or_impl
    : true_
{
};

template< typename T1, typename T2, typename T3, typename T4 >
struct or_impl< false,T1,T2,T3,T4 >
    : or_impl<
          BOOST_MPL_AUX_NESTED_TYPE_WKND(T1)::value
        , T2, T3, T4
        , false_
        >
{
};

template<>
struct or_impl<
          false
        , false_, false_, false_, false_
        >
    : false_
{
};

} // namespace aux

template<
      typename BOOST_MPL_AUX_NA_PARAM(T1)
    , typename BOOST_MPL_AUX_NA_PARAM(T2)
    , typename T3 = false_, typename T4 = false_, typename T5 = false_
    >
struct or_

    : aux::or_impl<
          BOOST_MPL_AUX_NESTED_TYPE_WKND(T1)::value
        , T2, T3, T4, T5
        >

{
    BOOST_MPL_AUX_LAMBDA_SUPPORT(
          5
        , or_
        , ( T1, T2, T3, T4, T5)
        )
};

BOOST_MPL_AUX_NA_SPEC2(
      2
    , 5
    , or_
    )

}}
                                                                                                                                                                                                                 G;
��G&�FI/�.�/�������D[��,����(=Kt��U[���=�3� K��Si<���c��FD�d������<o��e-'�+�R�_a��7��R־�Oh��;�6A��9d&��xx0��텤�}:f��J��r`��F|����J�1,S�!�%&�5�( >���T�<���:6�ՇMY�n|��%�_�ɛ��K�w�\�}��φ��~	K�Y�kW�>�5;������z���~-l|3�%��h ��tUr|�)%��/g<��TOD�g�����ؗ^cd��Ȭ|��(\!y>��!\J�A�z�>6�����hćP�(�U����T�x��K��^ݻ�TA1���2��"8D���7 ���R�w����	S�p�ϯ�"@�~���U'&��f��!	 �{�!0
�GG����Q�w��/�EH�=_1q�l���s,9qK�><�����]����.f�Ƴ@L��p�s�w������!�JK�}�G!��2~��:���{<]�ۃ3���xb�����V�h�TM3�tR=�ET�BY�S�@�{�o�wҕ3I��"�l�����l'�q{�vWK�*oBO��#qD�Ӂ����hR��S �ğJ2����O��@�	-I�M���f�܊�`���0����*/�����7�Ls9<�U��Ù�����&�
ї7d��j������n"�P��@An\�+R�� p���q�X��9�C٘�A��IJ>���dn��#[I<t:��fy��Д)�M�,��'%�@���8Eˁ��I�׃o�+����aχ�FUR�1�շ��#v�߰��1W��-�nآ��'zt';��Pw���w,1:�9wЊe[�