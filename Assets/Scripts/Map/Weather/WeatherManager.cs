using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.Map.Weather
{
	public class WeatherManager : MonoBehaviour
	{
		private ParticleSystem _rainSystem;
		private ParticleSystem.ShapeModule _rainSystemShape;
		private readonly System.Random _random = new System.Random();

		public bool Start;
		
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
			_weatherOptions.Add(gameObject.AddComponent<ClearSky>());
			
			_rainSystem = GetComponentInChildren<ParticleSystem>();
			_rainSystemShape = _rainSystem.shape;
		}

		public void Init ()
		{
			InitEnv();
			SetRandomWeather();
		}

		private void InitEnv()
		{
			// Set cloud (particleSystem) size to fit the map
			var size = GameManager.Instance.Size * 12;
			_rainSystemShape.box = new Vector3(size + 80,size + 80);

		}

		private int GetNewRandom()
		{
			var i = _random.Next(0, _weatherOptions.Count);
			return _weatherOptions[i] != ActiveWeather ? i : GetNewRandom();
		}

		private void SetRandomWeather()
		{
			ActiveWeather = _weatherOptions[GetNewRandom()];
		}

		private void WeatherProgramDone()
		{
			SetRandomWeather();
		}
	}
}
