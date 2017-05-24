using System;
using UnityEngine;

namespace Assets.Scripts.Map.Weather
{
    public abstract class Weather : MonoBehaviour
    {
        public WeatherManager Manager;

        public Weather(WeatherManager manager)
        {
            Manager = manager;
        }

        public abstract void Execute(Action test);

    }
}