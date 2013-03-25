using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Board
/// </summary>
public class Board
{
    public string[][] _board;
    private Shape _activeShape;
    private Shape _nextShape;
    private int _boardWidth;
    private int _boardHeight;
    private Game _game;

    public Board()
    {
        _boardHeight = 30;
        _boardWidth = 12;

        startGame();
    }

    private void startGame()
    {
        _board = new string[_boardWidth][];
        for (int i = 0; i < _boardWidth ; i++)
        {
            _board[i] = new string[_boardHeight];
            for (int j = 0; j < _boardHeight; j++)
            {
                _board[i][j] = string.Empty;
            }
        }


        //debug set bottom row complete
        //for (int j = 0; j < _boardHeight; j++)
        //{
        //    _board[0][j] = "EEEEEE";
        //}


        _activeShape = getRandomShape(_boardWidth / 2, _boardHeight -1);
        _nextShape = getRandomShape(2, 2);
    }

    public bool MoveActiveShapeLeft()
    {
        return _activeShape.MoveLeft(_board); //return true if shape moved, false otherwise
    }

    public bool MoveActiveShapeRight()
    {
        return _activeShape.MoveRight(_board); //return true if shape moved, false otherwise
    }

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

    public bool RemoveRow(int rowNumber)
    {
        try
        {
            for (int i = rowNumber; i >= 0; i--)
            {
                for (int j = 0; j < _boardWidth; j++)
                {
                    if (i != 0)
                    {
                        _board[j][i] = _board[j][i - 1];
                    }
                    else
                    {
                        _board[j][i] = String.Empty;
                    }
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool DropBlock()
    {
        bool flag = false;
        while (MoveActiveShapeDown())
        {
            flag = true;
        }
        return flag;
    }

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

    public bool RotateActiveShape()
    {
        return _activeShape.Rotate(_board); //return true if shape moved, false otherwise
    }

    // Properties
    public Shape NextShape
    {
        get
        {
            return _nextShape;
        }
    }

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

    public string[][] ToArray()
    {
        return _activeShape.AddShapeToBoard(_board);
    }
}