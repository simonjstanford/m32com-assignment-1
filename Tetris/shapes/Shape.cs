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
        bool canMove = true;

        //first, check if any block has hit the bottom of the grid
        foreach (Point coord in coords)
            if (coord.Y == 0)
                return false; //if so, return false

        //turn all blocks in the active shape white
        //        foreach (Point coord in coords)
        //            board[coord.X][coord.Y] = "FFFFFF";

        //check if a block is already filled by another shape.  
        foreach (Point coord in coords)
            if (board[coord.X][coord.Y - 1] != "FFFFFF")
                canMove = false; //If so, set canMove to false

        //if can move move the active shape.
        for (int i = 0; i < coords.Length; i++)
        {
            coords[i].Y = coords[i].Y - 1;
        }

        if (canMove)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if shape can move left. If true, the shape coords and board array are both updated
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public bool MoveLeft(string[][] board)
    {
        bool canMove = true;

        //first, check if any block has hit the far left of the grid
        foreach (Point coord in coords)
            if (coord.X == 0)
                return false; //if so, return false

        //turn all blocks in the active shape white
        foreach (Point coord in coords)
            board[coord.X][coord.Y] = "FFFFFF";

        //check if a block is already filled by another shape.  
        foreach (Point coord in coords)
            if (board[coord.X - 1][coord.Y] != "FFFFFF")
                canMove = false; //If so, set canMove to false

        //if can move, shift all coords left and re-colour
        if (canMove)
            for (int i = 0; i < coords.Length; i++)
            {
                coords[i].X -= 1;
                board[coords[i].X][coords[i].Y] = colourHexCode;
            }
        else //else just re-colour without shifting left
            for (int i = 0; i < coords.Length; i++)
            {
                board[coords[i].X][coords[i].Y] = colourHexCode;
            }

        if (canMove)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Checks if shape can move right. If true, the shape coords and board array are both updated
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public bool MoveRight(string[][] board)
    {
        bool canMove = true;

        //first, check if any block has hit the far right of the grid
        foreach (Point coord in coords)
            if (coord.X == board.GetLength(0))
                return false; //if so, return false

        //turn all blocks in the active shape white
        foreach (Point coord in coords)
            board[coord.X][coord.Y] = "FFFFFF";

        //check if a block is already filled by another shape.  
        foreach (Point coord in coords)
            if (board[coord.X + 1][coord.Y] != "FFFFFF")
                canMove = false; //If so][ set canMove to false

        //if can move, shift all coords left and re-colour
        if (canMove)
            for (int i = 0; i < coords.Length; i++)
            {
                coords[i].X += 1;
                board[coords[i].X][coords[i].Y] = colourHexCode;
            }
        else //else just re-colour without shifting left
            for (int i = 0; i < coords.Length; i++)
            {
                board[coords[i].X][coords[i].Y] = colourHexCode;
            }

        if (canMove)
            return true;
        else
            return false;
    }

    protected bool CheckIfCanMove(string[][] board)
    {
        //turn all blocks in the active shape white

        foreach (Point coord in coords)
        {
            if (board[coord.X][coord.Y] != "FFFFFF")
                return false; //If so, set canMove to false
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
        return newboard;
    }
}