//-*****************************************************************************

Pixar's Dtex to Exr conversion utility
Aug 1, 2012 - first checkin to OpenEXR 2.0 branch of OpenEXR git repository.

This utility converts a DTEX deep shadow file to an OpenEXR 2.0 deep exr file,
formerly, ".odz" (or .dexr, or whatever extension your facility may use). As of
the first checkin, the utility only supports 1, 3, or 4-channel dtex files,
corresponding to deep alpha or deep RGBA only.  The Nuke DTEX reader, which
uses basically the same code, will support a more complete arbitrary channel
set, and this utility may be upgraded as well. We also only support a single
view for the time being.

DTEX has a complicated set of interpretations, which represent six separate
code paths for conversion to the deep representation in OpenEXR 2.0. They
represent the permutations of these exemplary Display Driver config strings
to prman:

Display "+filename.dtex" "deepshad" "deepopacity" "string volumeinterpretation" "continuous"
Display "+filename.dtex" "deepshad" "deepopacity" "string volumeinterpretation" "discrete"
Display "+filename.dtex" "deepshad" "a" "string volumeinterpretation" "continuous"
Display "+filename.dtex" "deepshad" "a" "string volumeinterpretation" "discrete"
Display "+filename.dtex" "deepshad" "rgba" "string volumeinterpretation" "continuous"
Display "+filename.dtex" "deepshad" "rgba" "string volumeinterpretation" "discrete"

In our terminology, these six options are called:

OneChanDeepOpacityContinuous
OneChanDeepOpacityDiscrete
OneChanDeepAlphaContinuous
OneChanDeepAlphaDiscrete
FourChanDeepRgbaContinuous
FourChanDeepRgbaDiscrete

Renderman will write out both 1-channel and 3-channel opacity in the 
"deepopacity" case, which we currently only use the first channel of, as
we are only targeting a single opacity curve in space. This may evolve in
the future.

There are weird gotchas for each of these 6 cases, and they are documented
in the PxDeepUtils.h file, along with specific comments in each of the six
header files that correspond to each case:

PxOneChanDeepOpacityContinuous.h
PxOneChanDeepOpacityDiscrete.h
PxOneChanDeepAlphaContinuous.h
PxOneChanDeepAlphaDiscrete.h
PxFourChanDeepRgbaContinuous.h
PxFourChanDeepRgbaDiscrete.h

The gotchas relate to several issues, mostly to do with coincident or
out-of-order samples, precision issues, compression artifacts, and the
differences between the "deepopacity" vs "deepalpha" interpretations.

With "deepopacity" (the pre-prman 16 usage), opacity values stored in
dtex files were accumulated, so they were bounded between 0 and 1. The
values stored are actually not opacities, but rather transmissivities
(1-opacity), and monotonically decreased from fully transparent (1)
to fully opaque (0). This representation is ideal for meaningful error 
minimization during  compression, and also for usage as a shadow map 
by a renderer, because the extinction at a particular depth, at a 
particular pixel, can be evaluated with a single look-up. However, 
this representation is poor for deep compositing, because each of the 
samples includes data from smaller depth samples, and recombination is 
difficult. With these types of files, when using volumetric (continuous) 
interpretation, the samples represent the accumulated transmissivity at 
the NEAR SIDE of a depth span, and our code paths take this into account.

With all other usages - "a" and "rgba", the values represent filtered
samples of the given field at that point in space. The alpha values
will be between 0 and 1, but are not guaranteed to be increasing or
decreasing. This presents a strange problem for volumetric interpretation,
as what does it mean to describe the alpha of an infintesimally small
region of space (point sample). The best interpretation of the data
in this case uses the alpha to represent the accumulated opacity of 
the region of space nearer than the sample, and thus each sample
represents the FAR SIDE of a depth span. Our code paths take this into
account as well.

Please see the files for additional comments, and the command
"dtexToExr" may be run with -h, --h, --help, or no arguments to 
print its usage.

-Christopher Horvath, Aug 2012, Pixar

                                                                                                                                                                                                                               