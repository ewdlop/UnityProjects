------------------------------------------------------------------------
-- ddMax.decTest -- decDouble maxnum                                  --
-- Copyright (c) IBM Corporation, 1981, 2008.  All rights reserved.   --
------------------------------------------------------------------------
-- Please see the document "General Decimal Arithmetic Testcases"     --
-- at http://www2.hursley.ibm.com/decimal for the description of      --
-- these testcases.                                                   --
--                                                                    --
-- These testcases are experimental ('beta' versions), and they       --
-- may contain errors.  They are offered on an as-is basis.  In       --
-- particular, achieving the same results as the tests here is not    --
-- a guarantee that an implementation complies with any Standard      --
-- or specification.  The tests are not exhaustive.                   --
--                                                                    --
-- Please send comments, suggestions, and corrections to the author:  --
--   Mike Cowlishaw, IBM Fellow                                       --
--   IBM UK, PO Box 31, Birmingham Road, Warwick CV34 5JL, UK         --
--   mfc@uk.ibm.com                                                   --
------------------------------------------------------------------------
version: 2.59

-- we assume that base comparison is tested in compare.decTest, so
-- these mainly cover special cases and rounding
precision:   16
maxExponent: 384
minExponent: -383
extended:    1
clamp:       1
rounding:    half_even

-- sanity checks
ddmax001 max  -2  -2  -> -2
ddmax002 max  -2  -1  -> -1
ddmax003 max  -2   0  ->  0
ddmax004 max  -2   1  ->  1
ddmax005 max  -2   2  ->  2
ddmax006 max  -1  -2  -> -1
ddmax007 max  -1  -1  -> -1
ddmax008 max  -1   0  ->  0
ddmax009 max  -1   1  ->  1
ddmax010 max  -1   2  ->  2
ddmax011 max   0  -2  ->  0
ddmax012 max   0  -1  ->  0
ddmax013 max   0   0  ->  0
ddmax014 max   0   1  ->  1
ddmax015 max   0   2  ->  2
ddmax016 max   1  -2  ->  1
ddmax017 max   1  -1  ->  1
ddmax018 max   1   0  ->  1
ddmax019 max   1   1  ->  1
ddmax020 max   1   2  ->  2
ddmax021 max   2  -2  ->  2
ddmax022 max   2  -1  ->  2
ddmax023 max   2   0  ->  2
ddmax025 max   2   1  ->  2
ddmax026 max   2   2  ->  2

-- extended zeros
ddmax030 max   0     0   ->  0
ddmax031 max   0    -0   ->  0
ddmax032 max   0    -0.0 ->  0
ddmax033 max   0     0.0 ->  0
ddmax034 max  -0     0   ->  0    -- note: -0 = 0, but 0 chosen
ddmax035 max  -0    -0   -> -0
ddmax036 max  -0    -0.0 -> -0.0
ddmax037 max  -0     0.0 ->  0.0
ddmax038 max   0.0   0   ->  0
ddmax039 max   0.0  -0   ->  0.0
ddmax040 max   0.0  -0.0 ->  0.0
ddmax041 max   0.0   0.0 ->  0.0
ddmax042 max  -0.0   0   ->  0
ddmax043 max  -0.0  -0   -> -0.0
ddmax044 max  -0.0  -0.0 -> -0.0
ddmax045 max  -0.0   0.0 ->  0.0

ddmax050 max  -0E1   0E1 ->  0E+1
ddmax051 max  -0E2   0E2 ->  0E+2
ddmax052 max  -0E2   0E1 ->  0E+1
ddmax053 max  -0E1   0E2 ->  0E+2
ddmax054 max   0E1  -0E1 ->  0E+1
ddmax055 max   0E2  -0E2 ->  0E+2
ddmax056 max   0E2  -0E1 ->  0E+2
ddmax057 max   0E1  -0E2 ->  0E+1

ddmax058 max   0E1   0E1 ->  0E+1
ddmax059 max   0E2   0E2 ->  0E+2
ddmax060 max   0E2   0E1 ->  0E+2
ddmax061 max   0E1   0E2 ->  0E+2
ddmax062 max  -0E1  -0E1 -> -0E+1
ddmax063 max  -0E2  -0E2 -> -0E+2
ddmax064 max  -0E2  -0E1 -> -0E+1
ddmax065 max  -0E1  -0E2 -> -0E+1

