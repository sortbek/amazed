using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Map.Weather
{
	public class Storm : MonoBehaviour, IWeather
	{
		private const int ThunderInterval = 10;
		private int _duration = 30;
		private ParticleSystem _rain;
		private Light _light;
		private readonly System.Random _random = new System.Random();
		private RainCameraController _rainVision;
		private AudioSource _rainAudio;

		
		public void Execute(Action callback)
		{
			_rain = GetComponentInChildren<ParticleSystem>();
			_light = GetComponentInChildren<Light>();
			_rainVision = GetComponentInChildren<RainCameraController>();
			_rainAudio = _rain.gameObject.GetComponentInChildren<AudioSource>();
			
			// Start the rain
			var em = _rain.emission;
			em.rateOverTime = 10000f;

			// Start the visual 'on screen' effect of the rain.
			// 
			if (!_rainVision.enabled)
			{
				_rainVision.enabled = true;
			}

			_rainAudio.Play();
			StartCoroutine(FadeIn(_rainAudio, 1));
			
			StartCoroutine(StartThunderProgram(callback));
		}

		private IEnumerator StartThunderProgram(Action callback)
		{
			var random = new System.Random();
			while (_duration > 0)
			{
				var i = random.Next(0, ThunderInterval);
				yield return new WaitForSeconds(i);
				Thunder();
				yield return new WaitForSeconds(ThunderInterval - i);
				_duration -= i;
			}
			Stop();
			callback();
		}

		private void Stop()
		{
			_rainVision.Stop();	
		}

		private void Thunder()
		{
			StartCoroutine(Lightning(3));
		}

		private IEnumerator Lightning(int i)
		{
			var intens = _random.Next(0, 8);
			while (i > 0)
			{
				i--;

				yield return new WaitForSeconds((float)_random.Next(3, 7) / 10);
				intens = _random.Next(2, 7);
				_light.intensity = intens;
			}
			_light.intensity = intens/2;

			yield return new WaitForSeconds(0.2f);
			_light.intensity = 0;
		}
		
		public static IEnumerator FadeIn (AudioSource audioSource, float FadeTime) {
			var startVolume = 0.1f;
 
			while (audioSource.volume < 0.5) {
				audioSource.volume += startVolume * Time.deltaTime / FadeTime;
 
				yield return null;
			}
		}
		
		public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
			float startVolume = audioSource.volume;
 
			while (audioSource.volume > 0) {
				audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
				yield return null;
			}
 
			audioSource.Stop ();
			audioSource.volume = startVolume;
		}
	}
}
