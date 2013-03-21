using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;

//Singleton class to hold scores from game 

namespace TetrisHighScores
{
    public class ScoreController
    {
        // Fields
        private static ScoreController _instance;
        private string FileName = "Scores.xml";

        // Methods
        private ScoreController()
        {
        }

        public bool Add(string name,int score)
        {
            return true;
        }

        public DataTable getScoreTable()
        {
            return new DataTable();
        }

        public void setLocation(string Location)
        {
            this.FileName = Location;
        }

        // Properties
        public static ScoreController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScoreController();
                }
                return _instance;
            }
        }
    }
}

