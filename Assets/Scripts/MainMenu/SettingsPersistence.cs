using System;
using System.IO;
using UnityEngine;

// Created By:
// Jordi Wolthuis
// S1085303

namespace Assets.Scripts.MainMenu {
    public class SettingsPersistence : MonoBehaviour {
        public GameSettings GameSettings;
        public AudioSource MusicSource;
        public Resolution[] Resolutions;


        // Use this for initialization
        private void Start() {
            LoadSettings();
        }

        public void LoadSettings() {
            try {
                GameSettings =
                    JsonUtility.FromJson<GameSettings>(
                        File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

                AudioListener.volume = GameSettings.MusicVolume;
                QualitySettings.antiAliasing = GameSettings.Antialiasing;
                QualitySettings.vSyncCount = GameSettings.VSync;
                QualitySettings.masterTextureLimit = GameSettings.TextureQuality;
                Screen.SetResolution(GameSettings.ResolutionIndex,
                    GameSettings.ResolutionIndex, Screen.fullScreen);
                Screen.fullScreen = GameSettings.Fullscreen;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}