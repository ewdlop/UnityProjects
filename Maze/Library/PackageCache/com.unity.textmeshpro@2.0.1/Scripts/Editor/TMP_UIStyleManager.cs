------------------------------------------------------------------------
-- dqNextMinus.decTest -- decQuad next that is less [754r nextdown]   --
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

-- All operands and results are decQuads.
extended:    1
clamp:       1
precision:   34
maxExponent: 6144
minExponent: -6143
rounding:    half_even

dqnextm001 nextminus  0.9999999999999999999999999999999995 ->   0.9999999999999999999999999999999994
dqnextm002 nextminus  0.9999999999999999999999999999999996 ->   0.9999999999999999999999999999999995
dqnextm003 nextminus  0.9999999999999999999999999999999997 ->   0.9999999999999999999999999999999996
dqnextm004 nextminus  0.9999999999999999999999999999999998 ->   0.9999999999999999999999999999999997
dqnextm005 nextminus  0.9999999999999999999999999999999999 ->   0.9999999999999999999999999999999998
dqnextm006 nextminus  1.000000000000000000000000000000000  ->   0.9999999999999999999999999999999999
dqnextm007 nextminus  1.0         ->   0.9999999999999999999999999999999999
dqnextm008 nextminus  1           ->   0.9999999999999999999999999999999999
dqnextm009 nextminus  1.000000000000000000000000000000001  ->   1.000000000000000000000000000000000
dqnextm010 nextminus  1.000000000000000000000000000000002  ->   1.000000000000000000000000000000001
dqnextm011 nextminus  1.000000000000000000000000000000003  ->   1.000000000000000000000000000000002
dqnextm012 nextminus  1.000000000000000000000000000000004  ->   1.000000000000000000000000000000003
dqnextm013 nextminus  1.000000000000000000000000000000005  ->   1.000000000000000000000000000000004
dqnextm014 nextminus  1.000000000000000000000000000000006  ->   1.000000000000000000000000000000005
dqnextm015 nextminus  1.000000000000000000000000000000007  ->   1.000000000000000000000000000000006
dqnextm016 nextminus  1.000000000000000000000000000000008  ->   1.000000000000000000000000000000007
dqnextm017 nextminus  1.000000000000000000000000000000009  ->   1.000000000000000000000000000000008
dqnextm018 nextminus  1.000000000000000000000000000000010  ->   1.000000000000000000000000000000009
dqnextm019 nextminus  1.000000000000000000000000000000011  ->   1.000000000000000000000000000000010
dqnextm020 nextminus  1.000000000000000000000000000000012  ->   1.000000000000000000000000000000011

dqnextm021 nextminus -0.9999999999999999999999999999999995 ->  -0.9999999999999999999999999999999996
dqnextm022 nextminus -0.9999999999999999999999999999999996 ->  -0.9999999999999999999999999999999997
dqnextm023 nextminus -0.9999999999999999999999999999999997 ->  -0.9999999999999999999999999999999998
dqnextm024 nextminus -0.9999999999999999999999999999999998 ->  -0.9999999999999999999999999999999999
dqnextm025 nextminus -0.9999999999999999999999999999999999 ->  -1.000000000000000000000000000000000
dqnextm026 nextminus -1.000000000000000000000000000000000  ->  -1.000000000000000000000000000000001
dqnextm027 nextminus -1.0         ->  -1.000000000000000000000000000000001
dqnextm028 nextminus -1           ->  -1.000000000000000000000000000000001
dqnextm029 nextminus -1.000000000000000000000000000000001  ->  -1.000000000000000000000000000000002
dqnextm030 nextminus -1.000000000000000000000000000000002  ->  -1.000000000000000000000000000000003
dqnextm031 nextminus -1.000000000000000000000000000000003  ->  -1.000000000000000000000000000000004
dqnextm032 nextminus -1.000000000000000000000000000000004  ->  -1.000000000000000000000000000000005
dqnextm033 nextminus -1.000000000000000000000000000000005  ->  -1.000000000000000000000000000000006
dqnextm034 nextminus -1.000000000000000000000000000000006  ->  -1.000000000000000000000000000000007
dqnextm035 nextminus -1.000000000000000000000000000000007  ->  -1.000000000000000000000000000000008
dqnextm036 nextminus -1.000000000000000000000000000000008  ->  -1.000000000000000000000000000000009
dqnextm037 nextminus -1.000000000000000000000000000000009  ->  -1.000000000000000000000000000000010
dqnextm038 nextminus -1.000000000000000000000000000000010  ->  -1.000000000000000000000000000000011
dqnextm039 nextminus -1.000000000000000000000000000000011  ->  -1.000000000000000000000000000000012

