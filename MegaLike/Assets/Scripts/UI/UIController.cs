using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    private const string musicVolumeKey = "MusicVolume";
    private const string sfxVolumeKey = "SFXVolume";
    public GameObject settingsPanel;


    private void Start()
    {
        LoadVolumeSettings();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsPanel();
        }
    }

    private void LoadVolumeSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat(musicVolumeKey, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(sfxVolumeKey, 0.5f);

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        AudioManager.Instance.MusicVolume(musicVolume);
        AudioManager.Instance.SFXVolume(sfxVolume);
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        float volume = musicSlider.value;
        AudioManager.Instance.MusicVolume(volume);
        PlayerPrefs.SetFloat(musicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SFXVolume()
    {
        float volume = sfxSlider.value;
        AudioManager.Instance.SFXVolume(volume);
        PlayerPrefs.SetFloat(sfxVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void ClickOut()
    {
        AudioManager.Instance.PlaySFX("ClickOut");
    }

    public void Click()
    {
        AudioManager.Instance.PlaySFX("Click");
    }

    private void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);

        if (settingsPanel.activeSelf)
        {
            GameManager.Instance.PauseGame();
        }
        else
        {
            GameManager.Instance.ResumeGame();
        }
    }
    public void ResumeGameFromSettings()
    {
        settingsPanel.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}
