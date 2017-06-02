using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Created By:
// Jordi Wolthuis
// S1085303

public class SettingsBehaviour : MonoBehaviour {
    public Dropdown antialiasingDropdown;
    public Button ApplyButton;

    public Toggle fullscreenToggle;
    public GameSettings gameSettings;

    public AudioSource musicSource;
    public Slider musicVolumeSlider;
    public Dropdown resolutionDropdown;
    public Resolution[] resolutions;
    public Dropdown textureQualityDropdown;
    public Dropdown vSyncDropdown;

    private void OnEnable() {
        gameSettings = new GameSettings();

        musicVolumeSlider.value = 100;

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        ApplyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

        resolutions = Screen.resolutions;
        foreach (var resolution in resolutions)
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));

        LoadSettings();
    }

    public void OnFullscreenToggle() {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange() {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height,
            Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnTextureQualityChange() {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;
    }

    public void OnAntialiasingChange() {
        QualitySettings.antiAliasing = gameSettings.antialiasing = (int) Mathf.Pow(2f, antialiasingDropdown.value);
    }

    public void OnVSyncChange() {
        QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.value;
    }

    public void OnMusicVolumeChange() {
        musicSource.volume = gameSettings.musicVolume = musicVolumeSlider.value;
    }

    // in unity at the buildsettings you can select playersettings and change the save folder.
    public void OnApplyButtonClick() {
        SaveSettings();
    }

    public void SaveSettings() {
        var jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings() {
        try {
            gameSettings =
                JsonUtility.FromJson<GameSettings>(
                    File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

            musicVolumeSlider.value = gameSettings.musicVolume;
            antialiasingDropdown.value = gameSettings.antialiasing;
            vSyncDropdown.value = gameSettings.vSync;
            textureQualityDropdown.value = gameSettings.textureQuality;
            resolutionDropdown.value = gameSettings.resolutionIndex;
            fullscreenToggle.isOn = gameSettings.fullscreen;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }

        Screen.fullScreen = gameSettings.fullscreen;

        resolutionDropdown.RefreshShownValue();
    }
}