using Assets.Scripts.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SettingsPersistence : MonoBehaviour {

    public GameSettings gameSettings;
    public AudioSource musicSource;
    public Resolution[] resolutions;


    // Use this for initialization
    void Start () {
		LoadSettings();
    }

    public void LoadSettings() {
        try {
            gameSettings =
                JsonUtility.FromJson<GameSettings>(
                    File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

            musicSource.volume = gameSettings.musicVolume;
            QualitySettings.antiAliasing = gameSettings.antialiasing;
            QualitySettings.vSyncCount = gameSettings.vSync;
            QualitySettings.masterTextureLimit = gameSettings.textureQuality;
            Screen.SetResolution(gameSettings.resolutionIndex, 
                gameSettings.resolutionIndex, Screen.fullScreen);
            Screen.fullScreen = gameSettings.fullscreen;

        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }

    }

}
