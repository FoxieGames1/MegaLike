using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private float footstepInterval = 0.5f;
    private float lastFootstepTime;

    private const string musicVolumeKey = "MusicVolume";
    private const string sfxVolumeKey = "SFXVolume";
    private const float initializeVolume = 0.5f;
    public float fadeDurationMusic = 1f;

    public bool IsSFXEnabled { get; internal set; }
    public bool IsMusicEnabled { get; internal set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadVolumeSettings();
        PlayMusic("Theme-Waiting");
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        SaveVolumeSettings();
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
        SaveVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat(musicVolumeKey, initializeVolume);
        float sfxVolume = PlayerPrefs.GetFloat(sfxVolumeKey, initializeVolume);

        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    private void SaveVolumeSettings()
    {
        float musicVolume = musicSource.volume;
        float sfxVolume = sfxSource.volume;

        PlayerPrefs.SetFloat(musicVolumeKey, musicVolume);
        PlayerPrefs.SetFloat(sfxVolumeKey, sfxVolume);
        PlayerPrefs.Save();
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Music not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sfx not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayRandomFootstepSound()
    {
        if (footstepSounds.Length > 0 && Time.time - lastFootstepTime > footstepInterval)
        {
            int randomIndex = UnityEngine.Random.Range(0, footstepSounds.Length);
            AudioClip randomClip = footstepSounds[randomIndex];
            sfxSource.PlayOneShot(randomClip);
            lastFootstepTime = Time.time;
        }
    }

    public void PlayMusicByScene(string sceneName)
    {
        Sound[] musicSounds = Instance.musicSounds;
        Sound newSound = Array.Find(musicSounds, x => x.sceneName == sceneName);

        if (newSound == null)
        {
            Debug.Log("Music not found for scene: " + sceneName);
            return;
        }
        Debug.Log("ReproduciendoNuevaMusica");
        StartCoroutine(CrossfadeMusic(newSound.clip));
    }

    private IEnumerator CrossfadeMusic(AudioClip newClip)
    {
        float startVolume = musicSource.volume;
        for (float t = 0.0f; t < fadeDurationMusic; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0.0f, t / fadeDurationMusic);
            yield return null;
        }
        musicSource.volume = 0.0f;

        musicSource.clip = newClip;
        musicSource.Play();
        for (float t = 0.0f; t < fadeDurationMusic; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0.0f, startVolume, t / fadeDurationMusic);
            yield return null;
        }
        musicSource.volume = startVolume;
    }
}