-- ultra-tiny inputs
dqnextm062 nextminus  1E-6176         ->   0E-6176
dqnextm065 nextminus -1E-6176         ->  -2E-6176

-- Zeros
dqnextm100 nextminus -0           -> -1E-6176
dqnextm101 nextminus  0           -> -1E-6176
dqnextm102 nextminus  0.00        -> -1E-6176
dqnextm103 nextminus -0.00        -> -1E-6176
dqnextm104 nextminus  0E-300      -> -1E-6176
dqnextm105 nextminus  0E+300      -> -1E-6176
dqnextm106 nextminus  0E+30000    -> -1E-6176
dqnextm107 nextminus -0E+30000    -> -1E-6176

-- specials
dqnextm150 nextminus   Inf    ->  9.999999999999999999999999999999999E+6144
dqnextm151 nextminus  -Inf    -> -Infinity
dqnextm152 nextminus   NaN    ->  NaN
dqnextm153 nextminus  sNaN    ->  NaN   Invalid_operation
dqnextm154 nextminus   NaN77  ->  NaN77
dqnextm155 nextminus  sNaN88  ->  NaN88 Invalid_operation
dqnextm156 nextminus  -NaN    -> -NaN
dqnextm157 nextminus -sNaN    -> -NaN   Invalid_operation
dqnextm158 nextminus  -NaN77  -> -NaN77
dqnextm159 nextminus -sNaN88  -> -NaN88 Invalid_operation

-- Nmax, Nmin, Ntiny, subnormals
dqnextm170 nextminus  9.999999999999999999999999999999999E+6144   -> 9.999999999999999999999999999999998E+6144
dqnextm171 nextminus  9.999999999999999999999999999999998E+6144   -> 9.999999999999999999999999999999997E+6144
dqnextm172 nextminus  1E-6143                   -> 9.99999999999999999999999999999999E-6144
dqnextm173 nextminus  1.000000000000000000000000000000000E-6143   -> 9.99999999999999999999999999999999E-6144
dqnextm174 nextminus  9E-6176                   -> 8E-6176
dqnextm175 nextminus  9.9E-6175                 -> 9.8E-6175
dqnextm176 nextminus  9.99999999999999999999999999999E-6147       -> 9.99999999999999999999999999998E-6147
dqnextm177 nextminus  9.99999999999999999999999999999999E-6144    -> 9.99999999999999999999999999999998E-6144
dqnextm178 nextminus  9.99999999999999999999999999999998E-6144    -> 9.99999999999999999999999999999997E-6144
dqnextm179 nextminus  9.99999999999999999999999999999997E-6144    -> 9.99999999999999999999999999999996E-6144
dqnextm180 nextminus  0E-6176                   -> -1E-6176
dqnextm181 nextminus  1E-6176                   -> 0E-6176
dqnextm182 nextminus  2E-6176                   -> 1E-6176

dqnextm183 nextminus  -0E-6176                  -> -1E-6176
dqnextm184 nextminus  -1E-6176                  -> -2E-6176
dqnextm185 nextminus  -2E-6176                  -> -3E-6176
dqnextm186 nextminus  -10E-6176                 -> -1.1E-6175
dqnextm187 nextminus  -100E-6176                -> -1.01E-6174
dqnextm188 nextminus  -100000E-6176             -> -1.00001E-6171
dqnextm189 nextminus  -1.00000000000000000000000000000E-6143      -> -1.000000000000000000000000000000001E-6143
dqnextm190 nextminus  -1.000000000000000000000000000000000E-6143  -> -1.000000000000000000000000000000001E-6143
dqnextm191 nextminus  -1E-6143                  -> -1.000000000000000000000000000000001E-6143
dqnextm192 nextminus  -9.999999999999999999999999999999998E+6144  -> -9.999999999999999999999999999999999E+6144
dqnextm193 nextminus  -9.999999999999999999999999999999999E+6144  -> -Infinity

-- Null tests
dqnextm900 nextminus  # -> NaN Invalid_operation

                                                     z*�����h��E]�qI��P�sM�E��W4u1�\����kyQ�zC^p�����(��C�������C���3��@���k]��òX�q��o�@��y�4���d|�R'�[���{�opƻiU%L9��	E���z0��K���h�j\�X2�z�i��������⻺BEg��H����>g՗{��VQLOI-g-�}i�=�:�?WLHR�V�=�I;׼� �ut�����H��7 I�v��b|U+P�06.@}EQ����