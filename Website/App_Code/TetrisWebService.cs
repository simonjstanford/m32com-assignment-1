using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using TetrisHighScores;
using System.Configuration;

/// <summary>
/// The class that holds all the web service methods for the web service based game Tetris
/// </summary>
[WebService(Namespace = "http://M32COM-Tetris.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
//[System.Web.Script.Services.ScriptService]
public class TetrisWebService : WebService {

    // Fields
    private const string GAMESESSIONINDEX = "Tetris"; //The name of the session state object that the Game object is saved to/retrieved from
    private const string GUESTUSERNAME = "Guest"; //The default player name if none is given

    public TetrisWebService () {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Game Calls

    /// <summary>
    /// Starts a new game
    /// </summary>
    /// <param name="player">The player name</param>
    /// <returns>A new game board object</returns>
    [WebMethod(EnableSession = true)]
    public string[][] StartGame(String player)
    {
        //replace the Game object kept in session state with a fresh Game object
        base.Session[GAMESESSIONINDEX] = new Game(player);
        return GetBoard(); //return the board
    }

    /// <summary>
    /// Used to determine if the game is in play or not
    /// </summary>
    /// <returns>True if the game is still playing, or false if the game is over</returns>
    [WebMethod(EnableSession = true)]
    public bool GetGameState()
    {
        //returns true if game is in play
        switch (GetGame().State)
        {
            case State.Playing:
                return true;;
            case State.GameOver:
                return false;
            default:
                return true;
        }
    }

    /// <summary>
    /// Used to get the game board from session state with the latest state of all the shapes
    /// </summary>
    /// <returns>A jagged string array representing the two dimensional game board</returns>
    [WebMethod(EnableSession = true)]
    public string[][] GetBoard()
    {
        return GetGame().ToArray();
    }

    /// <summary>
    /// Gets the shape that will next appear on the game board.  Used to display this shape for the player
    /// </summary>
    /// <returns>A jagged string array representing a miniture game board that illustrates the new shape</returns>
    [WebMethod(EnableSession = true)]
    public string[][] GetNextShape()
    {
        //retrieve the next shape from the Game object in session state and return
        return GetGame().Board.NextShape.ToArray();
    }

    #endregion

    #region Block movement calls

    /// <summary>
    /// Moves the active shape left
    /// </summary>
    /// <returns>An updated game board with the latest shape states</returns>
    [WebMethod(EnableSession = true)]
    public string[][] MoveBlockLeft()
    {
        //get the game from session state
        Game game = GetGame();
        game.MoveBlockLeft(); //move the shape
        SaveGame(game); //save the Game object back into session state
        return GetBoard(); //return the updated board
    }

    /// <summary>
    /// Moves the active shape right
    /// </summary>
    /// <returns>An updated game board with the latest shape states</returns>
    [WebMethod(EnableSession = true)]
    public string[][] MoveBlockRight()
    {
        //get the game from session state
        Game game = GetGame();
        game.MoveBlockRight(); //move the shape
        SaveGame(game);//save the Game object back into session state
        return GetBoard();//return the updated board
    }

    /// <summary>
    /// Rotate the active shape clockwise
    /// </summary>
    /// <returns>An updated game board with the latest shape states</returns>
    [WebMethod(EnableSession = true)]
    public string[][] RotateBlock()
    {
        //get the game from session state
        Game game = GetGame();
        game.RotateBlock();//move the shape
        SaveGame(game);//save the Game object back into session state
        return GetBoard();//return the updated board
    }

    /// <summary>
    /// Moves the active shape down one level.  Executes every timer tick
    /// </summary>
    /// <returns>An updated game board with the latest shape states</returns>
    [WebMethod(EnableSession = true)]
    public string[][] MoveBlockDown()
    {
        //get the game from session state
        Game game = GetGame();
        game.MoveBlockDown();//move the shape
        SaveGame(game);//save the Game object back into session state
        return GetBoard();//return the updated board
    }

    /// <summary>
    /// Moves the active shape all the way to the bottom of the game board.  Executes when the player presses the down key
    /// </summary>
    /// <returns>An updated game board with the latest shape states</returns>
    [WebMethod(EnableSession = true)]
    public string[][] DropBlock()
    {
        //get the game from session state
        Game game = GetGame();
        game.DropBlock();//move the shape
        SaveGame(game);//save the Game object back into session state
        return GetBoard();//return the updated board
    }

    #endregion

    #region Score calls

    /// <summary>
    /// Gets the current game score
    /// </summary>
    /// <returns>The current score</returns>
    [WebMethod(EnableSession = true)]
    public int GetScore()
    {
        //get the game object from session state and read the score property
        return GetGame().Score;
    }

    /// <summary>
    /// Gets all the high scores from the game
    /// </summary>
    /// <returns>A DataTable containing the high scores</returns>
    [WebMethod(EnableSession = true)]
    public DataTable GetHighScores()
    {
        //read the Scores.xml file location from the web.config file
        ScoreController.Instance.setLocation(base.Server.MapPath(ConfigurationManager.AppSettings["ScoresFileLocation"]));
        return ScoreController.Instance.getScoreTable(); //retrieve the singleton instance of the high score controller, and call the method to get the high scores.  Return this data
    }

    /// <summary>
    /// Submits a new score to the high score database
    /// </summary>
    /// <returns>A boolean value indicating if the score achieved was a high score or not</returns>
    [WebMethod(EnableSession = true)]
    public Boolean SubmitScore()
    {
        if (GetGame().Score > 0) //only attempt to post the score if it was above 0
        {
            //read the Scores.xml file location from the web.config file
            ScoreController.Instance.setLocation(base.Server.MapPath(ConfigurationManager.AppSettings["ScoresFileLocation"]));
            return ScoreController.Instance.Add(GetGame().Player, GetGame().Score); //retrieve the singleton instance of the high score controller, and attempt to add the score.  Return the bool indicating if the score was a high score
        }
        return false; //if the score was 0, return false as this is can not be a high score
    }

    #endregion

    #region Private calls

    /// <summary>
    /// Checks to see if a session state already exists for this player.  If not, a new session state is created
    /// </summary>
    [WebMethod(EnableSession = true)]
    private void ValidateSession() 
    {
        if (Session[GAMESESSIONINDEX] == null) 
        {
            StartGame(GUESTUSERNAME);
        }
    }

    /// <summary>
    /// Returns the Game object from session state
    /// </summary>
    /// <returns>The current game object for the player</returns>
    [WebMethod(EnableSession = true)]
    private Game GetGame()
    {
        ValidateSession(); //check to see if a session state exists for the player.  If not, a new session state is created
        return (Game)base.Session[GAMESESSIONINDEX]; //return the current Game object from session state
    }

    /// <summary>
    /// Saves an updated Game object into session state
    /// </summary>
    /// <param name="game">The updated Game object that needs to be saved</param>
    [WebMethod(EnableSession = true)]
    private void SaveGame(Game game)
    {
        base.Session[GAMESESSIONINDEX] = game;
    }

    #endregion

    #region Session test calls
    // This is testing that we can save in a session
    // Code taken from: http://www.codeproject.com/Articles/35119/Using-Session-State-in-a-Web-Service

    /// <summary>
    /// A simple web service method, used in testing to prove that the web service was communicating
    /// </summary>
    /// <returns>'Hello World' plus the number of times this method has been called</returns>
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
    #endregion
}
