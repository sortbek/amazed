using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine;
using Random = System.Random;

// Created by:
// Jeffrey Wienen
// S1079065
namespace Assets.Scripts.Map.Weather {
    public class Storm : MonoBehaviour, IWeather {
        // CONSTANTS settings
        private const float ProgramDuration = 60f;
        private const float MaxRain = 10000f;
        private const float FadeTime = 5f;
        private const float FadeSteps = 10f;
        private const int ThunderInterval = 10;
        private readonly Random _random = new Random();
        private AudioSource[] _audioList;
        
        private Action _callback;
        private bool _isInitialized;
        private Light _light;

        // Unity Components
        private ParticleSystem _rain;
        private AudioSource _rainAudio;

        // Sub-components/modules
        private ParticleSystem.EmissionModule _rainEmission;

        private RainCameraController _rainVision;

        public void Execute(Action callback) {
            if (!_isInitialized)
                Init();
            _callback = callback;
            StartCoroutine(FadeInProgram());
        }

        public bool CanBeChained() {
            return false;
        }

        private void Init() {
            _rain = GetComponentInChildren<ParticleSystem>();
            _light = GetComponentInChildren<Light>();
            _rainVision = GetComponentInChildren<RainCameraController>();
            _rainAudio = _rain.gameObject.GetComponentInChildren<AudioSource>();
            _rainEmission = _rain.emission;
            _isInitialized = true;
            _audioList = GameObject.Find("Thunder").GetComponentsInChildren<AudioSource>();
        }

        private IEnumerator FadeInProgram() {
            StartCoroutine(AudioFader.FadeIn(_rainAudio, FadeTime, 0.2f));
            var rain = 0.1f;
            var duration = 0f;
            while (duration < FadeTime) {
                rain += MaxRain / (FadeTime * FadeSteps);
                _rainEmission.rateOverTime = rain;
                yield return new WaitForSeconds(1f / FadeSteps);
                duration += 1f / FadeSteps;
            }
            if (!_rainVision.enabled)
                _rainVision.enabled = true;
            else
                _rainVision.Play();
            StartCoroutine(ThunderProgram());
        }

        private IEnumerator FadeOutProgram() {
            StartCoroutine(AudioFader.FadeOut(_rainAudio, FadeTime));
            var duration = 0f;
            var rain = MaxRain;
            while (duration < FadeTime) {
                rain -= MaxRain / (FadeTime * FadeSteps);
                _rainEmission.rateOverTime = rain;
                yield return new WaitForSeconds(1f / FadeSteps);
                duration += 1f / FadeSteps;
            }

            _rainVision.Stop();
            _callback();
        }


        private IEnumerator ThunderProgram() {
            var timeLeft = ProgramDuration;
            var random = new Random();
            while (timeLeft > 0) {
                var i = random.Next(0, ThunderInterval);
                yield return new WaitForSeconds(i);
                StartCoroutine(Lightning(random.Next(1, 3)));
                yield return new WaitForSeconds(ThunderInterval - i);
                timeLeft -= ThunderInterval;
            }

            StartCoroutine(FadeOutProgram());
        }

        private IEnumerator Lightning(int i) {
            var intens = _random.Next(0, 8);
            while (i > 0) {
                i--;
                yield return new WaitForSeconds((float) _random.Next(3, 7) / 10);
                intens = _random.Next(2, 7);
                _light.intensity = intens;
                PlayThunder();
            }
            _light.intensity = intens / 2;

            yield return new WaitForSeconds(0.2f);
            _light.intensity = 0;
        }


        private void PlayThunder()
        {
            var i = _random.Next(0, _audioList.Length);
            //print("Playing: " + i);
            _audioList[i].Play();
        }
    }
}