-- Specials
ddmax090 max  Inf  -Inf   ->  Infinity
ddmax091 max  Inf  -1000  ->  Infinity
ddmax092 max  Inf  -1     ->  Infinity
ddmax093 max  Inf  -0     ->  Infinity
ddmax094 max  Inf   0     ->  Infinity
ddmax095 max  Inf   1     ->  Infinity
ddmax096 max  Inf   1000  ->  Infinity
ddmax097 max  Inf   Inf   ->  Infinity
ddmax098 max -1000  Inf   ->  Infinity
ddmax099 max -Inf   Inf   ->  Infinity
ddmax100 max -1     Inf   ->  Infinity
ddmax101 max -0     Inf   ->  Infinity
ddmax102 max  0     Inf   ->  Infinity
ddmax103 max  1     Inf   ->  Infinity
ddmax104 max  1000  Inf   ->  Infinity
ddmax105 max  Inf   Inf   ->  Infinity

ddmax120 max -Inf  -Inf   -> -Infinity
ddmax121 max -Inf  -1000  -> -1000
ddmax122 max -Inf  -1     -> -1
ddmax123 max -Inf  -0     -> -0
ddmax124 max -Inf   0     ->  0
ddmax125 max -Inf   1     ->  1
ddmax126 max -Inf   1000  ->  1000
ddmax127 max -Inf   Inf   ->  Infinity
ddmax128 max -Inf  -Inf   ->  -Infinity
ddmax129 max -1000 -Inf   ->  -1000
ddmax130 max -1    -Inf   ->  -1
ddmax131 max -0    -Inf   ->  -0
ddmax132 max  0    -Inf   ->  0
ddmax133 max  1    -Inf   ->  1
ddmax134 max  1000 -Inf   ->  1000
ddmax135 max  Inf  -Inf   ->  Infinity

-- 2004.08.02 754r chooses number over NaN in mixed cases
ddmax141 max  NaN -Inf    -> -Infinity
ddmax142 max  NaN -1000   -> -1000
ddmax143 max  NaN -1      -> -1
ddmax144 max  NaN -0      -> -0
ddmax145 max  NaN  0      ->  0
ddmax146 max  NaN  1      ->  1
ddmax147 max  NaN  1000   ->  1000
ddmax148 max  NaN  Inf    ->  Infinity
ddmax149 max  NaN  NaN    ->  NaN
ddmax150 max -Inf  NaN    -> -Infinity
ddmax151 max -1000 NaN    -> -1000
ddmax152 max -1    NaN    -> -1
ddmax153 max -0    NaN    -> -0
ddmax154 max  0    NaN    ->  0
ddmax155 max  1    NaN    ->  1
ddmax156 max  1000 NaN    ->  1000
ddmax157 max  Inf  NaN    ->  Infinity

ddmax161 max  sNaN -Inf   ->  NaN  Invalid_operation
ddmax162 max  sNaN -1000  ->  NaN  Invalid_operation
ddmax163 max  sNaN -1     ->  NaN  Invalid_operation
ddmax164 max  sNaN -0     ->  NaN  Invalid_operation
ddmax165 max  sNaN  0     ->  NaN  Invalid_operation
ddmax166 max  sNaN  1     ->  NaN  Invalid_operation
ddmax167 max  sNaN  1000  ->  NaN  Invalid_operation
ddmax168 max  sNaN  NaN   ->  NaN  Invalid_operation
ddmax169 max  sNaN sNaN   ->  NaN  Invalid_operation
ddmax170 max  NaN  sNaN   ->  NaN  Invalid_operation
ddmax171 max -Inf  sNaN   ->  NaN  Invalid_operation
ddmax172 max -1000 sNaN   ->  NaN  Invalid_operation
ddmax173 max -1    sNaN   ->  NaN  Invalid_operation
ddmax174 max -0    sNaN   ->  NaN  Invalid_operation
ddmax175 max  0    sNaN   ->  NaN  Invalid_operation
ddmax176 max  1    sNaN   ->  NaN  Invalid_operation
ddmax177 max  1000 sNaN   ->  NaN  Invalid_operation
ddmax178 max  Inf  sNaN   ->  NaN  Invalid_operation
ddmax179 max  NaN  sNaN   ->  NaN  Invalid_operation

-- propagating NaNs
ddmax181 max  NaN9  -Inf   -> -Infinity
ddmax182 max  NaN8     9   ->  9
ddmax183 max -NaN7   Inf   ->  Infinity

