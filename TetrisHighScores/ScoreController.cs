using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Security.Cryptography;
using System.Configuration;

//Singleton class to hold top 12 scores scores from game 

namespace TetrisHighScores
{
    public class ScoreController
    {
        #region Class Fields

        private static ScoreController _instance; //the instance of ScoreController used in the Singleton pattern
        private string FileName = "Scores.xml"; //the filename of the XML file that stores the scores
        private const int SIZE = 12; //the number of high scores to keep 


        //Used in data table and XML file to keep field names in sync between read and write
        const string SCORES = "Scores";
        const string HIGHSCORE = "HighScore";
        const string PLAYER = "Player";
        const string SCORE = "Score";
        const string DATE = "Date";

        #endregion

        #region Properties

        /// <summary>
        /// Implementation of the Singleton pattern - ensures only one instance of the ScoreController exists.  
        /// Note that this is a static method to enable instantiation of a new object
        /// </summary>
        public static ScoreController Instance
        {
            get
            {
                if (_instance == null) //if an instance of ScoreController doesnt exist...
                {
                    _instance = new ScoreController(); //create one and save it
                }
                return _instance; ///...then return the instance
            }
        }

        #endregion

        /// <summary>
        /// The constructor.  Note that it is private to stop the class being instantiated directly, as this class uses the Singleton pattern.
        /// </summary>
        private ScoreController()
        {
            //no action needed
        }

        #region Methods

        /// <summary>
        /// Adds a new score to the XML file
        /// </summary>
        /// <param name="player">The player name</param>
        /// <param name="score">The player's score</param>
        /// <returns>Return true if the score was a high score, else return false.</returns>
        public bool Add(string player, Int32 score)
        {
            List<PlayerScore> list = LoadScores(); //read the scores currently held in the XML file
            PlayerScore item = new PlayerScore //create a new PlayerScore object with the data passed to the method
            {
                Player = player,
                Score = score,
                Date = DateTime.Now
            };

            //try and add the new score to the high scores
            bool flag = false;
            if (item.Score != 0) //only add score if it is above 0
            {
                if (list.Count < SIZE) //if the list size is less than the max number of scores
                {
                    list.Add(item);//then add it
                    flag = true;
                }
                else
                {
                    //else, find the lowest score (possible because the PlayerScore object implements IComparable)
                    list.Sort();
                    PlayerScore lowestScore = list[0];
                    if (lowestScore.Score < item.Score)
                    {
                        //...if the new score is higher than the lowest score on the list then add it
                        list.RemoveAt(0);
                        list.Add(item);
                        flag = true;
                    }
                }
            }

            //if a new score has been added to the high scores list, then re-write the high scores XML file
            if (flag)
            {
                SaveScores(list);
            }
            return flag; //return true if the score was a high score, else return false.
        }

        /// <summary>
        /// Reads the high scores XML file and returns a collection of PlayerScore objects, each representing a high score
        /// As the high scores XML file is encrypted, this method decypts it.
        /// </summary>
        /// <returns></returns>
        private List<PlayerScore> LoadScores()
        {
            if (File.Exists(FileName))
            {
                try
                {
                    //read the XML file
                    XmlDocument xmlDoc = new XmlDocument
                    {
                        PreserveWhitespace = true
                    };
                    xmlDoc.Load(FileName);

                    // Create a new RSA key.  This key will encrypt a symmetric key,
                    // which will then be imbedded in the XML document.
                    //need to move the key into the webconfig
                    CspParameters parameters = new CspParameters
                    {
                        KeyContainerName = ConfigurationSettings.AppSettings["ScoresFileEncryptionKey"]
                        //KeyContainerName = "XML_ENC_RSA_KEY"
                    };
                    RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(parameters);
                    // Decrypt the "Player" element.
                    TrippleDESDocumentEncryption.Decrypt(xmlDoc, rsaKey, "rsaKey");

                    List<PlayerScore> list = new List<PlayerScore>(); //Create a new list
                    XmlNodeList elementsByTagName = null;
                    elementsByTagName = xmlDoc.GetElementsByTagName(HIGHSCORE); //find each score entry in the XML file
                    foreach (XmlNode node in elementsByTagName)
                    {
                        //for each score found in the XML file, create a new PlayerScore object with the data
                        PlayerScore item = new PlayerScore
                        {
                            Player = node[PLAYER].InnerText,
                            Score = Convert.ToInt32(node[SCORE].InnerText),
                            Date = Convert.ToDateTime(node[DATE].InnerText)
                        };
                        list.Add(item); //and add it to the collection
                    }
                    return list; //when finished, return this collection
                }
                catch (XmlException)
                {
                    //throw an exception if an error occured
                    throw new Exception("Error occured while reading data.");
                }
            }
            return new List<PlayerScore>(); //if no XML file exists, return an empty list
        }

