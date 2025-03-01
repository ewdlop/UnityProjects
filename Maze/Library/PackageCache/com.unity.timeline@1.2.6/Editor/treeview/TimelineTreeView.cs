------------------------------------------------------------------------
-- ddOr.decTest -- digitwise logical OR for decDoubles                --
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

precision:   16
maxExponent: 384
minExponent: -383
extended:    1
clamp:       1
rounding:    half_even

-- Sanity check (truth table)
ddor001 or             0    0 ->    0
ddor002 or             0    1 ->    1
ddor003 or             1    0 ->    1
ddor004 or             1    1 ->    1
ddor005 or          1100 1010 -> 1110
-- and at msd and msd-1
ddor006 or 0000000000000000 0000000000000000 ->           0
ddor007 or 0000000000000000 1000000000000000 ->   1000000000000000
ddor008 or 1000000000000000 0000000000000000 ->   1000000000000000
ddor009 or 1000000000000000 1000000000000000 ->   1000000000000000
ddor010 or 0000000000000000 0000000000000000 ->           0
ddor011 or 0000000000000000 0100000000000000 ->    100000000000000
ddor012 or 0100000000000000 0000000000000000 ->    100000000000000
ddor013 or 0100000000000000 0100000000000000 ->    100000000000000

-- Various lengths
--         1234567890123456     1234567890123456 1234567890123456
ddor020 or 1111111111111111     1111111111111111  ->  1111111111111111
ddor021 or  111111111111111      111111111111111  ->   111111111111111
ddor022 or   11111111111111       11111111111111  ->    11111111111111
ddor023 or    1111111111111        1111111111111  ->     1111111111111
ddor024 or     111111111111         111111111111  ->      111111111111
ddor025 or      11111111111          11111111111  ->       11111111111
ddor026 or       1111111111           1111111111  ->        1111111111
ddor027 or        111111111            111111111  ->         111111111
ddor028 or         11111111             11111111  ->          11111111
ddor029 or          1111111              1111111  ->           1111111
ddor030 or           111111               111111  ->            111111
ddor031 or            11111                11111  ->             11111
ddor032 or             1111                 1111  ->              1111
ddor033 or              111                  111  ->               111
ddor034 or               11                   11  ->                11
ddor035 or                1                    1  ->                 1
ddor036 or                0                    0  ->                 0

ddor042 or  111111110000000     1111111110000000  ->  1111111110000000
ddor043 or   11111110000000     1000000100000000  ->  1011111110000000
ddor044 or    1111110000000     1000001000000000  ->  1001111110000000
ddor045 or     111110000000     1000010000000000  ->  1000111110000000
ddor046 or      11110000000     1000100000000000  ->  1000111110000000
ddor047 or       1110000000     1001000000000000  ->  1001001110000000
ddor048 or        110000000     1010000000000000  ->  1010000110000000
ddor049 or         10000000     1100000000000000  ->  1100000010000000

ddor090 or 011111111  111101111  ->  111111111
ddor091 or 101111111  111101111  ->  111111111
ddor092 or 110111111  111101111  ->  111111111
ddor093 or 111011111  111101111  ->  111111111
ddor094 or 111101111  111101111  ->  111101111
ddor095 or 111110111  111101111  ->  111111111
ddor096 or 111111011  111101111  ->  111111111
ddor097 or 111111101  111101111  ->  111111111
ddor098 or 111111110  111101111  ->  111111111

ddor100 or 111101111  011111111  ->  111111111
ddor101 or 111101111  101111111  ->  111111111
ddor102 or 111101111  110111111  ->  111111111
ddor103 or 111101111  111011111  ->  111111111
ddor104 or 111101111  111101111  ->  111101111
ddor105 or 111101111  111110111  ->  111111111
ddor106 or 111101111  111111011  ->  111111111
ddor107 or 111101111  111111101  ->  111111111
ddor108 or 111101111  111111110  ->  111111111

