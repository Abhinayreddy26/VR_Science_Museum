using UnityEngine;

namespace VRScienceMuseum.Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private float musicVolume = 0.3f;
        [SerializeField] private float sfxVolume = 0.5f;

        private AudioSource musicSource;
        private AudioSource sfxSource;

        private void Awake()
        {
            Instance = this;
            Debug.Log("[AudioManager] Initialized");
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void Setup(Transform parent)
        {
            Debug.Log("[AudioManager] Setting up Audio System...");

            GameObject audioObj = new GameObject("AudioSystem");
            audioObj.transform.SetParent(parent);

            // Background music source
            musicSource = audioObj.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.volume = musicVolume;
            musicSource.playOnAwake = false;

            // SFX source
            GameObject sfxObj = new GameObject("SFX");
            sfxObj.transform.SetParent(audioObj.transform);
            sfxSource = sfxObj.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.volume = sfxVolume;
            sfxSource.playOnAwake = false;

            // Try to load and play background music
            LoadAndPlayMusic();

            Debug.Log("[AudioManager] Audio System ready");
        }

        private void LoadAndPlayMusic()
        {
            AudioClip musicClip = Resources.Load<AudioClip>("VRScienceMuseum/Audio/space_ambient");
            if (musicClip != null)
            {
                musicSource.clip = musicClip;
                musicSource.Play();
                Debug.Log("[AudioManager] Background music playing");
            }
            else
            {
                Debug.Log("[AudioManager] No music found. Add audio to: Resources/VRScienceMuseum/Audio/space_ambient");
            }
        }

        public void PlaySFX(string sfxName)
        {
            if (sfxSource == null) return;

            AudioClip clip = Resources.Load<AudioClip>($"VRScienceMuseum/Audio/{sfxName}");
            if (clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }

        public void PlayMusic(AudioClip clip)
        {
            if (musicSource != null && clip != null)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }

        public void StopMusic()
        {
            musicSource?.Stop();
        }

        public void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp01(volume);
            if (musicSource != null) musicSource.volume = musicVolume;
        }

        public void SetSFXVolume(float volume)
        {
            sfxVolume = Mathf.Clamp01(volume);
            if (sfxSource != null) sfxSource.volume = sfxVolume;
        }
    }
}
