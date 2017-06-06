using System;


// Created by:
// Jeffrey Wienen
// S1079065
namespace Assets.Scripts.Map.Weather {
    public interface IWeather {
        void Execute(Action test);
        bool CanBeChained();
    }
}