using System;
using System.IO;
using UnityEngine;

// Created By:
// Jordi Wolthuis
// S1085303

namespace Assets.Scripts.MainMenu {
    public class SettingsPersistence : MonoBehaviour {
        public GameSettings gameSettings;
        public AudioSource musicSource;
        public Resolution[] resolutions;


        // Use this for initialization
        private void Start() {
            LoadSettings();
        }

        public void LoadSettings() {
            try {
                gameSettings =
                    JsonUtility.FromJson<GameSettings>(
                        File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

                AudioListener.volume = gameSettings.musicVolume;
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
}