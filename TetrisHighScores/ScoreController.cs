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
        private const int size = 12;

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
                Time = DateTime.Now
            };
            bool flag = false;
            if (item.Score != 0)
            {
                if (list.Count < size)
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
                    elementsByTagName = xmlDoc.GetElementsByTagName("GameScore");
                    foreach (XmlNode node in elementsByTagName)
                    {
                        PlayerScore item = new PlayerScore
                        {
                            Score = Convert.ToInt32(node["Score"].InnerText),
                            Player = node["Player"].InnerText,
                            Time = Convert.ToDateTime(node["Time"].InnerText)
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
                    XmlElement newChild = xmlDoc.CreateElement("GameScore");
                    XmlElement element2 = xmlDoc.CreateElement("Score");
                    XmlElement element3 = xmlDoc.CreateElement("Player");
                    XmlElement element4 = xmlDoc.CreateElement("Time");
                    XmlText text = xmlDoc.CreateTextNode("score");
                    XmlText text2 = xmlDoc.CreateTextNode("player");
                    XmlText text3 = xmlDoc.CreateTextNode("time");
                    documentElement.AppendChild(newChild);
                    newChild.AppendChild(element2);
                    newChild.AppendChild(element3);
                    newChild.AppendChild(element4);
                    element2.AppendChild(text);
                    element3.AppendChild(text2);
                    element4.AppendChild(text3);
                    text.Value = score.Score.ToString();
                    text2.Value = score.Player;
                    text3.Value = score.Time.ToString();

                    // Encrypt the "Player" element.
                    TrippleDESDocumentEncryption.Encrypt(xmlDoc, "Player", rsaKey, "rsaKey");

                    //    XmlEncryptionController.Encrypt(doc, "GameScore", "EncryptedGameScore", alg, "rsaKey");
                    //    XmlEncryptionController.Encrypt(doc, "GamePlayer", "EncryptedGamePlayer", alg, "rsaKey");
                    //    XmlEncryptionController.Encrypt(doc, "GameScoreTime", "EncryptedGameScoreTime", alg, "rsaKey");
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

