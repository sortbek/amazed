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
		private System.Random _random = new System.Random();

		public void Execute(Action callback)
		{
			_rain = GetComponentInChildren<ParticleSystem>();
			_light = GetComponentInChildren<Light>();

			// Start the rain
			var em = _rain.emission;
			em.rateOverTime = 10000f;

			StartCoroutine(StartThunderProgram());
		}

		private IEnumerator StartThunderProgram()
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

	}
}
