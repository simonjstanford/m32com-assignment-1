using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Security.Cryptography;

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
        const string GAMESCORE = "GameScore";
        const string PLAYER = "Player";
        const string SCORE = "Score";
        const string DATE = "Date";
        
        // Methods
        private ScoreController()
        {
        }

        public bool Add(string player, int score)
        {
            List<PlayerScore> list = this.LoadScores();
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
                this.SaveScores(list);
            }
            return flag;
        }

        private List<PlayerScore> LoadScores()
        {
            if (File.Exists(this.FileName))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument
                    {
                        PreserveWhitespace = true
                    };
                    xmlDoc.Load(this.FileName);

                    // Create a new RSA key.  This key will encrypt a symmetric key,
                    // which will then be imbedded in the XML document.
                    //need to move the key into the webconfig
                    CspParameters parameters = new CspParameters
                    {
                        KeyContainerName = "XML_ENC_RSA_KEY"
                    };
                    RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(parameters);
                    // Decrypt the "Player" element.
                    TrippleDESDocumentEncryption.Decrypt(xmlDoc, rsaKey, "rsaKey");

                    List<PlayerScore> list = new List<PlayerScore>();
                    XmlNodeList elementsByTagName = null;
                    elementsByTagName = xmlDoc.GetElementsByTagName(GAMESCORE);
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

        private void SaveScores(List<PlayerScore> scores)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlTextWriter writer = new XmlTextWriter(this.FileName, Encoding.UTF8)
                {
                    Formatting = Formatting.Indented
                };
                writer.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                writer.WriteStartElement("Scores");
                writer.Close();
                //Blank the file
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Load(this.FileName);

                // Create a new RSA key.  This key will encrypt a symmetric key,
                // which will then be imbedded in the XML document.  
                CspParameters parameters = new CspParameters
                {
                    KeyContainerName = "XML_ENC_RSA_KEY"
                };
                RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(parameters);
                // Decrypt the "Player" element.
                TrippleDESDocumentEncryption.Decrypt(xmlDoc, rsaKey, "rsaKey");

                foreach (PlayerScore score in scores)
                {
                    XmlNode documentElement = xmlDoc.DocumentElement;
                    XmlElement gameScoreElement = xmlDoc.CreateElement(GAMESCORE);
                    XmlElement playerElement = xmlDoc.CreateElement(PLAYER);
                    XmlElement scoreElement = xmlDoc.CreateElement(SCORE);
                    XmlElement dateElement = xmlDoc.CreateElement(DATE);
                    XmlText playerText = xmlDoc.CreateTextNode(PLAYER);
                    XmlText scoreText = xmlDoc.CreateTextNode(SCORE);
                    XmlText dateText = xmlDoc.CreateTextNode(DATE);
                    documentElement.AppendChild(gameScoreElement);
                    gameScoreElement.AppendChild(playerElement);
                    gameScoreElement.AppendChild(scoreElement);
                    gameScoreElement.AppendChild(dateElement);
                    gameScoreElement.AppendChild(playerText);
                    gameScoreElement.AppendChild(scoreElement);
                    gameScoreElement.AppendChild(dateText);
                    scoreElement.Value = score.Score.ToString();
                    playerElement.Value = score.Player;
                    dateElement.Value = score.Date.ToString();

                    // Encrypt the "Player" element.
                    TrippleDESDocumentEncryption.Encrypt(xmlDoc, PLAYER, rsaKey, "rsaKey");
                }
                xmlDoc.Save(this.FileName);
            }
            catch (Exception exception4)
            {
                Console.Write(exception4.ToString());
            }
        }

        public DataTable getScoreTable()
        {
            DataTable scoreTable = new DataTable(GAMESCORE);
            DataColumn playerColumn = new DataColumn(PLAYER);
            playerColumn.DataType = Type.GetType("System.String");
            playerColumn.ReadOnly = false;
            playerColumn.Unique = false;
            scoreTable.Columns.Add(playerColumn);

            DataColumn scoreColumn = new DataColumn(SCORE);
            scoreColumn.DataType = Type.GetType("System.String");
            scoreColumn.ReadOnly = false;
            scoreColumn.Unique = false;
            scoreTable.Columns.Add(scoreColumn);

            DataColumn dateColumn = new DataColumn(DATE);
            dateColumn.DataType = Type.GetType("System.String");
            dateColumn.ReadOnly = false;
            dateColumn.Unique = false;
            scoreTable.Columns.Add(scoreColumn);

            List<PlayerScore> scoresList = this.LoadScores();

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

