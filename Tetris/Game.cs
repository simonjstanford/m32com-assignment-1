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

    private void CheckFullRows()
    {
    }

    public bool DropBlock()
    {
        return false;
    }

    private void GetNextBlock()
    {
    }

    public bool MoveBlockDown()
    {
        return _board.MoveActiveShapeDown();
    }

    public bool MoveBlockLeft()
    {
        return _board.MoveActiveShapeDown();
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



    // Properties
    public String Player
    {
        get
        {
            return this._player;
        }
        set
        {
            this._player = value;
        }
    }

    public int Score
    {
        get
        {
            return this._score;
        }
    }

    public State State
    {
        get
        {
            return this._state;
        }
    }
    
}