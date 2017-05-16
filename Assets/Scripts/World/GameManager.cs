using UnityEngine;

namespace Assets.Scripts.World
{
    public class GameManager : Singleton<GameManager>
    {
        public string GameSeed;
        public int Size;
        public bool RandomSeed;
        public bool Debug;

        // Game stuff
        public Transform EndPoint;

        private System.Random _random;

        public int GetRandom(int min = 0, int max = 0)
        {
            if (_random == null)
            {
                _random = new System.Random(GameSeed.GetHashCode());
            }
            if (min == 0 & max == 0)
            {
                return _random.Next();
            }
            return max == 0 ? _random.Next(min) : _random.Next(min, max);
        }

        public Vector3 GetEndpoint()
        {
            return new Vector3((Size - 1) * 12, 0, Size * 12);
        }

    }
}
