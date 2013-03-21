using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Board
/// </summary>
public class Board
{
    public string[,] board;
    private Shape activeShape;
    private Shape nextShape;
    private int boardWidth;
    private int boardHeight;

	public Board()
	{
        boardHeight = 50;
        boardWidth = 30;
        
        startGame();
	}

    private void startGame()
    {
        board = new string[boardWidth, boardHeight];
        activeShape = getRandomShape();
        nextShape = getRandomShape();
    }

    public bool MoveActiveShapeLeft()
    {

        return activeShape.MoveLeft(board); //return true if shape moved, false otherwise
    }

    public bool MoveActiveShapeRight()
    {

        return activeShape.MoveRight(board); //return true if shape moved, false otherwise
    }

    public bool MoveActiveShapeDown()
    {

        if (activeShape.MoveDown(board))
        {
            activeShape = nextShape;
            nextShape = getRandomShape();
            
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
        throw new NotImplementedException();
    }

    public bool RotateActiveShape()
    {

        return activeShape.Rotate(board); //return true if shape moved, false otherwise
    }
}