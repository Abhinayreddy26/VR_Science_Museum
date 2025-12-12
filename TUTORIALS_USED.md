# Tutorials and Learning Resources Used

**Project:** VR Science Museum - Solar System Explorer

**Note:** This project was developed from scratch based on my own knowledge and understanding. The tutorials listed below were used only to understand programming concepts and Unity fundamentals - **NO CODE WAS COPIED** from any tutorial.

---

## Tutorials Beyond Module Content

Below is a list of all tutorials and learning resources beyond the module content that I consulted to help develop my 3D application.

| Tutorial Link | Description of How Tutorial Was Used |
|--------------|-------------------------------------|
| [Unity Learn: Singleton Pattern](https://learn.unity.com/tutorial/singletons) | I studied the Singleton pattern concept presented here to understand how to create globally accessible managers. I adapted this pattern to create my own `GameManager.cs`, `AudioManager.cs`, `PlanetManager.cs`, and other manager classes. My implementation includes custom Instance properties and proper Awake/OnDestroy lifecycle management specific to my project's needs. **Lines affected:** GameManager.cs (lines 10-33), AudioManager.cs (lines 7-24), PlanetManager.cs (lines 8-24). |
| [Unity Documentation: Physics.Raycast](https://docs.unity3d.com/ScriptReference/Physics.Raycast.html) | I used the official Unity documentation to understand raycasting mechanics. I adapted the concepts to implement planet interaction in `SimplePlayerController.cs` where I raycast from the camera center to detect planet clicks, and in `HUDController.cs` for real-time target detection. My implementation uses ViewportPointToRay with center coordinates (0.5, 0.5) for crosshair-based interaction. **Lines affected:** SimplePlayerController.cs (lines 223-234), HUDController.cs (lines 184-194). |
| [Unity Answers: Random Points on Sphere](https://answers.unity.com/questions/213437/random-points-on-sphere.html) | I viewed general discussion about generating random positions on a sphere surface. I then wrote my own `StarfieldGenerator.cs` using `Random.onUnitSphere` multiplied by distance to place 500 stars around the player. I added my own twinkle animation system and color variation that was not part of any tutorial. **Lines affected:** StarfieldGenerator.cs (lines 47-75). |
| [Unity Documentation: Coroutines](https://docs.unity3d.com/Manual/Coroutines.html) | I studied the official Unity documentation on coroutines and IEnumerator to understand how to create time-based sequences. I applied this knowledge to write my own `GuidedTourController.cs` which automatically moves the player between planets using smooth lerp transitions and timed information displays. My implementation includes custom smoothstep easing and state checking. **Lines affected:** GuidedTourController.cs (lines 85-177). |
| [Unity Documentation: Canvas Scaler](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/script-CanvasScaler.html) | I read about Canvas Scaler modes to understand UI scaling across different resolutions. I used this knowledge to create resolution-adaptive UI in `UIHelper.cs` with ScaleWithScreenSize mode referencing 1920x1080. I also implemented custom OnGUI scaling math in `MainMenuUI.cs` for the fallback menu system. **Lines affected:** UIHelper.cs (lines 22-27), MainMenuUI.cs (lines 81-95). |
| [Unity Forum: Material.color vs Emission](https://forum.unity.com/threads/material-color-vs-emission.409348/) | I read forum discussions about making objects appear to glow. I adapted this understanding to create the emissive Sun in `PlanetManager.cs` by multiplying the material color by 1.5 to increase brightness, creating a glowing effect without using actual emission properties. **Lines affected:** PlanetManager.cs (lines 188-192). |
| [Unity Documentation: CharacterController](https://docs.unity3d.com/ScriptReference/CharacterController.html) | I studied the CharacterController component documentation to understand player movement mechanics. I implemented my own first-person controller in `SimplePlayerController.cs` with WASD movement, gravity, and camera-relative direction calculations. My implementation includes dual input system support (new and legacy). **Lines affected:** SimplePlayerController.cs (lines 105-177). |
| [Unity Learn: State Machines](https://learn.unity.com/tutorial/5c5151c1edbc2a001fd5c696) | I reviewed state machine concepts to understand game state management. I designed and implemented my own GameState enum with 5 states (MainMenu, Loading, Playing, Paused, GuidedTour) in `GameManager.cs` with event-driven state transitions and proper time scale handling. **Lines affected:** GameManager.cs (lines 6-71). |
| [Unity Documentation: Resources.Load](https://docs.unity3d.com/ScriptReference/Resources.Load.html) | I read documentation about loading assets at runtime from Resources folders. I applied this to create texture loading in `PlanetManager.cs` with multiple fallback paths and audio loading in `AudioManager.cs`. My implementation includes error handling and debug logging. **Lines affected:** PlanetManager.cs (lines 161-186), AudioManager.cs (lines 53-76). |

---

## Module Resources Used

I primarily relied on module content including:
- Lecture slides on Unity fundamentals
- Workshop materials on UI systems
- Lab sessions on 3D graphics and scripting
- Module examples of C# programming patterns

---

## How Tutorials Were Used

### Learning Approach:
1. **Concept Understanding:** I used tutorials to understand programming concepts and Unity features
2. **No Code Copying:** I never copied code directly from any tutorial
3. **Original Implementation:** All code was written from scratch based on my understanding
4. **Adaptation:** Where I learned a concept, I adapted it to fit my specific project requirements
5. **Documentation Reading:** I primarily relied on official Unity documentation rather than third-party tutorials

### Original Work Percentage: 100%
- All 17 scripts written from scratch
- All game logic designed specifically for this project
- All UI layouts created for this application
- All educational content (270+ facts) researched and written by me

---

## Academic Integrity Confirmation

I confirm that:
- ✅ No code was copied from any tutorial
- ✅ Tutorials were used only to understand concepts
- ✅ All implementations are my original work
- ✅ Where I learned a concept, I adapted it to my project's needs
- ✅ I did not follow any tutorial step-by-step
- ✅ No paid tutorials or resources were used
- ✅ All work is my own and created specifically for this coursework

---

## Key Original Contributions

1. **Comprehensive Planet Data System:** 270+ facts across 9 celestial bodies - entirely researched and implemented by me
2. **Dual-Tier Information Display:** Two-level info system (boards + detailed panels) - my own design
3. **Procedural Scene Generation:** Entire museum generated from code - my own architecture
4. **Hybrid UI System:** OnGUI fallback + Canvas UI - my own solution for reliability
5. **Event-Driven State Management:** Complete game flow with 5 states - my own design
6. **Resolution-Adaptive Scaling:** Custom scaling math for any screen size - my own implementation
7. **Starfield with Twinkle Animation:** 500 procedurally placed stars with animation - my own creation
8. **Guided Tour with Smooth Transitions:** Automated camera movement system - my own algorithm

---

**Total Development Time:** Approximately 1 month (November 15 - December 15, 2024)

**Lines of Code:** ~3,500 lines across 17 C# scripts

**Original Code Percentage:** 100%
