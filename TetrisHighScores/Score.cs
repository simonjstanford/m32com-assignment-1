using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisHighScores
{
    /// <summary>
    /// A class that is used to keep track of a player and their score for high score logging in Scores.xml.  Implements the IComparable interface, so that scores can be compared
    /// </summary>
    public class PlayerScore : IComparable
    {
        #region Class Fields

        private string _player; // the players' name
        private Int32 _score; // the players score
        private DateTime _date; //the date and time the score was submitted

        #endregion

        #region Class Properties

        /// <summary>
        /// Gets or sets the player's name
        /// </summary>
        /// <value>The player</value>
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
        /// Gets or sets the player' score
        /// </summary>
        /// <value>The score</value>
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
        /// Gets or sets the date/time the score was submitted
        /// </summary>
        /// <value>The date/time</value>
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

        #endregion

        #region Methods

        /// <summary>
        /// Compares two PlayerScore objects to determine which one has the highest score
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(Object obj)
        {
            PlayerScore scoreToCompare = obj as PlayerScore;
            if (this.Score > scoreToCompare.Score)
                return 1;
            else if (this.Score < scoreToCompare.Score)
                return -1;
            else
                return 0;
        }

        #endregion
    }
}