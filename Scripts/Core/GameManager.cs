using UnityEngine;
using System;

namespace VRScienceMuseum.Core
{
    public enum GameState { MainMenu, Loading, Playing, Paused, GuidedTour }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState CurrentState { get; private set; } = GameState.MainMenu;
        public string PlayerName { get; set; } = "Explorer";
        public bool IsGuidedTour { get; set; } = false;
        public int PlanetsVisited { get; set; } = 0;

        public event Action<GameState> OnStateChanged;

        private void Awake()
        {
            // Always take over as Instance - previous one may be destroyed
            Instance = this;
            Debug.Log("[GameManager] Initialized (Instance set)");
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
                Debug.Log("[GameManager] Instance cleared");
            }
        }

        private void Start()
        {
            // Delay state change to ensure UI is ready
            Invoke(nameof(InitialStateChange), 0.1f);
        }

        private void InitialStateChange()
        {
            Debug.Log("[GameManager] InitialStateChange called, firing MainMenu state");
            ChangeState(GameState.MainMenu);
        }

        public void ChangeState(GameState newState)
        {
            if (CurrentState == newState && CurrentState != GameState.MainMenu) return;

            GameState oldState = CurrentState;
            CurrentState = newState;

            Debug.Log($"[GameManager] State: {oldState} -> {newState}");

            // Handle time scale
            Time.timeScale = (newState == GameState.Paused) ? 0f : 1f;

            // Handle cursor - show cursor for menu states, hide for gameplay
            bool showCursor = (newState == GameState.MainMenu ||
                              newState == GameState.Loading ||
                              newState == GameState.Paused);

            Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = showCursor;

            // Notify listeners
            int listenerCount = OnStateChanged?.GetInvocationList()?.Length ?? 0;
            Debug.Log($"[GameManager] Notifying {listenerCount} listeners of state change to {newState}");
            OnStateChanged?.Invoke(newState);
        }

        public void StartGame(bool guidedTour, string playerName)
        {
            PlayerName = string.IsNullOrWhiteSpace(playerName) ? "Explorer" : playerName;
            IsGuidedTour = guidedTour;
            PlanetsVisited = 0;

            Debug.Log($"[GameManager] Starting - Player: {PlayerName}, Mode: {(guidedTour ? "Guided Tour" : "Free Explore")}");

            ChangeState(GameState.Loading);
        }

        public void ResumeGame()
        {
            ChangeState(IsGuidedTour ? GameState.GuidedTour : GameState.Playing);
        }

        public void PauseGame()
        {
            if (CurrentState == GameState.Playing || CurrentState == GameState.GuidedTour)
            {
                ChangeState(GameState.Paused);
            }
        }

        public void SwitchMode()
        {
            IsGuidedTour = !IsGuidedTour;
            Debug.Log($"[GameManager] Mode switched to: {(IsGuidedTour ? "Guided Tour" : "Free Explore")}");
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1f;
            IsGuidedTour = false;
            PlanetsVisited = 0;
            ChangeState(GameState.MainMenu);
        }

        public void VisitPlanet(string planetName)
        {
            PlanetsVisited++;
            Debug.Log($"[GameManager] Visited: {planetName} (Total: {PlanetsVisited}/9)");
        }

        public void ExitGame()
        {
            Debug.Log("[GameManager] Exiting game...");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
