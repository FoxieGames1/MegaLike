using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;

    private const string musicVolumeParameter = "Background";
    private const string soundVolumeParameter = "Effects";

    [SerializeField] private SoundManager soundManager;

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
    }

    private void Start()
    {
        SetMusicVolume(musicVolumeSlider.value);
        SetSoundVolume(soundVolumeSlider.value);

        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        soundVolumeSlider.onValueChanged.AddListener(SetSoundVolume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(musicVolumeParameter, volume);
        Debug.Log("Volumen de música ajustado a: " + volume);
        soundManager.SetMusicVolume(volume);
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat(soundVolumeParameter, volume);
        Debug.Log("Volumen de efectos de sonido ajustado a: " + volume);
    }
}
