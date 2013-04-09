using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Game board
/// </summary>
public class Board
{
    public string[][] _board;
    private Shape _activeShape;
    private Shape _nextShape;
    private int _boardWidth;
    private int _boardHeight;
    private Game _game;

    /// <summary>
    /// Initializes a new instance of the <see cref="Board"/> class.
    /// </summary>
    public Board()
    {
        _boardHeight = 25;
        _boardWidth = 12;

        startGame();
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    private void startGame()
    {
        _board = new string[_boardWidth][];
        for (int i = 0; i < _boardWidth; i++)
        {
            _board[i] = new string[_boardHeight];
            for (int j = 0; j < _boardHeight; j++)
            {
                _board[i][j] = string.Empty;
            }
        }

        _activeShape = getRandomShape(_boardWidth / 2, _boardHeight - 1);
        _nextShape = getRandomShape(2, 2);
    }

    /// <summary>
    /// Moves the active shape left.
    /// </summary>
    /// <returns></returns>
    public bool MoveActiveShapeLeft()
    {
        return _activeShape.MoveLeft(_board); //return true if shape moved, false otherwise
    }

    /// <summary>
    /// Moves the active shape right.
    /// </summary>
    /// <returns></returns>
    public bool MoveActiveShapeRight()
    {
        return _activeShape.MoveRight(_board); //return true if shape moved, false otherwise
    }

    /// <summary>
    /// Moves the active shape down.
    /// </summary>
    /// <returns></returns>
    public bool MoveActiveShapeDown()
    {
        if (_activeShape.MoveDown(_board))
        {
            return true;
        }
        else
        {
            _board = ToArray();
            CheckFullRows();
            _nextShape.Reposition(_boardWidth / 2 - 1, _boardHeight - 1);
            _activeShape = _nextShape;
            _nextShape = getRandomShape(2, 2);

            if (!_activeShape.CanMoveDown(_board))
            {
                Game.State = State.GameOver;
            }
            return false;
        }
    }

    /// <summary>
    /// Checks for full rows.
    /// </summary>
    public void CheckFullRows()
    {
        for (int i = 0; i < _boardHeight; i++)
        {
            bool flag = true;
            for (int j = 0; j < _boardWidth; j++)
            {
                if (String.IsNullOrEmpty(_board[j][i]))
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                RemoveRow(i);
                _game.Score += 10;
                CheckFullRows();
            }
        }
    }

    /// <summary>
    /// Removes the row and moves all rows above down.
    /// </summary>
    /// <param name="rowNumber">The row number to remove.</param>
    /// <returns></returns>
    public bool RemoveRow(int rowNumber)
    {
        try
        {
            //for each row form row to be removed to the _board height.....
            for (int i = rowNumber; i <= _boardHeight - 1; i++)
            {
                for (int j = 0; j < _boardWidth; j++)
                {
                    //move cell down
                    _board[j][i] = _board[j][i + 1];
                    //empty old cell
                    _board[j][i + 1] = String.Empty;
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Drops the block.
    /// </summary>
    /// <returns></returns>
    public bool DropBlock()
    {
        bool flag = false;
        while (MoveActiveShapeDown())
        {
            flag = true;
        }
        return flag;
    }

    /// <summary>
    /// Gets the next random shape.
    /// </summary>
    /// <param name="topMiddleXCord">The top middle X cord.</param>
    /// <param name="topMiddleYCord">The top middle Y cord.</param>
    /// <returns></returns>
    private Shape getRandomShape(int topMiddleXCord, int topMiddleYCord)
    {
        Random random = new Random();

        switch (random.Next(0, 7))
        {
            case 0:
                return new I_Shape(topMiddleXCord, topMiddleYCord);
            case 1:
                return new J_Shape(topMiddleXCord, topMiddleYCord);
            case 2:
                return new L_Shape(topMiddleXCord, topMiddleYCord);
            case 3:
                return new O_Shape(topMiddleXCord, topMiddleYCord);
            case 4:
                return new S_Shape(topMiddleXCord, topMiddleYCord);
            case 5:
                return new T_Shape(topMiddleXCord, topMiddleYCord);
            case 6:
                return new Z_Shape(topMiddleXCord, topMiddleYCord);
        }

        return null;
    }

    /// <summary>
    /// Rotates the active shape.
    /// </summary>
    /// <returns></returns>
    public bool RotateActiveShape()
    {
        return _activeShape.Rotate(_board); //return true if shape moved, false otherwise
    }

    // Properties

    /// <summary>
    /// Gets the next shape.
    /// </summary>
    /// <value>
    /// The next shape.
    /// </value>
    public Shape NextShape
    {
        get
        {
            return _nextShape;
        }
    }

    /// <summary>
    /// Gets or sets the game.
    /// </summary>
    /// <value>
    /// The game.
    /// </value>
    public Game Game
    {
        get
        {
            return _game;
        }
        set
        {
            _game = value;
        }
    }

    /// <summary>
    /// Turns the borad to an array.
    /// </summary>
    /// <returns></returns>
    public string[][] ToArray()
    {
        return _activeShape.AddShapeToBoard(_board);
    }
}