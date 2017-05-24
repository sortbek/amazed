using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.Map.Weather
{
	public class WeatherManager : MonoBehaviour
	{
		private IWeather _activeWeather;
		public IWeather ActiveWeather
		{
			get { return _activeWeather; }
			set
			{
				_activeWeather = value;
				_activeWeather.Execute(WeatherProgramDone);
			}
		}

		private readonly List<IWeather> _weatherOptions = new List<IWeather>();

		void Awake()
		{
			_weatherOptions.Add(gameObject.AddComponent<Storm>());
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
