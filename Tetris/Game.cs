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
    public Game()
    {
        _board = new Board();
    }

    // Methods
    public Game(String player)
    {
        _board = new Board();
        _player = player;
    }

    public bool DropBlock()
    {
        return _board.DropBlock();
    }

    public bool MoveBlockDown()
    {
        return _board.MoveActiveShapeDown();
    }

    public bool MoveBlockLeft()
    {
        return _board.MoveActiveShapeLeft();
    }

    public bool MoveBlockRight()
    {
        return _board.MoveActiveShapeRight();
    }

    public void Restart()
    {
    }

    public bool RotateBlock()
    {
        return _board.RotateActiveShape();
    }

    public string[][] ToArray()
    {
        return _board.ToArray();
    }

    // Properties
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

    public int Score
    {
        get
        {
            return _score;
        }
    }

    public State State
    {
        get
        {
            return _state;
        }
    }

    public Board Board
    {
        get
        {
            return _board;
        }
    }    

}