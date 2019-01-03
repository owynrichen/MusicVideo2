The Beat Kitchen Music Video Template 2
=====================================

This is some code I pulled together to generate some basic music videos for instrumentals my buddy and I write.

The audio code and some of the spectral visualizations were built by following an awesome set of tutorials on YouTube
by Peer Play, here's the first one: https://www.youtube.com/watch?v=4Av788P9stk

The results of this template output an audio visualization that looks similar to this.

[![Music Video Screenshot](./screenshot.png)](https://www.youtube.com/watch?v=w0AQ8lia9w8)

Unity Asset Requirements
========================
* Unity Post Processing Stack - v1.0.4 [https://assetstore.unity.com/packages/essentials/post-processing-stack-83912](https://assetstore.unity.com/packages/essentials/post-processing-stack-83912)
* Unity Recorder - v1.0.2 [https://assetstore.unity.com/packages/essentials/unity-recorder-94079](https://assetstore.unity.com/packages/essentials/unity-recorder-94079)
* Aura Volumetric Lighting - v1.0.5 [https://assetstore.unity.com/packages/vfx/shaders/aura-volumetric-lighting-111664](https://assetstore.unity.com/packages/vfx/shaders/aura-volumetric-lighting-111664) and on github at [https://github.com/raphael-ernaelsten/Aura](https://github.com/raphael-ernaelsten/Aura)
* Rainmaker - v2.1.1 [https://assetstore.unity.com/packages/vfx/particles/environment/rain-maker-2d-and-3d-rain-particle-system-for-unity-34938](https://assetstore.unity.com/packages/vfx/particles/environment/rain-maker-2d-and-3d-rain-particle-system-for-unity-34938)

The above versions are what I've tested it with, it may work with newer versions (Post Processing has a 2.0 in development, for instance).


How-To
======

* Import your audio file into the project.
* In the AudioPeerContainer, there is a reference to the AudioSource, reference your new audio file.
* Also in the AudioPeerContainer, there is a StartSimulation component that identifies a delay time.  This delay time should
be > 0.25 or so because the UnityRecorder component misses the first few frames, so you lose audio without a slight delay.
* With the free UnityRecorder asset installed, you head to the Window > General > Recorder > Recording Window
* Set the start and end times, and hit Start Recording

Credits
======

The human model and dancing animation are from [mixamo.com](https://www.mixamo.com/).