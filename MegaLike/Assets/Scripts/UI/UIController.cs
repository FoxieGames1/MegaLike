using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    private const string musicVolumeKey = "MusicVolume";
    private const string sfxVolumeKey = "SFXVolume";
    public GameObject settingsPanel;
    public Button musicButton;
    public Button sfxButton;

    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Sprite sfxOnSprite;
    public Sprite sfxOffSprite;

    private Image musicButtonImage;
    private Image sfxButtonImage;
    private bool musicEnabled = true;
    private bool sfxEnabled = true;

    private void Start()
    {
        LoadVolumeSettings();
        musicButtonImage = musicButton.GetComponent<Image>();
        sfxButtonImage = sfxButton.GetComponent<Image>();
        UpdateButtonSprites();
    }

    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;
        UpdateButtonSprites();
        UpdateVolumeSettings();
    }

    public void ToggleSFX()
    {
        sfxEnabled = !sfxEnabled;
        UpdateButtonSprites();
        UpdateVolumeSettings();
    }

    private void UpdateButtonSprites()
    {
        musicButtonImage.sprite = musicEnabled ? musicOnSprite : musicOffSprite;
        sfxButtonImage.sprite = sfxEnabled ? sfxOnSprite : sfxOffSprite;

        // Habilitar o deshabilitar interactividad según el estado del sonido
        musicButton.interactable = true;
        sfxButton.interactable = true;
    }

    private void UpdateVolumeSettings()
    {
        float musicVolume = musicEnabled ? musicSlider.value : 0f;
        float sfxVolume = sfxEnabled ? sfxSlider.value : 0f;

        AudioManager.Instance.MusicVolume(musicVolume);
        AudioManager.Instance.SFXVolume(sfxVolume);

        PlayerPrefs.SetFloat(musicVolumeKey, musicVolume);
        PlayerPrefs.SetFloat(sfxVolumeKey, sfxVolume);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat(musicVolumeKey, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(sfxVolumeKey, 0.5f);

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        UpdateVolumeSettings();
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
