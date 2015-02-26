# TODO List

## New color spaces
 * [ ] CIE RGB
 * [ ] CIELUV
 * [ ] CIELAB
 * [ ] HSV
 * [ ] HSL
 * [ ] CMYK

## Illuminants
 * [X] A (Incandescent / Tungsten)
 * [X] B (Direct sunlight at noon (obsolete))
 * [X] C (Average / North sky Daylight (obsolete))
 * [X] D50 (Horizon Light. ICC profile PCS)
 * [X] D55 (Mid-morning / Mid-afternoon Daylight)
 * [X] D65 (Noon Daylight: Television, sRGB color space)
 * [ ] D75 (North sky Daylight)
 * [ ] E (Equal energy)
 * [ ] F2 (Cool White Fluorescent)
 * [ ] F7 (D65 simulator, Daylight simulator)
 * [ ] F11 (Philips TL84, Ultralume 40)
 * [ ] Custom White Points

## Connectivity & Interoperability
 * [ ] Connection with spectralworkbench.org's API .

## Feature Extraction

### Light Spectrums
  * [ ] Color Quality Scale (CQS)
  * [ ] Color Rendering Index (CRI)

### Colors
  * [X] Color Temperature

## Color Operations
  * [ ] IsAdditive : bool
  * [ ] colorC = colorA + colorB

## Performance
  * [ ] Make use of OpenCL to improve conversions performance (with OpenCL.NET?)
  * [ ] SIMD? (Mono & .NET have different approaches :s)

## Packaging
  * [ ] Multiple versions for multiple .NET versionsi conside one single NuGet package
  * [ ] .DEB packages
  * [ ] .RPM packages

## Quality Assurance
  * [ ] Ensure immutability wherever it's possible.
  * [ ] >= 85% Unit Testing Coverage
  * [ ] AppVeyor Integration
  * [ ] Coverity Scan Integration

## Documentation
  * [ ] Wiki
  * [X] Improve inline documentation (code comments)
  * [ ] Create Roadmap

## Trademark Issues?
 * [ ] Change the project name! ( and many links... :s )