ddmax184 max -NaN1   NaN11 -> -NaN1
ddmax185 max  NaN2   NaN12 ->  NaN2
ddmax186 max -NaN13 -NaN7  -> -NaN13
ddmax187 max  NaN14 -NaN5  ->  NaN14

ddmax188 max -Inf    NaN4  -> -Infinity
ddmax189 max -9     -NaN3  -> -9
ddmax190 max  Inf    NaN2  ->  Infinity

ddmax191 max  sNaN99 -Inf    ->  NaN99 Invalid_operation
ddmax192 max  sNaN98 -1      ->  NaN98 Invalid_operation
ddmax193 max -sNaN97  NaN    -> -NaN97 Invalid_operation
ddmax194 max  sNaN96 sNaN94  ->  NaN96 Invalid_operation
ddmax195 max  NaN95  sNaN93  ->  NaN93 Invalid_operation
ddmax196 max -Inf    sNaN92  ->  NaN92 Invalid_operation
ddmax197 max  0      sNaN91  ->  NaN91 Invalid_operation
ddmax198 max  Inf   -sNaN90  -> -NaN90 Invalid_operation
ddmax199 max  NaN    sNaN89  ->  NaN89 Invalid_operation

-- old rounding checks
ddmax221 max 12345678000 1  -> 12345678000
ddmax222 max 1 12345678000  -> 12345678000
ddmax223 max 1234567800  1  -> 1234567800
ddmax224 max 1 1234567800   -> 1234567800
ddmax225 max 1234567890  1  -> 1234567890
ddmax226 max 1 1234567890   -> 1234567890
ddmax227 max 1234567891  1  -> 1234567891
ddmax228 max 1 1234567891   -> 1234567891
ddmax229 max 12345678901 1  -> 12345678901
ddmax230 max 1 12345678901  -> 12345678901
ddmax231 max 1234567896  1  -> 1234567896
ddmax232 max 1 1234567896   -> 1234567896
ddmax233 max -1234567891  1 -> 1
ddmax234 max 1 -1234567891  -> 1
ddmax235 max -12345678901 1 -> 1
ddmax236 max 1 -12345678901 -> 1
ddmax237 max -1234567896  1 -> 1
ddmax238 max 1 -1234567896  -> 1

-- from examples
ddmax280 max '3'   '2'  ->  '3'
ddmax281 max '-10' '3'  ->  '3'
ddmax282 max '1.0' '1'  ->  '1'
ddmax283 max '1' '1.0'  ->  '1'
ddmax284 max '7' 'NaN'  ->  '7'

-- expanded list from min/max 754r purple prose
-- [explicit tests for exponent ordering]
ddmax401 max  Inf    1.1     ->  Infinity
ddmax402 max  1.1    1       ->  1.1
ddmax403 max  1      1.0     ->  1
ddmax404 max  1.0    0.1     ->  1.0
ddmax405 max  0.1    0.10    ->  0.1
ddmax406 max  0.10   0.100   ->  0.10
ddmax407 max  0.10   0       ->  0.10
ddmax408 max  0      0.0     ->  0
ddmax409 max  0.0   -0       ->  0.0
ddmax410 max  0.0   -0.0     ->  0.0
ddmax411 max  0.00  -0.0     ->  0.00
ddmax412 max  0.0   -0.00    ->  0.0
ddmax413 max  0     -0.0     ->  0
ddmax414 max  0     -0       ->  0
ddmax415 max -0.0   -0       -> -0.0
ddmax416 max -0     -0.100   -> -0
ddmax417 max -0.100 -0.10    -> -0.100
ddmax418 max -0.10  -0.1     -> -0.10
ddmax419 max -0.1   -1.0     -> -0.1
ddmax420 max -1.0   -1       -> -1.0
ddmax421 max -1     -1.1     -> -1
ddmax423 max -1.1   -Inf     -> -1.1
-- same with operands reversed
ddmax431 max  1.1    Inf     ->  Infinity
ddmax432 max  1      1.1     ->  1.1
ddmax433 max  1.0    1       ->  1
ddmax434 max  0.1    1.0     ->  1.0
ddmax435 max  0.10   0.1     ->  0.1
ddmax436 max  0.100  0.10    ->  0.10
ddmax437 max  0      0.10    ->  0.10
ddmax438 max  0.0    0       ->  0
ddmax439 max -0      0.0     ->  0.0
ddmax440 max -0.0    0.0     ->  0.0
ddmax441 max -0.0    0.00    ->  0.00
ddmax442 max -0.00   0.0     ->  0.0
ddmax443 max -0.0    0       ->  0
ddmax444 max -0      0       ->  0
ddmax445 max -0     -0.0     -> -0.0
ddmax446 max -0.100 -0       -> -0
ddmax447 max -0.10  -0.100   -> -0.100
ddmax448 max -0.1   -0.10    -> -0.10
ddmax449 max -1.0   -0.1     -> -0.1
ddmax450 max -1     -1.0     -> -1.0
ddmax451 max -1.1   -1       -> -1
ddmax453 max -Inf   -1.1     -> -1.1
-- largies
ddmax460 max  1000   1E+3    ->  1E+3
ddmax461 max  1E+3   1000    ->  1E+3
ddmax462 max  1000  -1E+3    ->  1000
ddmax463 max  1E+3  -1000    ->  1E+3
ddmax464 max -1000   1E+3    ->  1E+3
ddmax465 max -1E+3   1000    ->  1000
ddmax466 max -1000  -1E+3    -> -1000
ddmax467 max -1E+3  -1000    -> -1000

