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
        private Int32 _score;
        private DateTime _date;

        // Methods
        public int CompareTo(Object  obj)
        {
            PlayerScore scoreToCompare = obj as PlayerScore;
            if (this.Score > scoreToCompare.Score)
                return 1;
            else if (this.Score < scoreToCompare.Score)
                return -1;
            else
                return 0;
        }

        // Properties
        public string Player
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
            }
        }

        public Int32 Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }
    }
}