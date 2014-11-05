ColorSharp's Changelog
======================

## 2014-11-05 : 0.6.0 Release

### Contributors
 * Andrés Correa Casablanca <castarco@gmail.com , castarco@litipk.com>

### Changes
 * Improved XML documentation.
 * Removed many build warnings (related with XML documentation)
 * Sealed many classes.
 * Removed unused properties from CIExyY class.
 * Split project into ColorSharp and ColorSharpTests.
 * Removed NUnit dependency.

### Bugfixes
 * Bugfix in sRGB->CIE's 1931 XYZ conversion (the gamma correction was done after the lineal transformation)
