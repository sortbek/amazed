using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.Map.Weather
{
	public class WeatherManager : MonoBehaviour
	{
		private Weather _activeWeather;
		public Weather ActiveWeather
		{
			get { return _activeWeather; }
			set
			{
				_activeWeather = value;
				_activeWeather.Execute(WeatherProgramDone);
			}
		}

		private readonly List<Weather> _weatherOptions = new List<Weather>();

		void Awake()
		{
			_weatherOptions.Add(new Storm(this));
		}

		public void Init ()
		{
			SetRandomWeather();
		}

		private void SetRandomWeather()
		{
			var i = GameManager.Instance.GetRandom(0, _weatherOptions.Count);
			ActiveWeather = _weatherOptions[i];
		}

		private void WeatherProgramDone()
		{
			SetRandomWeather();
		}
	}
}
