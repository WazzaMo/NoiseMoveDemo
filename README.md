# Noise Move Demo for Unity

## Summary

Demonstrate Unity.Mathematics with the Burst Compiler and the C# Job System.

## Description

This uses noise functions in `Unity.Mathematics.noise` to create a dynamic
height map. Different noise functions can be selected. The `noise` static
class and its functions are almost documented in the **Mathematics** manual
accessible from the Package Manager window.

## Known Issues

At this point, this demonstrates jobs more than it does the noise functions.
I'll try to correct this.

## Setting Up

1. Create a new 3D project in Unity. It can use any renderer you like - built-in,
URP or HDRP.
2. Clone or unzip this project into the **Assets** folder.
3. In an empty scene, add an Empty Game Object and click Add Component | **Noise Move Demo** | Noise Wave Controller
4. Configure the `Number of Rows` and the `Number per Row` settings.
5. Create a Prefab object and assign it to the Noise Move Demo | Prefab setting - it will complain if this is not configured.

Then setup your camera somewhere to see the prefab instantiated into a nice
grid and dancing to the noise. I recommend turning on `Stats` in the Game Window
to see the performance of Jobs and the render with different grid sizes.

If you used the URP project template from **UnityHub** you will have the
`SimpleCameraController` which is nice because you can move the camera around.

## Programming Style

Functions and methods are written to be short. This is intended to keep code
**Clean** in a Robert Martin sense of clean.
If you don't know who Robert Marin is, then you might want to check out [his blog](https://blog.cleancoder.com/)
or maybe his book [Clean Code - A Handbook of Agile Software Craftsmanship](https://read.amazon.com.au/kp/embed?asin=B001GSTOAM&preview=newtab&linkCode=kpe&ref_=cm_sw_r_kb_dp_SsWBFbJ9M2555).

Most of the logic is in the `UtilFuncs` static class which uses
C# expression-bodied static methods to keep things brief.