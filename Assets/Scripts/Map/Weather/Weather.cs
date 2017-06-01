using System;

/*
    File owner: Jeffrey Wienen
    Created by:
    Jeffrey Wienen     s1079065 
*/
namespace Assets.Scripts.Map.Weather
{
    public interface IWeather
    {
        void Execute(Action test);
        bool CanBeChained();
    }
}