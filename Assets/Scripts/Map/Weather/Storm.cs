using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Map.Weather
{
	public class Storm : Weather
	{

		private int _thunderInterval = 10;
		private int _duration = 30;
		private ParticleSystem _rain;

		public Storm(WeatherManager manager) : base(manager)
		{
		}

		public override void Execute(Action callback)
		{
			_rain = Manager.GetComponentInChildren<ParticleSystem>();

			// Start the rain
			var em = _rain.emission;
			em.rateOverTime = 5000f;

			StartCoroutine(StartThunderProgram());

		}

		private IEnumerator StartThunderProgram()
		{
			var random = new Random();
			while (_duration > 0)
			{

				yield return new WaitForSeconds(Random.Range(0,1));
			}
		}


	}
}
