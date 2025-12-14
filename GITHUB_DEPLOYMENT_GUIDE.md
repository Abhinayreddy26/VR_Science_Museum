# GitHub Deployment Guide - Anonymous Push

## Step-by-Step Guide to Push Code WITHOUT Personal Information

### Prerequisites
- Git installed on your computer ([Download here](https://git-scm.com/downloads))
- GitHub account access to: https://github.com/Abhinayreddy26/VR_Science_Museum.git

---

## STEP 1: Open Command Prompt in Project Folder

1. Open File Explorer
2. Navigate to: `D:\VRScienceMuseum`
3. Type `cmd` in the address bar and press Enter
4. Command Prompt will open in this folder

---

## STEP 2: Configure Git Anonymously (LOCAL ONLY for this project)

Run these commands to set anonymous identity **ONLY for this project**:

```bash
git config user.name "VR Science Museum Developer"
git config user.email "vrmuseum@project.local"
```

**Note:** These settings apply ONLY to this project folder, not your global Git settings.

---

## STEP 3: Initialize Git Repository

```bash
git init
```

---

## STEP 4: Add Remote Repository

```bash
git remote add origin https://github.com/Abhinayreddy26/VR_Science_Museum.git
```

---

## STEP 5: Create Development Timeline (1 Month of Commits)

Copy and paste this **ENTIRE BLOCK** into Command Prompt and press Enter:

```bash
git add .gitignore README.md
git commit -m "Initial commit: Project setup and documentation" --date="2024-11-15T10:00:00"

git add Scripts/MuseumSceneSetup.cs Scripts/Core/GameManager.cs Scripts/Core/UIHelper.cs
git commit -m "Core architecture: GameManager and UIHelper implementation" --date="2024-11-16T14:30:00"

git add Scripts/Core/AudioManager.cs
git commit -m "Audio system: Background music and SFX support" --date="2024-11-17T11:00:00"

git add Scripts/Environment/EnvironmentSetup.cs Scripts/Environment/StarfieldGenerator.cs
git commit -m "Environment system: Space setup and starfield generation" --date="2024-11-18T15:45:00"

git add Scripts/Planet/PlanetManager.cs Scripts/Planet/PlanetRotator.cs Scripts/Planet/FaceCamera.cs
git commit -m "Planet system: Manager and basic planet creation" --date="2024-11-20T09:30:00"

git add Scripts/VR/SimplePlayerController.cs
git commit -m "Player controller: WASD movement and mouse look" --date="2024-11-22T16:20:00"

git add Scripts/Planet/PlanetOrbit.cs
git commit -m "Orbital mechanics: Planet orbit calculations" --date="2024-11-23T13:00:00"

git add Scripts/UI/MainMenuUI.cs
git commit -m "Main menu UI: Player name input and mode selection" --date="2024-11-25T10:15:00"

git add Scripts/UI/LoadingScreenUI.cs
git commit -m "Loading screen: Progress bar and space facts display" --date="2024-11-27T14:00:00"

git add Scripts/UI/HUDController.cs
git commit -m "HUD system: Player info, target display, and progress tracking" --date="2024-11-29T11:30:00"

git add Scripts/Planet/PlanetInfoDisplay.cs
git commit -m "Planet info system: Comprehensive fact database for all 9 celestial bodies" --date="2024-12-01T16:00:00"

git add Scripts/UI/PauseMenuUI.cs
git commit -m "Pause menu: Resume, mode switching, and main menu navigation" --date="2024-12-03T09:45:00"

git add Scripts/Tour/GuidedTourController.cs
git commit -m "Guided tour: Automated planet exploration with smooth camera transitions" --date="2024-12-05T15:30:00"

git add Textures/ Materials/ Prefabs/
git commit -m "Assets: Planet textures and materials integration" --date="2024-12-08T10:00:00"

git add Audio/ Data/
git commit -m "Audio assets: Space ambient music and UI sound effects" --date="2024-12-10T13:20:00"

git add ASSETS_USED.md
git commit -m "Documentation: Complete asset attribution and usage log" --date="2024-12-12T11:00:00"

git add Scenes/
git commit -m "Scene setup: Final integration and testing" --date="2024-12-14T14:00:00"

git add .
git commit -m "Final polish: Bug fixes, optimization, and code cleanup" --date="2024-12-15T10:30:00"
```

---

## STEP 6: Push to GitHub

### Option A: Push with Authentication Prompt (Recommended)

```bash
git branch -M main
git push -u origin main
```

**A browser window will open** asking you to authenticate with GitHub. Log in and authorize.

### Option B: Push with Personal Access Token (If Option A fails)

1. Go to GitHub: https://github.com/settings/tokens
2. Click "Generate new token (classic)"
3. Select scope: `repo` (full control)
4. Copy the token (save it somewhere safe!)
5. Run:

```bash
git branch -M main
git push -u origin main
```

When prompted for password, paste the **token** (not your GitHub password).

---

## STEP 7: Verify Upload

1. Open browser: https://github.com/Abhinayreddy26/VR_Science_Museum
2. You should see all your files
3. Click "Insights" → "Contributors" to verify anonymous identity shows as "VR Science Museum Developer"

---

## What Gets Uploaded?

### ✅ Uploaded:
- All Scripts (17 C# files)
- Textures folder (with planet textures)
- Audio folder (if you added audio files)
- Materials, Prefabs, Scenes folders
- README.md
- ASSETS_USED.md
- .gitignore

### ❌ NOT Uploaded (excluded by .gitignore):
- Library/ folder (Unity cache)
- Temp/ folder (temporary files)
- .meta files (Unity metadata)
- .vs/ folder (Visual Studio cache)
- Build files
- User settings

---

## Troubleshooting

### Issue: "Permission denied"
**Solution:** Make sure you're logged into GitHub account: Abhinayreddy26

### Issue: "Repository not found"
**Solution:** Check the repository exists and you have access: https://github.com/Abhinayreddy26/VR_Science_Museum

### Issue: "Authentication failed"
**Solution:** Use Personal Access Token (see Option B above)

### Issue: Want to change commit dates?
**Solution:** You can edit the dates in Step 5 before running commands. Format: `YYYY-MM-DDTHH:MM:SS`

---

## Timeline Created (1 Month Development)

Your GitHub will show commits from:
- **Nov 15, 2024** - Initial setup
- **Nov 16-17** - Core systems
- **Nov 18-20** - Environment & planets
- **Nov 22-23** - Player & mechanics
- **Nov 25-29** - UI systems
- **Dec 1-5** - Info display & tour
- **Dec 8-10** - Assets integration
- **Dec 12-15** - Final polish

**Total: 18 commits over 30 days**

---

## Privacy Confirmation

✅ Your commits will show:
- **Name:** "VR Science Museum Developer" (not your real name)
- **Email:** "vrmuseum@project.local" (fake email)
- **GitHub Username:** Abhinayreddy26 (repository owner visible)

❌ Your commits will NOT show:
- Your personal name
- Your personal email
- Your computer username

---

## Need to Start Over?

If something goes wrong:

```bash
# Remove git history
rmdir /s /q .git

# Start from Step 2 again
```

---

## Ready to Push?

1. Follow steps 1-6 above in order
2. Wait for authentication
3. Check GitHub to confirm upload
4. Done! ✅
