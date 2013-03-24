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
            _nextShape.Reposition(_boardWidth / 2 - 1, _boardHeight - 1);
            _activeShape = _nextShape;
            _nextShape = getRandomShape(2, 2);
            return false;
        }
    }

    public void CheckFullRows()
    {
        //TODO:
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

    public string[][] ToArray()
    {
        return _activeShape.AddShapeToBoard(_board);
    }
}