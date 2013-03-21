using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisHighScores
{
    public class Score : IComparable
    {
        // Fields
        private string _name;
        private int _score;
        private DateTime _time;

        // Methods
        public int CompareTo(object obj)
        {
            Score score = (Score)obj;
            int num = this._score.CompareTo(score.Score);
            if (num == 0)
            {
                num = this._score.CompareTo(score.Score);
            }
            return num;
        }

        // Properties
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
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