using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip[] backgroundMusic;
    private AudioSource backgroundMusicSource;
    private bool isMusicPlaying = false;

    private const string musicVolumeParameter = "Background";

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

        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.loop = true;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (!isMusicPlaying)
        {
            if (backgroundMusic.Length > 0)
            {
                int randomIndex = Random.Range(0, backgroundMusic.Length);
                AudioClip randomClip = backgroundMusic[randomIndex];
                backgroundMusicSource.clip = randomClip;
                backgroundMusicSource.Play();
                isMusicPlaying = true;
            }
            else
            {
                Debug.LogWarning("No background music available!");
            }
        }
    }

    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
        isMusicPlaying = false;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(musicVolumeParameter, volume);
    }
}
