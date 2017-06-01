using System;
using System.Collections.Generic;

namespace Assets.Scripts.HighScores {

    // Created by:
    // Hugo Kamps
    // S1084074
    [Serializable]
    public class HighScores {
        public List<HighScore> HighScoresList = new List<HighScore>();
    }

    [Serializable]
    public struct HighScore {
        public string Name;
        public int Level;
        public int Points;
    }
}
