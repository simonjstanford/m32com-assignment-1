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
        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.
        /// </returns>
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
        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
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

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
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

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
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