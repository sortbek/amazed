using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBehaviour : MonoBehaviour
{

    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSyncDropdown;
    public Slider musicVolumeSlider;
    public Button ApplyButton;

    public AudioSource musicSource;
    public Resolution[] resolutions;
    public GameSettings gameSettings;

    void OnEnable(){
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        ApplyButton.onClick.AddListener(delegate { onApplyButtonClick(); });

        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));       
        }

        LoadSettings();
    }

    public void OnFullscreenToggle() {
        gameSettings.Fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange() {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.ResolutionIndex = resolutionDropdown.value;
    }

    public void OnTextureQualityChange() {
        QualitySettings.masterTextureLimit = gameSettings.TextureQuality = textureQualityDropdown.value;  
    }

    public void OnAntialiasingChange() {
        QualitySettings.antiAliasing = gameSettings.Antialiasing = (int)Mathf.Pow(2f, antialiasingDropdown.value);
    }

    public void OnVSyncChange() {
        QualitySettings.vSyncCount = gameSettings.VSync = vSyncDropdown.value;
    }

    public void OnMusicVolumeChange() {
        musicSource.volume = gameSettings.MusicVolume = musicVolumeSlider.value;
    }
    // in unity at the buildsettings you can select playersettings and change the save folder.
    public void onApplyButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings() {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings() {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        musicVolumeSlider.value = gameSettings.MusicVolume;
        antialiasingDropdown.value = gameSettings.Antialiasing;
        vSyncDropdown.value = gameSettings.VSync;
        textureQualityDropdown.value = gameSettings.TextureQuality;
        resolutionDropdown.value = gameSettings.ResolutionIndex;
        fullscreenToggle.isOn = gameSettings.Fullscreen;
        Screen.fullScreen = gameSettings.Fullscreen;

        resolutionDropdown.RefreshShownValue();
    }







}
