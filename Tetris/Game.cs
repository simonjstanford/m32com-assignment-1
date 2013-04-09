using System;

/// <summary>
/// The game and its status
/// </summary>
public sealed class Game
{
    // Fields
    private Board _board;
    private int _score = 0;
    private State _state = State.Playing;
    private String _player = "Guest";

    // Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="Game"/> class with no player name
    /// </summary>
    public Game()
    {
        _board = new Board();
        _board.Game = this;
    }

    // Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="Game"/> class With player name.
    /// </summary>
    /// <param name="player">The players name.</param>
    public Game(String player)
    {
        _board = new Board();
        _player = player;
        _board.Game = this;
    }

    /// <summary>
    /// Drops the block.
    /// </summary>
    /// <returns></returns>
    public bool DropBlock()
    {
        return _board.DropBlock();
    }

    /// <summary>
    /// Moves the block down.
    /// </summary>
    /// <returns></returns>
    public bool MoveBlockDown()
    {
        return _board.MoveActiveShapeDown();
    }

    /// <summary>
    /// Moves the block left.
    /// </summary>
    /// <returns></returns>
    public bool MoveBlockLeft()
    {
        return _board.MoveActiveShapeLeft();
    }

    /// <summary>
    /// Moves the block right.
    /// </summary>
    /// <returns></returns>
    public bool MoveBlockRight()
    {
        return _board.MoveActiveShapeRight();
    }

    /// <summary>
    /// Rotates the block.
    /// </summary>
    /// <returns></returns>
    public bool RotateBlock()
    {
        return _board.RotateActiveShape();
    }

    /// <summary>
    /// Returns the game board as an array.
    /// </summary>
    /// <returns></returns>
    public string[][] ToArray()
    {
        return _board.ToArray();
    }

    // Properties
    
    /// <summary>
    /// Gets or sets the player.
    /// </summary>
    /// <value>
    /// The player.
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
    /// Gets or sets the state.
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
    /// Gets the board.
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
}