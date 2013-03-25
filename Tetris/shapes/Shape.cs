using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

/// <summary>
/// Summary description for Shape
/// </summary>
public abstract class Shape
{
    protected string colourHexCode;
    protected Rotation rotation;
    protected Point[] coords;
    public ShapeTypes Type { get; protected set; }

    public Shape()
    {
        rotation = Rotation.North;
    }

    /// <summary>
    /// Used when moving the shape from next shape board to game board
    /// </summary>
    /// <param name="topMiddleXCord"></param>
    /// <param name="topMiddleYCord"></param>
    /// <returns></returns>
    public abstract void Reposition(int topMiddleXCord, int topMiddleYCord);

    /// <summary>
    /// Rotates the shape
    /// </summary>
    /// /// <param name="board"></param>
    /// <returns></returns>
    public abstract bool Rotate(string[][] board);

    /// <summary>
    /// Checks if shape can move down. If true, the shape coords and board array are both updated
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public bool MoveDown(string[][] board)
    {
        bool canMove = CheckIfCanMove(board);
        if (canMove)
        {
            foreach (Point coord in coords)
            {
                //check if a block is already filled by another shape.  
                if (!String.IsNullOrEmpty(board[coord.X][coord.Y - 1]))
                {
                    return false;
                }
            }

            for (int i = 0; i < coords.Length; i++)
            {
                coords[i].Y -= 1;
            }
        }
        return canMove;
    }

    /// <summary>
    /// Checks if shape can move left. If true, the shape coords and board array are both updated
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public bool MoveLeft(string[][] board)
    {
        bool canMove = CheckIfCanMove(board);
        if (canMove)
        {
            foreach (Point coord in coords)
            {
                //check if a block is already filled by another shape.  
                if (!String.IsNullOrEmpty(board[coord.X - 1][coord.Y]))
                {
                    return false;
                }
            }

            for (int i = 0; i < coords.Length; i++)
            {
                coords[i].X -= 1;
            }
        }
        return canMove;
    }

    /// <summary>
    /// Checks if shape can move right. If true, the shape coords and board array are both updated
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public bool MoveRight(string[][] board)
    {
        bool canMove = CheckIfCanMove(board);
        if (canMove)
        {
            foreach (Point coord in coords)
            {
                //check if a block is already filled by another shape.  
                if (!String.IsNullOrEmpty(board[coord.X + 1][coord.Y]))
                {
                    return false;
                }
            }

            for (int i = 0; i < coords.Length; i++)
            {
                coords[i].X += 1;
            }
        }
        return canMove;
    }

    protected bool CheckIfCanMove(string[][] board)
    {
        foreach (Point coord in coords)
        {
            //is block off board or at the bottom
            //Length is one more than grid
            if (((coord.Y >= board[board.Length - 1].Length) || (coord.X >= board.Length)) || ((coord.Y <= 0) || (coord.X <= 0)))
            {
                return false;
            }
        }
        return true;
    }

    public string[][] ToArray()
    {
        string[][] shapeArray = new string[4][];

        for (int i = 0; i < shapeArray.Length; i++)
        {
            shapeArray[i] = new string[4];
        }

        foreach (Point coord in coords)
        {
            shapeArray[coord.X][coord.Y] = colourHexCode;
        }
        return shapeArray;
    }

    public string[][] AddShapeToBoard(string[][] board)
    {
        string[][] newboard = new string[board.Length][];
        for (int i = 0; i < board.Length; i++)
        {
            newboard[i] = new string[board[board.Length - 1].Length];
        }

        for (int j = 0; j < board.Length; j++)
        {
            for (int k = 0; k < board[j].Length; k++)
            {
                newboard[j][k] = board[j][k];
            }
        }

        foreach (Point coord in coords)
        {
            newboard[coord.X][coord.Y] = colourHexCode;
        }


        //Used for debug sets the active shape to 0,1,2,3 cell number.
        //for (int i = 0; i < coords.Length; i++)
        //{
        //    newboard[coords[i].X][coords[i].Y] = i.ToString();
        //}

        return newboard;
    }
}