-- non-0/1 should not be accepted, nor should signs
ddor220 or 111111112  111111111  ->  NaN Invalid_operation
ddor221 or 333333333  333333333  ->  NaN Invalid_operation
ddor222 or 555555555  555555555  ->  NaN Invalid_operation
ddor223 or 777777777  777777777  ->  NaN Invalid_operation
ddor224 or 999999999  999999999  ->  NaN Invalid_operation
ddor225 or 222222222  999999999  ->  NaN Invalid_operation
ddor226 or 444444444  999999999  ->  NaN Invalid_operation
ddor227 or 666666666  999999999  ->  NaN Invalid_operation
ddor228 or 888888888  999999999  ->  NaN Invalid_operation
ddor229 or 999999999  222222222  ->  NaN Invalid_operation
ddor230 or 999999999  444444444  ->  NaN Invalid_operation
ddor231 or 999999999  666666666  ->  NaN Invalid_operation
ddor232 or 999999999  888888888  ->  NaN Invalid_operation
-- a few randoms
ddor240 or  567468689 -934981942 ->  NaN Invalid_operation
ddor241 or  567367689  934981942 ->  NaN Invalid_operation
ddor242 or -631917772 -706014634 ->  NaN Invalid_operation
ddor243 or -756253257  138579234 ->  NaN Invalid_operation
ddor244 or  835590149  567435400 ->  NaN Invalid_operation
-- test MSD
ddor250 or  2000000000000000 1000000000000000 ->  NaN Invalid_operation
ddor251 or  7000000000000000 1000000000000000 ->  NaN Invalid_operation
ddor252 or  8000000000000000 1000000000000000 ->  NaN Invalid_operation
ddor253 or  9000000000000000 1000000000000000 ->  NaN Invalid_operation
ddor254 or  2000000000000000 0000000000000000 ->  NaN Invalid_operation
ddor255 or  7000000000000000 0000000000000000 ->  NaN Invalid_operation
ddor256 or  8000000000000000 0000000000000000 ->  NaN Invalid_operation
ddor257 or  9000000000000000 0000000000000000 ->  NaN Invalid_operation
ddor258 or  1000000000000000 2000000000000000 ->  NaN Invalid_operation
ddor259 or  1000000000000000 7000000000000000 ->  NaN Invalid_operation
ddor260 or  1000000000000000 8000000000000000 ->  NaN Invalid_operation
ddor261 or  1000000000000000 9000000000000000 ->  NaN Invalid_operation
ddor262 or  0000000000000000 2000000000000000 ->  NaN Invalid_operation
ddor263 or  0000000000000000 7000000000000000 ->  NaN Invalid_operation
ddor264 or  0000000000000000 8000000000000000 ->  NaN Invalid_operation
ddor265 or  0000000000000000 9000000000000000 ->  NaN Invalid_operation
-- test MSD-1
ddor270 or  0200001000000000 1000100000000010 ->  NaN Invalid_operation
ddor271 or  0700000100000000 1000010000000100 ->  NaN Invalid_operation
ddor272 or  0800000010000000 1000001000001000 ->  NaN Invalid_operation
ddor273 or  0900000001000000 1000000100010000 ->  NaN Invalid_operation
ddor274 or  1000000000100000 0200000010100000 ->  NaN Invalid_operation
ddor275 or  1000000000010000 0700000001000000 ->  NaN Invalid_operation
ddor276 or  1000000000001000 0800000010100000 ->  NaN Invalid_operation
ddor277 or  1000000000000100 0900000000010000 ->  NaN Invalid_operation
-- test LSD
ddor280 or  0010000000000002 1000000100000001 ->  NaN Invalid_operation
ddor281 or  0001000000000007 1000001000000011 ->  NaN Invalid_operation
ddor282 or  0000100000000008 1000010000000001 ->  NaN Invalid_operation
ddor283 or  0000010000000009 1000100000000001 ->  NaN Invalid_operation
ddor284 or  1000001000000000 0001000000000002 ->  NaN Invalid_operation
ddor285 or  1000000100000000 0010000000000007 ->  NaN Invalid_operation
ddor286 or  1000000010000000 0100000000000008 ->  NaN Invalid_operation
ddor287 or  1000000001000000 1000000000000009 ->  NaN Invalid_operation
-- test Middie
ddor288 or  0010000020000000 1000001000000000 ->  NaN Invalid_operation
ddor289 or  0001000070000001 1000000100000000 ->  NaN Invalid_operation
ddor290 or  0000100080000010 1000000010000000 ->  NaN Invalid_operation
ddor291 or  0000010090000100 1000000001000000 ->  NaN Invalid_operation
ddor292 or  1000001000001000 0000000020100000 ->  NaN Invalid_operation
ddor293 or  1000000100010000 0000000070010000 ->  NaN Invalid_operation
ddor294 or  1000000010100000 0000000080001000 ->  NaN Invalid_operation
ddor295 or  1000000001000000 0000000090000100 ->  NaN Invalid_operation
-- signs
ddor296 or -1000000001000000 -0000010000000100 ->  NaN Invalid_operation
ddor297 or -1000000001000000  0000000010000100 ->  NaN Invalid_operation
ddor298 or  1000000001000000 -0000001000000100 ->  NaN Invalid_operation
ddor299 or  1000000001000000  0000000011000100 ->  1000000011000100

