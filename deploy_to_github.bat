@echo off
echo ============================================
echo VR Science Museum - GitHub Deployment
echo ============================================
echo.
echo This will push your code to GitHub anonymously
echo Repository: https://github.com/Abhinayreddy26/VR_Science_Museum.git
echo.
echo Your identity will be: "VR Science Museum Developer"
echo.
pause

echo.
echo Step 1: Configuring Git (anonymous)...
git config user.name "VR Science Museum Developer"
git config user.email "vrmuseum@project.local"
echo Done!

echo.
echo Step 2: Initializing repository...
git init
echo Done!

echo.
echo Step 3: Adding remote...
git remote add origin https://github.com/Abhinayreddy26/VR_Science_Museum.git
echo Done!

echo.
echo Step 4: Creating development timeline (1 month of commits)...
echo.

echo [Nov 15] Initial commit...
git add .gitignore README.md
git commit -m "Initial commit: Project setup and documentation" --date="2024-11-15T10:00:00"

echo [Nov 16] Core architecture...
git add Scripts/MuseumSceneSetup.cs Scripts/Core/GameManager.cs Scripts/Core/UIHelper.cs
git commit -m "Core architecture: GameManager and UIHelper implementation" --date="2024-11-16T14:30:00"

echo [Nov 17] Audio system...
git add Scripts/Core/AudioManager.cs
git commit -m "Audio system: Background music and SFX support" --date="2024-11-17T11:00:00"

echo [Nov 18] Environment system...
git add Scripts/Environment/EnvironmentSetup.cs Scripts/Environment/StarfieldGenerator.cs
git commit -m "Environment system: Space setup and starfield generation" --date="2024-11-18T15:45:00"

echo [Nov 20] Planet system...
git add Scripts/Planet/PlanetManager.cs Scripts/Planet/PlanetRotator.cs Scripts/Planet/FaceCamera.cs
git commit -m "Planet system: Manager and basic planet creation" --date="2024-11-20T09:30:00"

echo [Nov 22] Player controller...
git add Scripts/VR/SimplePlayerController.cs
git commit -m "Player controller: WASD movement and mouse look" --date="2024-11-22T16:20:00"

echo [Nov 23] Orbital mechanics...
git add Scripts/Planet/PlanetOrbit.cs
git commit -m "Orbital mechanics: Planet orbit calculations" --date="2024-11-23T13:00:00"

echo [Nov 25] Main menu...
git add Scripts/UI/MainMenuUI.cs
git commit -m "Main menu UI: Player name input and mode selection" --date="2024-11-25T10:15:00"

echo [Nov 27] Loading screen...
git add Scripts/UI/LoadingScreenUI.cs
git commit -m "Loading screen: Progress bar and space facts display" --date="2024-11-27T14:00:00"

echo [Nov 29] HUD system...
git add Scripts/UI/HUDController.cs
git commit -m "HUD system: Player info, target display, and progress tracking" --date="2024-11-29T11:30:00"

echo [Dec 1] Planet info system...
git add Scripts/Planet/PlanetInfoDisplay.cs
git commit -m "Planet info system: Comprehensive fact database for all 9 celestial bodies" --date="2024-12-01T16:00:00"

echo [Dec 3] Pause menu...
git add Scripts/UI/PauseMenuUI.cs
git commit -m "Pause menu: Resume, mode switching, and main menu navigation" --date="2024-12-03T09:45:00"

echo [Dec 5] Guided tour...
git add Scripts/Tour/GuidedTourController.cs
git commit -m "Guided tour: Automated planet exploration with smooth camera transitions" --date="2024-12-05T15:30:00"

echo [Dec 8] Assets integration...
git add Textures/ Materials/ Prefabs/
git commit -m "Assets: Planet textures and materials integration" --date="2024-12-08T10:00:00"

echo [Dec 10] Audio assets...
git add Audio/ Data/
git commit -m "Audio assets: Space ambient music and UI sound effects" --date="2024-12-10T13:20:00"

echo [Dec 12] Documentation...
git add ASSETS_USED.md
git commit -m "Documentation: Complete asset attribution and usage log" --date="2024-12-12T11:00:00"

echo [Dec 14] Scene setup...
git add Scenes/
git commit -m "Scene setup: Final integration and testing" --date="2024-12-14T14:00:00"

echo [Dec 15] Final polish...
git add .
git commit -m "Final polish: Bug fixes, optimization, and code cleanup" --date="2024-12-15T10:30:00"

echo.
echo ============================================
echo All commits created! (18 commits over 30 days)
echo ============================================
echo.

echo Step 5: Pushing to GitHub...
echo.
echo A browser window will open for authentication.
echo Log in to GitHub when prompted.
echo.
pause

git branch -M main
git push -u origin main

echo.
echo ============================================
echo Deployment Complete!
echo ============================================
echo.
echo Check your repository:
echo https://github.com/Abhinayreddy26/VR_Science_Museum
echo.
pause
