using System;
using System.Collections;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Map.Weather
{
	public class Storm : MonoBehaviour, IWeather
	{
		private const int ThunderInterval = 10;
		private int _duration = 20;
		private readonly System.Random _random = new System.Random();

		// Unity Components
		private ParticleSystem _rain;
		private Light _light;
		private RainCameraController _rainVision;
		private AudioSource _rainAudio;
		
		// Sub-components/modules
		private ParticleSystem.EmissionModule _rainEmission;


		// CONSTANTS settings
		private const float MaxRain = 10000f;
		private const float FadeTime = 5f;
		private const float FadeSteps = 10f;

		private bool _isInitialized;
		
		private void Init()
		{
			_rain = GetComponentInChildren<ParticleSystem>();
			_light = GetComponentInChildren<Light>();
			_rainVision = GetComponentInChildren<RainCameraController>();
			_rainAudio = _rain.gameObject.GetComponentInChildren<AudioSource>();
			_rainEmission = _rain.emission;
			_isInitialized = true;
		}
		
		public void Execute(Action callback)
		{
			if (!_isInitialized)
			{
				Init();
			}
			StartCoroutine(FadeInProgram());
		}
		
		private IEnumerator FadeInProgram()
		{
			StartCoroutine(AudioFader.FadeIn(_rainAudio, 5f, 0.5f));
			var rain = 0.1f;
			var duration = 0f;
			while (duration < FadeTime)
			{
				rain += MaxRain / (FadeTime * FadeSteps);
				_rainEmission.rateOverTime = rain;
				yield return new WaitForSeconds(1f / FadeSteps);
				duration += 1f / FadeSteps;
			}

			if (!_rainVision.enabled)
			{
				_rainVision.enabled = true;
			}
			StartCoroutine(ThunderProgram());
		}

		private IEnumerator FadeOutProgram()
		{
			yield return new WaitForSeconds(1);
			
			
			_rainVision.Stop();
			StartCoroutine(AudioFader.FadeOut(_rainAudio, 2));

			var em = _rain.emission;
			em.rateOverTime = 0f;
			_rainAudio.volume = 0;
			
		}
		
		
		private IEnumerator ThunderProgram()
		{
			var timeLeft = _duration;
			var random = new System.Random();
			while (timeLeft > 0)
			{
				var i = random.Next(0, ThunderInterval);
				yield return new WaitForSeconds(i);
				StartCoroutine(Lightning(random.Next(1,3)));
				yield return new WaitForSeconds(ThunderInterval - i);
				timeLeft -= ThunderInterval;
			}

			StartCoroutine(FadeOutProgram());
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
	}
}
