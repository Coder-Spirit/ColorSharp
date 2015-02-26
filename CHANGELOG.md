ColorSharp's Changelog
======================

## 2015-02-26 : 0.11.0 Release

### Contributors
 * Andrés Correa Casablanca <castarco@gmail.com , castarco@litipk.com>

### Changes
 * Added CCT (Correlated Color Temparature) computation.
 * Added Duv (Distance on CIE's 1960 UCS/Yuv color space) computation.

## 2015-02-25 : 0.10.0 Release

### Project Handling
 * Until we have more contributors, removed the references to contributors for
   every release in the CHANGELOG file.
 * Connected the ColorSharp GIT repository to Gitter in order to make possible
   only & real-time assistance to users and other programmers.
 * Connected the project to Gitcolony. This is an experiment, and only will be
   useful if more programmers join to the project.
 * Added a basic CONTRIBUTING guidelines file.

### Changes
 * Added the illuminants B, C, D50 and D55.
 * Added the CIE's 1964 10º matching functions.
 * Added black body spectrums for given CCTs.
 * Replaced the CIE's 1931 2º matching functions data with more precisa data.
 * Minor improvements in spectrum->color conversions, splitted spectrum->color
   conversion strategies from color->color conversion strategies.
 * Improved XML documentation.
 * Removed MathNet.Numerics dependency.

### Bugfixes
 * Many bugfixes in the UCS color space handling.

### Warnings
 * Minor API breaks, mainly in spectrum->color conversions, but not only there.

## 2015-01-13 : 0.9.1 Release

### Changes
 * Added CIE's 1960 UCS color space
 * Less destructive conversions (now the data is better preserved)
 * Now ToSRGB, ToCIExyY and ToCIEUCS are virtual methods, not abstract.
 * Updated MathNet.Numerics dependency.


## 2014-12-19 : 0.8.3 Release

### Changes
 * Updated MathNet.Numerics dependency
 * Added HashCode to light spectrum objects
 * Minor performance tweaks


## 2014-12-05 : 0.8.2 Release

### Minor changes
 * Added new constructor to the RegularLightSpectrum class.

## 2014-11-12 : 0.8.1 Release

### Bugfixes
 * Fixed D65 spectrum data points.


## 2014-11-12 : 0.8.0 Release

### Changes
 * Added Illuminants (only D65)
 * Added more precise matching functions
 * Improved XML documentation
 * Increased unit testing coverage
 * Improved class constructors flexibility
 * Exposed more immutable properties


## 2014-11-05 : 0.7.0 Release

### Changes
 * Big refactor:
   * Removed the colors conversion path search mechanism.
   * Now is less flexible, but more efficient and simple.
   * Now it's better to use the non type-parametric conversion methods.
   * **WARNING:** Now we assume that Y=1 in CIE's xyY or XYZ color spaces is the luminance of the white point.
   * **WARNING:** Breaks API.


## 2014-11-05 : 0.6.0 Release

### Changes
 * Improved XML documentation.
 * Removed many build warnings (related with XML documentation)
 * Sealed many classes.
 * Removed unused properties from CIExyY class.
 * Split project into ColorSharp and ColorSharpTests.
 * Removed NUnit dependency.

### Bugfixes
 * Bugfix in sRGB->CIE's 1931 XYZ conversion (the gamma correction was done after the lineal transformation)