-- Nmax, Nmin, Ntiny-like
ddor331 or  2   9.99999999E+199     -> NaN Invalid_operation
ddor332 or  3   1E-199              -> NaN Invalid_operation
ddor333 or  4   1.00000000E-199     -> NaN Invalid_operation
ddor334 or  5   1E-100              -> NaN Invalid_operation
ddor335 or  6   -1E-100             -> NaN Invalid_operation
ddor336 or  7   -1.00000000E-199    -> NaN Invalid_operation
ddor337 or  8   -1E-199             -> NaN Invalid_operation
ddor338 or  9   -9.99999999E+199    -> NaN Invalid_operation
ddor341 or  9.99999999E+299     -18 -> NaN Invalid_operation
ddor342 or  1E-299               01 -> NaN Invalid_operation
ddor343 or  1.00000000E-299     -18 -> NaN Invalid_operation
ddor344 or  1E-100               18 -> NaN Invalid_operation
ddor345 or  -1E-100             -10 -> NaN Invalid_operation
ddor346 or  -1.00000000E-299     18 -> NaN Invalid_operation
ddor347 or  -1E-299              10 -> NaN Invalid_operation
ddor348 or  -9.99999999E+299    -18 -> NaN Invalid_operation

-- A few other non-integers
ddor361 or  1.0                  1  -> NaN Invalid_operation
ddor362 or  1E+1                 1  -> NaN Invalid_operation
ddor363 or  0.0                  1  -> NaN Invalid_operation
ddor364 or  0E+1                 1  -> NaN Invalid_operation
ddor365 or  9.9                  1  -> NaN Invalid_operation
ddor366 or  9E+1                 1  -> NaN Invalid_operation
ddor371 or  0 1.0                   -> NaN Invalid_operation
ddor372 or  0 1E+1                  -> NaN Invalid_operation
ddor373 or  0 0.0                   -> NaN Invalid_operation
ddor374 or  0 0E+1                  -> NaN Invalid_operation
ddor375 or  0 9.9                   -> NaN Invalid_operation
ddor376 or  0 9E+1                  -> NaN Invalid_operation

-- All Specials are in error
ddor780 or -Inf  -Inf   -> NaN Invalid_operation
ddor781 or -Inf  -1000  -> NaN Invalid_operation
ddor782 or -Inf  -1     -> NaN Invalid_operation
ddor783 or -Inf  -0     -> NaN Invalid_operation
ddor784 or -Inf   0     -> NaN Invalid_operation
ddor785 or -Inf   1     -> NaN Invalid_operation
ddor786 or -Inf   1000  -> NaN Invalid_operation
ddor787 or -1000 -Inf   -> NaN Invalid_operation
ddor788 or -Inf  -Inf   -> NaN Invalid_operation
ddor789 or -1    -Inf   -> NaN Invalid_operation
ddor790 or -0    -Inf   -> NaN Invalid_operation
ddor791 or  0    -Inf   -> NaN Invalid_operation
ddor792 or  1    -Inf   -> NaN Invalid_operation
ddor793 or  1000 -Inf   -> NaN Invalid_operation
ddor794 or  Inf  -Inf   -> NaN Invalid_operation

ddor800 or  Inf  -Inf   -> NaN Invalid_operation
ddor801 or  Inf  -1000  -> NaN Invalid_operation
ddor802 or  Inf  -1     -> NaN Invalid_operation
ddor803 or  Inf  -0     -> NaN Invalid_operation
ddor804 or  Inf   0     -> NaN Invalid_operation
ddor805 or  Inf   1     -> NaN Invalid_operation
ddor806 or  Inf   1000  -> NaN Invalid_operation
ddor807 or  Inf   Inf   -> NaN Invalid_operation
ddor808 or -1000  Inf   -> NaN Invalid_operation
ddor809 or -Inf   Inf   -> NaN Invalid_operation
ddor810 or -1     Inf   -> NaN Invalid_operation
ddor811 or -0     Inf   -> NaN Invalid_operation
ddor812 or  0     Inf   -> NaN Invalid_operation
ddor813 or  1     Inf   -> NaN Invalid_operation
ddor814 or  1000  Inf   -> NaN Invalid_operation
ddor815 or  Inf   Inf   -> NaN Invalid_operation

