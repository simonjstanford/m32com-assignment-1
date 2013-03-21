using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisHighScores
{
    public class PlayerScore : IComparable
    {
        // Fields
        private string _player;
        private int _score;
        private DateTime _time;

        // Methods
        public int CompareTo(object obj)
        {
            PlayerScore score = (PlayerScore)obj;
            int num = this._score.CompareTo(score.Score);
            if (num == 0)
            {
                num = this._score.CompareTo(score.Score);
            }
            return num;
        }

        // Properties
        public string Player
        {
            get
            {
                return this._player;
            }
            set
            {
                this._player = value;
            }
        }

        public int Score
        {
            get
            {
                return this._score;
            }
            set
            {
                this._score = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return this._time;
            }
            set
            {
                this._time = value;
            }
        }
    }
}