        /// <summary>
        /// Creates a new high scores XML file using a collection of PlayerScore objects.  If the file already exists, it will be overwritten.
        /// This method encrypts the name of the player in the XML file.
        /// </summary>
        /// <param name="scores">A collection of PlayerScore objects, representing all the high scores needed to be stored</param>
        private void SaveScores(List<PlayerScore> scores)
        {
            try
            {
                //create the XML file
                XmlDocument xmlDoc = new XmlDocument();
                XmlTextWriter writer = new XmlTextWriter(FileName, Encoding.UTF8)
                {
                    Formatting = Formatting.Indented
                };
                writer.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                writer.WriteStartElement(SCORES);
                writer.Close();


                //Blank the file
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Load(FileName);

                // Create a new RSA key.  This key will encrypt a symmetric key,
                // which will then be imbedded in the XML document.  
                CspParameters parameters = new CspParameters
                {
                    KeyContainerName = ConfigurationSettings.AppSettings["ScoresFileEncryptionKey"]
                    //KeyContainerName = "XML_ENC_RSA_KEY"
                };
                RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(parameters);
                // Decrypt the "Player" element.
                TrippleDESDocumentEncryption.Decrypt(xmlDoc, rsaKey, "rsaKey");

                //Add each high score to the XML file
                foreach (PlayerScore score in scores)
                {
                    //get the main node
                    XmlNode documentNode = xmlDoc.DocumentElement;

                    //create a new child node for each high score
                    XmlElement highScoreChild = xmlDoc.CreateElement(HIGHSCORE);

                    //create new elements for the name, score and date/time
                    XmlElement ScoreElement = xmlDoc.CreateElement(SCORE);
                    XmlElement PlayerElement = xmlDoc.CreateElement(PLAYER);
                    XmlElement DateElement = xmlDoc.CreateElement(DATE);

                    XmlText scoreText = xmlDoc.CreateTextNode("score");
                    XmlText playerText = xmlDoc.CreateTextNode("player");
                    XmlText dateText = xmlDoc.CreateTextNode("date");

                    //add the new high score child node to the main node
                    documentNode.AppendChild(highScoreChild);

                    //and add all the data to the high score child node
                    highScoreChild.AppendChild(ScoreElement);
                    highScoreChild.AppendChild(PlayerElement);
                    highScoreChild.AppendChild(DateElement);

                    ScoreElement.AppendChild(scoreText);
                    PlayerElement.AppendChild(playerText);
                    DateElement.AppendChild(dateText);

                    scoreText.Value = score.Score.ToString();
                    playerText.Value = score.Player;
                    dateText.Value = score.Date.ToString();

                    // Encrypt the "Player" element.
                    TrippleDESDocumentEncryption.Encrypt(xmlDoc, PLAYER, rsaKey, "rsaKey");
                }
                xmlDoc.Save(FileName); //save the file
            }
            catch (Exception exception4)
            {
            }
        }

        /// <summary>
        /// Get a DataTable of the high scores from the high scores XML file
        /// </summary>
        /// <returns>A DataTable of the current high scores</returns>
        public DataTable getScoreTable()
        {
            DataTable scoreTable = new DataTable(SCORES); //create the DataTable
            DataColumn playerColumn = new DataColumn(PLAYER); //add the Player column
            playerColumn.DataType = Type.GetType("System.String"); //set the type as string 
            playerColumn.ReadOnly = false; //set other properties
            playerColumn.Unique = false;
            scoreTable.Columns.Add(playerColumn); //add the column to the table

            DataColumn scoreColumn = new DataColumn(SCORE); //add the score column
            scoreColumn.DataType = Type.GetType("System.Int32"); //set the type as int 
            scoreColumn.ReadOnly = false;//set other properties
            scoreColumn.Unique = false;
            scoreTable.Columns.Add(scoreColumn);//add the column to the table

            DataColumn dateColumn = new DataColumn(DATE);//add the Date column
            dateColumn.DataType = Type.GetType("System.DateTime");//set the type as DateTime 
            dateColumn.ReadOnly = false;//set other properties
            dateColumn.Unique = false;
            scoreTable.Columns.Add(dateColumn);//add the column to the table

            List<PlayerScore> scoresList = LoadScores(); //retrieve a collection of the high scores

            //iterate through the collection and add a new row into the table for each object
            foreach (PlayerScore score in scoresList)
            {
                DataRow scoreRow = scoreTable.NewRow(); //create a new row
                scoreRow[PLAYER] = score.Player; //add the name
                scoreRow[SCORE] = score.Score; //add the score
                scoreRow[DATE] = score.Date; //add the data
                scoreTable.Rows.Add(scoreRow); //add the row to the table
            }
            return scoreTable; //when finished, return the complete DataTable
        }

        /// <summary>
        /// Set the location of the XML file
        /// </summary>
        /// <param name="Location">XML file location</param>
        public void setLocation(string Location)
        {
            FileName = Location;
        }

        #endregion
    }
}

