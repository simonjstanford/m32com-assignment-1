using System;

/// <summary>
/// The game and its status
/// </summary>
public sealed class Game
{
    #region Class Fields

    private Board _board; //the game board object, where all movement occurs
    private int _score = 0; //the current score
    private State _state = State.Playing; //the current game state, i.e. Playing or Game Over
    private String _player = "Guest"; //the player name, with a default of 'Guest'

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor that initializes a new instance of the <see cref="Game"/> class with no player name
    /// </summary>
    public Game()
    {
        _board = new Board(); //instantiate the board object
        _board.Game = this; 
    }

    /// <summary>
    /// Constructor that a new instance of the <see cref="Game"/> class With player name.
    /// </summary>
    /// <param name="player">The players name.</param>
    public Game(String player)
    {
        _board = new Board(); //instantiate the board object
        _player = player; //pass it the player's name
        _board.Game = this;
    }

    #endregion

    #region Class Properties

    /// <summary>
    /// Gets or sets the player's name.
    /// </summary>
    /// <value>
    /// The player's name
    /// </value>
    public String Player
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
    /// Gets or sets the score.
    /// </summary>
    /// <value>
    /// The score.
    /// </value>
    public int Score
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
    /// Gets or sets the state of the game - Playing or Game Over
    /// </summary>
    /// <value>
    /// The state.
    /// </value>
    public State State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
        }
    }

    /// <summary>
    /// Gets the game board object
    /// </summary>
    /// <value>
    /// The board.
    /// </value>
    public Board Board
    {
        get
        {
            return _board;
        }
    }

    #endregion

    #region Shape Movement Methods

    /// <summary>
    /// Drops the shape all the way to the bottom of the game board.
    /// </summary>
    /// <returns>True if the move was successful, false otherwise</returns>
    public bool DropBlock()
    {
        return _board.DropBlock();
    }

    /// <summary>
    /// Moves the shape down one row of the game board.
    /// </summary>
    /// <returns>True if the move was successful, false otherwise</returns>
    public bool MoveBlockDown()
    {
        return _board.MoveActiveShapeDown();
    }

    /// <summary>
    /// Moves the shape left.
    /// </summary>
    /// <returns>True if the move was successful, false otherwise</returns>
    public bool MoveBlockLeft()
    {
        return _board.MoveActiveShapeLeft();
    }

    /// <summary>
    /// Moves the shape right.
    /// </summary>
    /// <returns>True if the move was successful, false otherwise</returns>
    public bool MoveBlockRight()
    {
        return _board.MoveActiveShapeRight();
    }

    /// <summary>
    /// Rotates the shape clocwise 
    /// </summary>
    /// <returns>True if the move was successful, false otherwise</returns>
    public bool RotateBlock()
    {
        return _board.RotateActiveShape();
    }

    #endregion

    #region Misc. class methods
    /// <summary>
    /// Returns the game board as an array.
    /// </summary>
    /// <returns></returns>
    public string[][] ToArray()
    {
        return _board.ToArray();
    }

    #endregion
}