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
//[System.Web.Script.Services.ScriptService]
public class TetrisWebService : WebService {

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
        return GetGame().ToArray();
    }

    [WebMethod(EnableSession = true)]
    public string[][] GetNextShape()
    {
        return GetGame().Board.NextShape.ToArray();
    }

    //********************************************
    //
    //    Block movement calls
    //
    //********************************************

    [WebMethod(EnableSession = true)]
    public void MoveBlockLeft()
    {
        Game game = GetGame();
        game.MoveBlockLeft();
        SaveGame(game);
    }

    [WebMethod(EnableSession = true)]
    public void MoveBlockRight()
    {
        Game game = GetGame();
        game.MoveBlockRight();
        SaveGame(game);
    }

    [WebMethod(EnableSession = true)]
    public void RotateBlock()
    {
        Game game = GetGame();
        game.RotateBlock();
        SaveGame(game);
    }

    [WebMethod(EnableSession = true)]
    public void MoveBlockDown()
    {
        Game game = GetGame();
        game.MoveBlockDown();
        SaveGame(game);
    }

    [WebMethod(EnableSession = true)]
    public void DropBlock()
    {
        Game game = GetGame();
        game.DropBlock();
        SaveGame(game);
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

    [WebMethod(EnableSession = true)]
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
        if (Session[GAMESESSIONINDEX] == null) 
        {
            StartGame(GUESTUSERNAME);
        }
    }

    [WebMethod(EnableSession = true)]
    private Game GetGame()
    {
        ValidateSession();
        return (Game)base.Session[GAMESESSIONINDEX];
    }

    [WebMethod(EnableSession = true)]
    private void SaveGame(Game game)
    {
        base.Session[GAMESESSIONINDEX] = game;
    }

    //********************************************
    //
    //    Session test calls - this is testing that we can save in a session
    //    Code taken from:
    //    http://www.codeproject.com/Articles/35119/Using-Session-State-in-a-Web-Service
    //
    //********************************************
    [WebMethod(EnableSession = true)]
    public string HelloWorld()
    {
        // get the Count out of Session State
        int? Count = (int?)Session["Count"];

        if (Count == null)
            Count = 0;

        // increment and store the count
        Count++;
        Session["Count"] = Count;

        return "Hello World - Call Number: " + Count.ToString();
    }
}
