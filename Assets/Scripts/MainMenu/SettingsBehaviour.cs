using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Created By:
// Jordi Wolthuis
// S1085303

public class SettingsBehaviour : MonoBehaviour {
    public Dropdown AntialiasingDropdown;
    public Button ApplyButton;

    public Toggle FullscreenToggle;
    public GameSettings GameSettings;

    public AudioSource MusicSource;
    public Slider MusicVolumeSlider;
    public Dropdown ResolutionDropdown;
    public Resolution[] Resolutions;
    public Dropdown TextureQualityDropdown;
    public Dropdown VSyncDropdown;

    private void OnEnable() {
        GameSettings = new GameSettings();

        MusicVolumeSlider.value = 100;

        FullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        ResolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        TextureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        AntialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        VSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        MusicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        ApplyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

        Resolutions = Screen.resolutions;
        foreach (var resolution in Resolutions)
            ResolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));

        LoadSettings();
    }

    public void OnFullscreenToggle() {
        GameSettings.Fullscreen = Screen.fullScreen = FullscreenToggle.isOn;
    }

    public void OnResolutionChange() {
        Screen.SetResolution(Resolutions[ResolutionDropdown.value].width, Resolutions[ResolutionDropdown.value].height,
            Screen.fullScreen);
        GameSettings.ResolutionIndex = ResolutionDropdown.value;
    }

    public void OnTextureQualityChange() {
        QualitySettings.masterTextureLimit = GameSettings.TextureQuality = TextureQualityDropdown.value;
    }

    public void OnAntialiasingChange() {
        QualitySettings.antiAliasing = GameSettings.Antialiasing = (int) Mathf.Pow(2f, AntialiasingDropdown.value);
    }

    public void OnVSyncChange() {
        QualitySettings.vSyncCount = GameSettings.VSync = VSyncDropdown.value;
    }

    public void OnMusicVolumeChange() {
        AudioListener.volume = GameSettings.MusicVolume = MusicVolumeSlider.value;
    }

    // in unity at the buildsettings you can select playersettings and change the save folder.
    public void OnApplyButtonClick() {
        SaveSettings();
    }

    public void SaveSettings() {
        var jsonData = JsonUtility.ToJson(GameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings() {
        try {
            GameSettings =
                JsonUtility.FromJson<GameSettings>(
                    File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

            MusicVolumeSlider.value = GameSettings.MusicVolume;
            AntialiasingDropdown.value = GameSettings.Antialiasing;
            VSyncDropdown.value = GameSettings.VSync;
            TextureQualityDropdown.value = GameSettings.TextureQuality;
            ResolutionDropdown.value = GameSettings.ResolutionIndex;
            FullscreenToggle.isOn = GameSettings.Fullscreen;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }

        Screen.fullScreen = GameSettings.Fullscreen;

        ResolutionDropdown.RefreshShownValue();
    }
}