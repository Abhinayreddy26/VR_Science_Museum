# VR Science Museum - Solar System Explorer

An interactive educational 3D application built in Unity that allows users to explore the Solar System with comprehensive scientific information about all 9 celestial bodies (Sun + 8 planets).

![Solar System Museum](Screenshot%202025-12-09%20143037.png)

## 📋 Project Overview

This is an educational VR-style museum experience where users can:
- **Explore 9 celestial bodies** with accurate scientific data
- **Choose between two modes**: Guided Tour (automated) or Free Explore (manual)
- **Learn 270+ facts** about planets including physical data, composition, and interesting trivia
- **Interact with planets** by clicking to view detailed information panels
- **Navigate freely** using first-person WASD controls

## 🎮 Controls

| Input | Action |
|-------|--------|
| **WASD** | Move player |
| **Mouse** | Look around |
| **Left Click** | Interact with planets (show/hide info) |
| **ESC** | Pause menu |

## 🎯 Features

### Core Gameplay
- ✅ Main Menu with player name input
- ✅ Loading screen with space facts
- ✅ Two exploration modes (Guided Tour / Free Explore)
- ✅ In-game HUD showing progress (planets visited: X/9)
- ✅ Pause menu with mode switching
- ✅ Planet interaction system (click to view details)

### Educational Content
- ✅ **Comprehensive planet data**:
  - Basic Info (type, diameter, mass, moons)
  - Physical Data (temperature, atmosphere, orbital period)
  - Amazing Facts (unique characteristics, records)
- ✅ **Two-tier information display**:
  - Always-visible info boards with key facts
  - Detailed panels with complete scientific data (on click)

### Visual Features
- ✅ 500-star procedural starfield with twinkling effect
- ✅ High-quality 2K/4K planet textures
- ✅ Saturn ring system
- ✅ Emissive Sun material
- ✅ Individual planet spotlights
- ✅ Billboard labels facing camera

### Audio
- ✅ Background space ambient music
- ✅ UI sound effects (click, select, start)
- ✅ Volume controls

## 🛠️ Technical Details

### Unity Version
- **Unity 2022.3 LTS or later** (compatible with Unity 6)
- **Universal Render Pipeline (URP)**

### Architecture
- **Modular namespace organization**: Core, Planet, Environment, UI, VR, Tour
- **Singleton pattern** for all managers
- **Event-driven state management**
- **100% procedurally generated** scene (no manual setup required)

### Required Unity Packages
1. **Universal Render Pipeline (URP)** - Unity Registry
2. **Input System** - Unity Registry
3. **UI (UGUI)** - Pre-installed

### Project Structure
```
VRScienceMuseum/
├── Scripts/
│   ├── MuseumSceneSetup.cs         (Master orchestrator)
│   ├── Core/
│   │   ├── GameManager.cs          (State machine)
│   │   ├── AudioManager.cs         (Audio system)
│   │   └── UIHelper.cs             (UI utilities)
│   ├── Planet/
│   │   ├── PlanetManager.cs        (Creates 9 planets)
│   │   ├── PlanetInfoDisplay.cs    (Info panels)
│   │   ├── PlanetRotator.cs        (Rotation animation)
│   │   ├── PlanetOrbit.cs          (Orbital motion)
│   │   └── FaceCamera.cs           (Billboard effect)
│   ├── Environment/
│   │   ├── EnvironmentSetup.cs     (Space environment)
│   │   └── StarfieldGenerator.cs   (Stars + twinkle)
│   ├── VR/
│   │   └── SimplePlayerController.cs (FPS controls)
│   ├── UI/
│   │   ├── MainMenuUI.cs           (Start screen)
│   │   ├── LoadingScreenUI.cs      (Loading experience)
│   │   ├── HUDController.cs        (In-game overlay)
│   │   └── PauseMenuUI.cs          (Pause screen)
│   └── Tour/
│       └── GuidedTourController.cs (Automated tour)
├── Textures/
│   └── Planets/                    (Planet textures - see setup)
├── Audio/                          (Audio files - see setup)
├── Materials/
├── Prefabs/
├── Scenes/
└── Data/
```

## 📦 Installation & Setup

### 1. Clone Repository
```bash
git clone https://github.com/yourusername/VRScienceMuseum.git
cd VRScienceMuseum
```

### 2. Install Required Packages
Open Unity Package Manager (Window → Package Manager):
- ✅ Install **Universal Render Pipeline**
- ✅ Install **Input System**

### 3. Configure URP
- Right-click in Project → Create → Rendering → URP Asset (Forward Renderer)
- Edit → Project Settings → Graphics → Set your URP Asset

