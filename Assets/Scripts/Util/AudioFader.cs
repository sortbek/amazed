using System.Collections;
using UnityEngine;

// Created by:
// Jeffrey Wienen
// S1079065
namespace Assets.Scripts.Util {
    public static class AudioFader {
        public static IEnumerator FadeIn(AudioSource audioSource, float fadeTime, float maxVolume) {
            audioSource.Play();
            audioSource.volume = 0.0001f;
            while (audioSource.volume < maxVolume) {
                var t = maxVolume / (fadeTime / Time.deltaTime);
                audioSource.volume += t;
                yield return null;
            }
        }

        public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime) {
            var startVolume = audioSource.volume;

            while (audioSource.volume > 0) {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }
    }
}