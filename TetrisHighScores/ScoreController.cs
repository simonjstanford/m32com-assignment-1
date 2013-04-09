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
        // Fields
        private static ScoreController _instance;
        private string FileName = "Scores.xml";
        private const int SIZE = 12;


        //Used in data table and XML file to keep field names in sync between read and write
        const string SCORES = "Scores";
        const string HIGHSCORE = "HighScore";
        const string PLAYER = "Player";
        const string SCORE = "Score";
        const string DATE = "Date";

        // Methods
        /// <summary>
        /// Prevents a default instance of the <see cref="ScoreController"/> class from being created.
        /// </summary>
        private ScoreController()
        {
        }

        /// <summary>
        /// Adds the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="score">The score.</param>
        /// <returns></returns>
        public bool Add(string player, Int32 score)
        {
            List<PlayerScore> list = LoadScores();
            PlayerScore item = new PlayerScore
            {
                Player = player,
                Score = score,
                Date = DateTime.Now
            };
            bool flag = false;
            if (item.Score != 0)
            {
                if (list.Count < SIZE)
                {
                    list.Add(item);
                    flag = true;
                }
                else
                {
                    list.Sort();
                    PlayerScore lowestScore = list[0];
                    if (lowestScore.Score < item.Score)
                    {
                        list.RemoveAt(0);
                        list.Add(item);
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                SaveScores(list);
            }
            return flag;
        }

        /// <summary>
        /// Loads the scores.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error occured while reading data.</exception>
        private List<PlayerScore> LoadScores()
        {
            if (File.Exists(FileName))
            {
                try
                {
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

                    List<PlayerScore> list = new List<PlayerScore>();
                    XmlNodeList elementsByTagName = null;
                    elementsByTagName = xmlDoc.GetElementsByTagName(HIGHSCORE);
                    foreach (XmlNode node in elementsByTagName)
                    {
                        PlayerScore item = new PlayerScore
                        {
                            Player = node[PLAYER].InnerText,
                            Score = Convert.ToInt32(node[SCORE].InnerText),
                            Date = Convert.ToDateTime(node[DATE].InnerText)
                        };
                        list.Add(item);
                    }
                    return list;
                }
                catch (XmlException)
                {
                    throw new Exception("Error occured while reading data.");
                }
            }
            return new List<PlayerScore>();
        }

        /// <summary>
        /// Saves the scores.
        /// </summary>
        /// <param name="scores">The scores.</param>
        private void SaveScores(List<PlayerScore> scores)
        {
            try
            {
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

                foreach (PlayerScore score in scores)
                {
                    XmlNode documentNode = xmlDoc.DocumentElement;

                    XmlElement highScoreChild = xmlDoc.CreateElement(HIGHSCORE);

                    XmlElement ScoreElement = xmlDoc.CreateElement(SCORE);
                    XmlElement PlayerElement = xmlDoc.CreateElement(PLAYER);
                    XmlElement DateElement = xmlDoc.CreateElement(DATE);

                    XmlText scoreText = xmlDoc.CreateTextNode("score");
                    XmlText playerText = xmlDoc.CreateTextNode("player");
                    XmlText dateText = xmlDoc.CreateTextNode("date");

                    documentNode.AppendChild(highScoreChild);

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
                xmlDoc.Save(FileName);
            }
            catch (Exception exception4)
            {
            }
        }

        /// <summary>
        /// Gets the score table.
        /// </summary>
        /// <returns></returns>
        public DataTable getScoreTable()
        {
            DataTable scoreTable = new DataTable(SCORES);
            DataColumn playerColumn = new DataColumn(PLAYER);
            playerColumn.DataType = Type.GetType("System.String");
            playerColumn.ReadOnly = false;
            playerColumn.Unique = false;
            scoreTable.Columns.Add(playerColumn);

            DataColumn scoreColumn = new DataColumn(SCORE);
            scoreColumn.DataType = Type.GetType("System.Int32");
            scoreColumn.ReadOnly = false;
            scoreColumn.Unique = false;
            scoreTable.Columns.Add(scoreColumn);

            DataColumn dateColumn = new DataColumn(DATE);
            dateColumn.DataType = Type.GetType("System.DateTime");
            dateColumn.ReadOnly = false;
            dateColumn.Unique = false;
            scoreTable.Columns.Add(dateColumn);

            List<PlayerScore> scoresList = LoadScores();

            foreach (PlayerScore score in scoresList)
            {
                DataRow scoreRow = scoreTable.NewRow();
                scoreRow[PLAYER] = score.Player;
                scoreRow[SCORE] = score.Score;
                scoreRow[DATE] = score.Date;
                scoreTable.Rows.Add(scoreRow);
            }
            return scoreTable;
        }

        /// <summary>
        /// Sets the location.
        /// </summary>
        /// <param name="Location">The location.</param>
        public void setLocation(string Location)
        {
            FileName = Location;
        }

        // Properties
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
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

