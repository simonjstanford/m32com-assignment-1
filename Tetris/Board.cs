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
        for (int i = 0; i < _boardWidth; i++)
        {
            _board[i] = new string[_boardHeight];
        }

        _activeShape = getRandomShape(_boardWidth/2,_boardHeight);
        _nextShape = getRandomShape(2,2);
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
            _nextShape.Reposition(_boardWidth/2, _boardHeight);
            _activeShape = _nextShape;
            _nextShape = getRandomShape(2,2);   
            return false;
        }
        else
        {
            return true;
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
        return new I_Shape(topMiddleXCord,topMiddleYCord);
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