using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using TetrisHighScores;

/// <summary>
/// Summary description for TetrisWebService
/// </summary>
[WebService(Namespace = "http://M32COM-Tetris.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class TetrisWebService : System.Web.Services.WebService {

    public TetrisWebService () {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //********************************************
    //
    //    Game calls
    //
    //********************************************

    [WebMethod(EnableSession = true)]
    public void StartGame(String playerName)
    {
    }

    [WebMethod(EnableSession = true)]
    public bool GetGameState()
    {
        //returns true if game is in play
        return true;
    }

    [WebMethod(EnableSession = true)]
    public int[][] GetBoard()
    {
        int[][] testretval = new int[4][];
        return testretval;
    }

    [WebMethod(EnableSession = true)]
    public int[][] GetNextBlock()
    {
        int[][] testretval = new int[4][];
        return testretval;
    }

    //********************************************
    //
    //    Block movement calls
    //
    //********************************************

    [WebMethod(EnableSession = true)]
    public void MoveBlockLeft()
    {
    }

    [WebMethod(EnableSession = true)]
    public void MoveBlockRight()
    {
    }

    [WebMethod(EnableSession = true)]
    public void RotateBlock()
    {
    }

    [WebMethod(EnableSession = true)]
    public void MoveBlockDown()
    {
    }

    [WebMethod(EnableSession = true)]
    public void DropBlock()
    {
    }

    //********************************************
    //
    //    Score calls
    //
    //********************************************
    [WebMethod(EnableSession = true)]
    public int GetScore()
    {
        return 10;
    }

    [WebMethod]
    public DataTable GetHighScores()
    {
        //file location need to be moved to web.config
        ScoreController.Instance.setLocation(base.Server.MapPath("~/Scores.xml"));
        return ScoreController.Instance.getScoreTable();
    }

    [WebMethod(EnableSession = true)]
    public void SubmitScore()
    {
        //need to get them form game object
        int score = 100;
        string player = "Guest";
        if (score > 0)
        {
            //file location need to be moved to web.config
            ScoreController.Instance.setLocation(base.Server.MapPath("~/Scores.xml"));
            ScoreController.Instance.Add(player,score);
        }
    }    
}