-- misalignment traps for little-endian
ddmax471 max      1.0       0.1  -> 1.0
ddmax472 max      0.1       1.0  -> 1.0
ddmax473 max     10.0       0.1  -> 10.0
ddmax474 max      0.1      10.0  -> 10.0
ddmax475 max      100       1.0  -> 100
ddmax476 max      1.0       100  -> 100
ddmax477 max     1000      10.0  -> 1000
ddmax478 max     10.0      1000  -> 1000
ddmax479 max    10000     100.0  -> 10000
ddmax480 max    100.0     10000  -> 10000
ddmax481 max   100000    1000.0  -> 100000
ddmax482 max   1000.0    100000  -> 100000
ddmax483 max  1000000   10000.0  -> 1000000
ddmax484 max  10000.0   1000000  -> 1000000

-- subnormals
ddmax510 max  1.00E-383       0  ->   1.00E-383
ddmax511 max  0.1E-383        0  ->   1E-384    Subnormal
ddmax512 max  0.10E-383       0  ->   1.0E-384  Subnormal
ddmax513 max  0.100E-383      0  ->   1.00E-384 Subnormal
ddmax514 max  0.01E-383       0  ->   1E-385    Subnormal
ddmax515 max  0.999E-383      0  ->   9.99E-384 Subnormal
ddmax516 max  0.099E-383      0  ->   9.9E-385  Subnormal
ddmax517 max  0.009E-383      0  ->   9E-386    Subnormal
ddmax518 max  0.001E-383      0  ->   1E-386    Subnormal
ddmax519 max  0.0009E-383     0  ->   9E-387    Subnormal
ddmax520 max  0.0001E-383     0  ->   1E-387    Subnormal

ddmax530 max -1.00E-383       0  ->   0
ddmax531 max -0.1E-383        0  ->   0
ddmax532 max -0.10E-383       0  ->   0
ddmax533 max -0.100E-383      0  ->   0
ddmax534 max -0.01E-383       0  ->   0
ddmax535 max -0.999E-383      0  ->   0
ddmax536 max -0.099E-383      0  ->   0
ddmax537 max -0.009E-383      0  ->   0
ddmax538 max -0.001E-383      0  ->   0
ddmax539 max -0.0009E-383     0  ->   0
ddmax540 max -0.0001E-383     0  ->   0

-- Null tests
ddmax900 max 10  #  -> NaN Invalid_operation
ddmax901 max  # 10  -> NaN Invalid_operation



                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      �>�p	�K�\̌�}/3w���b�=��5-�ǯ�`�FPP�Ӥ+�尋�%䊢zk)�5~xf���-�C JR�e�&��U�쭓���12��s'�<�m5��Ć�����fp3E;G�,�������\T��g�o�DN�	��3������n���a��_w*����Ql�X��v�_p O��f��Dy��H��R�m_�֗v&�Z�Ɩ_� ޸x�d6���=6����R�zz���T��/,��X�o^�_�Ms	��0D�E���t��0�����7��2Ȳ�-M)�Q�rC�;�S@��?PK?��uE�Q�	߀ѷ�l�V� �>�`\C�q�/g��DZ�����27���M�~��������J��m����i"�pv�@I�Ӏ��