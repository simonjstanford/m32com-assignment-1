using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Board
/// </summary>
public class Board
{
    public string[,] _board;
    private Shape _activeShape;
    private Shape _nextShape;
    private int _boardWidth;
    private int _boardHeight;

	public Board()
	{
        _boardHeight = 50;
        _boardWidth = 30;
        
        startGame();
	}

    private void startGame()
    {
        _board = new string[_boardWidth, _boardHeight];
        _activeShape = getRandomShape();
        _nextShape = getRandomShape();
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
            _activeShape = _nextShape;
            _nextShape = getRandomShape();
            
            return false;
        }
        else
        {

            return true;
        }

        //return activeShape.MoveDown(Board); //return true if shape moved, false otherwise
    }

    private Shape getRandomShape()
    {
        return new I_Shape(4,4);
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

}