ddor821 or  NaN -Inf    -> NaN Invalid_operation
ddor822 or  NaN -1000   -> NaN Invalid_operation
ddor823 or  NaN -1      -> NaN Invalid_operation
ddor824 or  NaN -0      -> NaN Invalid_operation
ddor825 or  NaN  0      -> NaN Invalid_operation
ddor826 or  NaN  1      -> NaN Invalid_operation
ddor827 or  NaN  1000   -> NaN Invalid_operation
ddor828 or  NaN  Inf    -> NaN Invalid_operation
ddor829 or  NaN  NaN    -> NaN Invalid_operation
ddor830 or -Inf  NaN    -> NaN Invalid_operation
ddor831 or -1000 NaN    -> NaN Invalid_operation
ddor832 or -1    NaN    -> NaN Invalid_operation
ddor833 or -0    NaN    -> NaN Invalid_operation
ddor834 or  0    NaN    -> NaN Invalid_operation
ddor835 or  1    NaN    -> NaN Invalid_operation
ddor836 or  1000 NaN    -> NaN Invalid_operation
ddor837 or  Inf  NaN    -> NaN Invalid_operation

ddor841 or  sNaN -Inf   ->  NaN  Invalid_operation
ddor842 or  sNaN -1000  ->  NaN  Invalid_operation
ddor843 or  sNaN -1     ->  NaN  Invalid_operation
ddor844 or  sNaN -0     ->  NaN  Invalid_operation
ddor845 or  sNaN  0     ->  NaN  Invalid_operation
ddor846 or  sNaN  1     ->  NaN  Invalid_operation
ddor847 or  sNaN  1000  ->  NaN  Invalid_operation
ddor848 or  sNaN  NaN   ->  NaN  Invalid_operation
ddor849 or  sNaN sNaN   ->  NaN  Invalid_operation
ddor850 or  NaN  sNaN   ->  NaN  Invalid_operation
ddor851 or -Inf  sNaN   ->  NaN  Invalid_operation
ddor852 or -1000 sNaN   ->  NaN  Invalid_operation
ddor853 or -1    sNaN   ->  NaN  Invalid_operation
ddor854 or -0    sNaN   ->  NaN  Invalid_operation
ddor855 or  0    sNaN   ->  NaN  Invalid_operation
ddor856 or  1    sNaN   ->  NaN  Invalid_operation
ddor857 or  1000 sNaN   ->  NaN  Invalid_operation
ddor858 or  Inf  sNaN   ->  NaN  Invalid_operation
ddor859 or  NaN  sNaN   ->  NaN  Invalid_operation

-- propagating NaNs
ddor861 or  NaN1   -Inf    -> NaN Invalid_operation
ddor862 or +NaN2   -1000   -> NaN Invalid_operation
ddor863 or  NaN3    1000   -> NaN Invalid_operation
ddor864 or  NaN4    Inf    -> NaN Invalid_operation
ddor865 or  NaN5   +NaN6   -> NaN Invalid_operation
ddor866 or -Inf     NaN7   -> NaN Invalid_operation
ddor867 or -1000    NaN8   -> NaN Invalid_operation
ddor868 or  1000    NaN9   -> NaN Invalid_operation
ddor869 or  Inf    +NaN10  -> NaN Invalid_operation
ddor871 or  sNaN11  -Inf   -> NaN Invalid_operation
ddor872 or  sNaN12  -1000  -> NaN Invalid_operation
ddor873 or  sNaN13   1000  -> NaN Invalid_operation
ddor874 or  sNaN14   NaN17 -> NaN Invalid_operation
ddor875 or  sNaN15  sNaN18 -> NaN Invalid_operation
ddor876 or  NaN16   sNaN19 -> NaN Invalid_operation
ddor877 or -Inf    +sNaN20 -> NaN Invalid_operation
ddor878 or -1000    sNaN21 -> NaN Invalid_operation
ddor879 or  1000    sNaN22 -> NaN Invalid_operation
ddor880 or  Inf     sNaN23 -> NaN Invalid_operation
ddor881 or +NaN25  +sNaN24 -> NaN Invalid_operation
ddor882 or -NaN26    NaN28 -> NaN Invalid_operation
ddor883 or -sNaN27  sNaN29 -> NaN Invalid_operation
ddor884 or  1000    -NaN30 -> NaN Invalid_operation
ddor885 or  1000   -sNaN31 -> NaN Invalid_operation
                                                     