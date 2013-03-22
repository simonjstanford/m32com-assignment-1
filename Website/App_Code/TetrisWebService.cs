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

    // Fields
    private const string GAMESESSIONINDEX = "Tetris";
    private const string GUESTUSERNAME = "Guest";


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
    public void StartGame(String player)
    {
        base.Session[GAMESESSIONINDEX] = new Game(player);
    }

    [WebMethod(EnableSession = true)]
    public bool GetGameState()
    {
        //returns true if game is in play
        return (GetGame().State != State.Playing);
    }

    [WebMethod(EnableSession = true)]
    public string[][] GetBoard()
    {
        string[][] testretval = new string[4][];
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
        GetGame().MoveBlockLeft();
    }

    [WebMethod(EnableSession = true)]
    public void MoveBlockRight()
    {
        GetGame().MoveBlockRight();
    }

    [WebMethod(EnableSession = true)]
    public void RotateBlock()
    {
        GetGame().RotateBlock();
    }


    [WebMethod(EnableSession = true)]
    public void MoveBlockDown()
    {
        GetGame().MoveBlockDown();
    }

    [WebMethod(EnableSession = true)]
    public void DropBlock()
    {
        GetGame().DropBlock();
    }

    //********************************************
    //
    //    Score calls
    //
    //********************************************
    [WebMethod(EnableSession = true)]
    public int GetScore()
    {
        return GetGame().Score;
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
        if (GetGame().Score > 0)
        {
            //file location need to be moved to web.config
            ScoreController.Instance.setLocation(base.Server.MapPath("~/Scores.xml"));
            ScoreController.Instance.Add(GetGame().Player,GetGame().Score);
        }
    }


    //********************************************
    //
    //    Private calls
    //
    //********************************************
    [WebMethod(EnableSession = true)]
    private void ValidateSession() 
    {
        if (base.Session[GAMESESSIONINDEX] == null) 
        {
            this.StartGame(GUESTUSERNAME);
        }
    }

    [WebMethod(EnableSession = true)]
    private Game GetGame()
    {
        this.ValidateSession();
        return (Game)base.Session[GAMESESSIONINDEX];
    }

    [WebMethod(EnableSession = true)]
    private void SaveGame(Game game)
    {
        base.Session[GAMESESSIONINDEX] = game;
    }
}
