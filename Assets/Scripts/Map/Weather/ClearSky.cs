using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Map.Weather
{
    public class ClearSky: MonoBehaviour, IWeather
    {
        private const float ProgramDuration = 10f;
        public void Execute(Action callback)
        {
            StartCoroutine(Program(callback));
        }

        private IEnumerator Program(Action callback)
        {
            yield return new WaitForSeconds(ProgramDuration);

            callback();
        }
    }
}