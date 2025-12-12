# Assets and Tutorials Used Log

**Student Name:** [Enter your name]

**Student Number:** [Enter your student number]

**Project:** VR Science Museum - Solar System Explorer

**Submission Date:** December 2024

---

## Assets Used

Below is a complete list of all pre-made assets (e.g., 3D models, images, animations, effects, code, etc.) used in this 3D application.

| Asset Name | Asset Publisher | Asset Location & Link | Description of Usage |
|------------|----------------|----------------------|---------------------|
| **Solar System Textures (2K/4K)** | Solar System Scope | **Website:** [Solar System Scope Textures](https://www.solarsystemscope.com/textures/)<br>**License:** Free for personal and educational use | Used high-resolution planet textures (2k_sun.jpg, 2k_earth_daymap.jpg, 2k_mars.jpg, 2k_jupiter.jpg, 2k_saturn.jpg, 2k_uranus.jpg, 2k_neptune.jpg, etc.) in `PlanetManager.cs` lines 147-186 to wrap around procedurally generated sphere primitives for realistic planet appearance. |
| **Universal Render Pipeline (URP)** | Unity Technologies | **Unity Registry:** [URP Documentation](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest)<br>**License:** Unity Companion License | Used throughout the project for rendering. Specifically used "Universal Render Pipeline/Unlit" shader in `PlanetManager.cs` (line 144), `StarfieldGenerator.cs` (line 63), and camera setup in `SimplePlayerController.cs` (lines 96-98) to create consistent space environment rendering with proper URP Camera Data component. |
| **Input System Package** | Unity Technologies | **Unity Registry:** [Input System Documentation](https://docs.unity3d.com/Packages/com.unity.inputsystem@latest)<br>**License:** Unity Companion License | Used in `SimplePlayerController.cs` (lines 144-157, 184-196, 242-246) for WASD movement and mouse look controls. Implemented alongside legacy input fallback for compatibility. Also used in `MuseumSceneSetup.cs` (lines 171-181) for EventSystem setup. |
| **Legacy/System Fonts** | Microsoft / OS Built-in | **Operating System:** Built-in system fonts<br>**License:** OS License | Used in `UIHelper.cs` (lines 277-328) with fallback loading system. Code attempts to load Unity built-in fonts (LegacyRuntime.ttf, Arial.ttf) and system fonts (Arial, Segoe UI, Helvetica, Verdana) to ensure UI text renders correctly without external font assets. Used across all UI components. |
| **Saturn Ring Texture** | Solar System Scope | **Website:** [Solar System Scope Textures](https://www.solarsystemscope.com/textures/)<br>**License:** Free for personal and educational use | Used in `PlanetManager.cs` (lines 197-224) specifically line 211 to texture the procedurally generated cylinder primitive that forms Saturn's rings. Loaded from Resources path "VRScienceMuseum/Textures/Planets/Saturn_Ring". |
| **Unity Primitives** | Unity Technologies | **Unity Engine:** Built-in primitives<br>**License:** Unity Companion License | Used extensively throughout the project:<br>- `PrimitiveType.Sphere` in `PlanetManager.cs` (line 108) for planets and `StarfieldGenerator.cs` (line 49) for stars<br>- `PrimitiveType.Plane` in `EnvironmentSetup.cs` (line 45) for floor<br>- `PrimitiveType.Cylinder` in `PlanetManager.cs` (line 199) for Saturn rings<br>- `PrimitiveType.Quad` in `PlanetInfoDisplay.cs` (lines 302, 357) for info boards |
| **Unity Physics System** | Unity Technologies | **Unity Engine:** Built-in physics<br>**License:** Unity Companion License | Used CharacterController component in `SimplePlayerController.cs` (lines 107-111) for player movement and collision. Used Physics.Raycast in `SimplePlayerController.cs` (line 225) and `HUDController.cs` (line 185) for planet interaction detection. |
| **Unity UI (UGUI)** | Unity Technologies | **Unity Engine:** Built-in UI system<br>**License:** Unity Companion License | Used extensively across all UI scripts:<br>- Canvas, Text, Button, Image, InputField components in `UIHelper.cs` (entire file)<br>- Used in `MainMenuUI.cs`, `HUDController.cs`, `PauseMenuUI.cs`, `LoadingScreenUI.cs` for all screen-space UI rendering |
| **Unity Audio System** | Unity Technologies | **Unity Engine:** Built-in audio<br>**License:** Unity Companion License | AudioSource and AudioListener components used in `AudioManager.cs` (lines 34-45) for background music and sound effects playback. AudioClip loading from Resources in lines 55-65. |
| **Space Audio (Optional)** | Freesound.org Community | **Website:** [Freesound.org](https://freesound.org/)<br>**License:** Creative Commons (varies by file) | Optional ambient space music and UI sound effects (click, select, start) loaded in `AudioManager.cs` (lines 55-76). Application works without audio files. |

---

## Development Approach

### Original Work
All C# scripts (17 files, ~3,500 lines of code) were written from scratch specifically for this coursework:
- `MuseumSceneSetup.cs` - Master orchestration
- `GameManager.cs`, `AudioManager.cs`, `UIHelper.cs` - Core systems
- `PlanetManager.cs`, `PlanetInfoDisplay.cs`, `PlanetRotator.cs`, etc. - Planet systems
- `EnvironmentSetup.cs`, `StarfieldGenerator.cs` - Environment
- `SimplePlayerController.cs` - Player controls
- All UI scripts - Menu systems
- `GuidedTourController.cs` - Automated tour

### Procedural Generation
The entire scene is generated programmatically - no manual scene setup required. Everything is created from code using Unity primitives and components.

### No Copied Code
No code was copied from tutorials or external sources. All implementations were written based on understanding of Unity fundamentals and documentation.

---

## Academic Integrity Statement

I confirm that:
- ✅ All scripts were written by me specifically for this coursework
- ✅ I started from an empty Unity project
- ✅ I used only pre-made graphical assets (textures) as permitted
- ✅ I did not copy code from tutorials or online resources
- ✅ I did not use paid tutorials or learning resources
- ✅ All tutorial references below were used only to understand concepts, not copy implementation
- ✅ I have properly attributed all assets used above

**Signed:** _____________________ **Date:** _____________