### 4. Download External Assets

#### Planet Textures (Required)
Download from [Solar System Scope Textures](https://www.solarsystemscope.com/textures/)

Place in `Assets/Resources/VRScienceMuseum/Textures/Planets/`:
- `2k_sun.jpg`
- `2k_mercury.jpg` or `Mercury_2k.png`
- `2k_venus_surface.jpg` or `Venus_2K.png`
- `2k_earth_daymap.jpg` or `Earth_4k.png`
- `2k_mars.jpg` or `Mars_2k_hq.png`
- `2k_jupiter.jpg` or `Jupiter_2k.png`
- `2k_saturn.jpg` or `Saturn_2k.png`
- `2k_saturn_ring.png` or `Saturn_Ring.png`
- `2k_uranus.jpg` or `Uranus_2k.png`
- `2k_neptune.jpg` or `Neptune_2k.png`

#### Audio Files (Optional)
Download from [Freesound.org](https://freesound.org/)

Place in `Assets/Resources/VRScienceMuseum/Audio/`:
- `space_ambient.mp3` (background music)
- `click.wav` (UI click sound)
- `select.wav` (planet selection sound)
- `start.wav` (game start sound)

### 5. Scene Setup
1. Create a new empty scene
2. Create an empty GameObject
3. Add `MuseumSceneSetup.cs` script to it
4. In Inspector, check "Setup On Start" OR right-click → "Setup Museum"
5. Press Play!

## 🎓 Educational Use

This application is designed for:
- **Museum exhibits** - Interactive kiosks
- **Schools** - Science education (ages 8-16)
- **Home learning** - Self-paced exploration
- **Planetariums** - Supplementary material

### Learning Outcomes
Students will learn about:
- Planetary sizes, distances, and compositions
- Orbital mechanics and rotation periods
- Unique characteristics of each planet
- Comparative planetology
- Space exploration facts

## 📊 Statistics

- **17 C# scripts** (~3,500 lines of code)
- **270+ educational facts** across 9 celestial bodies
- **4 UI screens** (Main Menu, Loading, HUD, Pause)
- **500 procedural stars** with animation
- **2 gameplay modes** (Guided/Free)
- **Zero paid assets** required

## 🔧 Development Notes

### Game States
1. **MainMenu** - Player enters name and chooses mode
2. **Loading** - 4-second loading with facts
3. **Playing** - Free exploration mode
4. **GuidedTour** - Automated planet tour
5. **Paused** - Pause menu with mode switching

### Code Highlights
- **Singleton managers** for global access
- **Event-driven architecture** (OnStateChanged)
- **Dual input support** (New Input System + Legacy fallback)
- **Resolution-adaptive UI** scaling
- **Procedural starfield generation**
- **Smooth camera lerping** in guided tour

## 📝 License & Attribution

### Code
- All scripts are original work created for educational purposes
- May be used for educational and non-commercial purposes

### Assets Used
See [ASSETS_USED.md](ASSETS_USED.md) for complete attribution:
- Planet textures: [Solar System Scope](https://www.solarsystemscope.com/textures/) (Free for educational use)
- Audio: [Freesound.org](https://freesound.org/) (Creative Commons)
- Unity Packages: Official Unity packages (Free)

### Tutorials Referenced
See [TUTORIALS_USED.md](TUTORIALS_USED.md) for learning resources consulted during development.

## 🐛 Troubleshooting

### Issue: Menu not appearing
- Ensure EventSystem exists in scene
- Check OnGUI fallback is working (should auto-enable)

### Issue: Planets not visible
- Verify URP is configured correctly
- Check textures are in correct Resource folder path
- Ensure camera has URP Camera Data component

### Issue: No audio
- Audio files are optional - app works without them
- Check files are in `Resources/VRScienceMuseum/Audio/`

### Issue: Input not working
- Try both New Input System and Legacy (has automatic fallback)
- Check Input System package is installed

## 👨‍💻 Author

Created as an educational project demonstrating:
- Unity game development skills
- Clean code architecture
- Educational content design
- UI/UX implementation
- 3D graphics programming

## 🚀 Future Enhancements (Potential)

- [ ] Actual VR headset support (Oculus, Vive)
- [ ] More celestial bodies (moons, asteroids, comets)
- [ ] Quiz mode to test knowledge
- [ ] Multiplayer exploration
- [ ] Orbital mechanics visualization
- [ ] Scale comparison tool
- [ ] Audio narration for facts
- [ ] Multiple languages support

---

**Built with Unity & C# | Educational Application | 2024-2025**
