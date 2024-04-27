using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSound : MonoBehaviour
{
    public static GameManagerSound Instance;

    [Header("Settings")]
    public float defaultMusicVolume = 0.5f;

    [Header("Scene Music")]
    public AudioClip[] sceneMusic;
    private AudioSource musicSource;

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
            return;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.volume = defaultMusicVolume;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeMusic(scene.buildIndex);
    }

    public void ChangeMusic(int sceneIndex)
    {
        musicSource.Stop();

        if (sceneIndex >= 0 && sceneIndex < sceneMusic.Length && sceneMusic[sceneIndex] != null)
        {
            musicSource.clip = sceneMusic[sceneIndex];
            musicSource.Play();
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
