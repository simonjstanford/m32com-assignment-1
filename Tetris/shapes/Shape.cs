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
        bool canMove = CanMoveDown(board);
        if (canMove)
        {
            foreach (Point coord in coords)
            {
                //is the block still on the board
                if (((coord.Y -1 > board[board.Length - 1].Length) || (coord.X > board.Length)) || ((coord.Y -1 < 0) || (coord.X < 0)))
                {
                    return false;
                }

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
    /// Checks if shape can move down.
    /// </summary>
    /// <param name="board">Game Board</param>
    /// <returns></returns>
    public bool CanMoveDown(string[][] board)
    {
        bool canMove = true;
        if (canMove)
        {
            foreach (Point coord in coords)
            {
                //is the block still on the board
                if (((coord.Y - 1 > board[board.Length - 1].Length) || (coord.X > board.Length)) || ((coord.Y - 1 < 0) || (coord.X < 0)))
                {
                    return false;
                }

                //check if a block is already filled by another shape.  
                if (!String.IsNullOrEmpty(board[coord.X][coord.Y - 1]))
                {
                    return false;
                }
            }
        }
        return canMove;
    }

    /// <summary>
    /// The shape coords are updated
    /// </summary>
    /// <param name="board">Game board</param>
    /// <returns></returns>
    public bool MoveLeft(string[][] board)
    {
        bool canMove = true;
        if (canMove)
        {
            foreach (Point coord in coords)
            {
                //is the block still on the board
                if (((coord.Y > board[board.Length - 1].Length) || (coord.X - 1 > board.Length -1 )) || ((coord.Y < 0) || (coord.X - 1 < 0)))
                {
                    return false;
                }

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
        bool canMove = true;
        if (canMove)
        {
            foreach (Point coord in coords)
            {
                //is the block still on the board
                if (((coord.Y > board[board.Length - 1].Length) || (coord.X + 1 > board.Length - 1)) || ((coord.Y < 0) || (coord.X + 1 < 0)))
                {
                    return false;
                }

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
            for (int k = 0; k < board[i].Length; k++)
            {
                newboard[i][k] = board[i][k];
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