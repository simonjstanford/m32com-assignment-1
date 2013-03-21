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
    public bool Active { get; private set; }
    protected string colourHexCode;
    protected Rotation rotation;
    protected Point[] coords;

    public Shape()
    {
        Active = true;
        rotation = Rotation.North;
    }

    public abstract bool Rotate(string[,] board);

    /// <summary>
    /// Checks if shape can move down. If true, the shape coords and board array are both updated
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public bool MoveDown(string[,] board)
    {
        bool canMove = true;

        //first, check if any block has hit the bottom of the grid
        foreach (Point coord in coords)
            if (coord.Y == 0)
                return false; //if so, return false

        //turn all blocks in the active shape white
        foreach (Point coord in coords)
            board[coord.X, coord.Y] = "FFFFFF";

        //check if a block is already filled by another shape.  
        foreach (Point coord in coords)
            if (board[coord.X, coord.Y - 1] != "FFFFFF")
                canMove = false; //If so, set canMove to false

        //if can move, shift all coords down and re-colour
        if (canMove)
            for (int i = 0; i < coords.Length; i++)
            {
                coords[i].Y -= 1;
                board[coords[i].X, coords[i].Y] = colourHexCode;
            }
        else //else just re-colour without shifting down
            for (int i = 0; i < coords.Length; i++)
            {
                board[coords[i].X, coords[i].Y] = colourHexCode;
            }

        if (canMove)
        {
            return true;
        }
        else
        {
            Active = false;
            return false;
        }
    }

    /// <summary>
    /// Checks if shape can move left. If true, the shape coords and board array are both updated
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public bool MoveLeft(string[,] board)
    {
        bool canMove = true;

        //first, check if any block has hit the far left of the grid
        foreach (Point coord in coords)
            if (coord.X == 0)
                return false; //if so, return false

        //turn all blocks in the active shape white
        foreach (Point coord in coords)
            board[coord.X, coord.Y] = "FFFFFF";

        //check if a block is already filled by another shape.  
        foreach (Point coord in coords)
            if (board[coord.X - 1, coord.Y] != "FFFFFF")
                canMove = false; //If so, set canMove to false

        //if can move, shift all coords left and re-colour
        if (canMove)
            for (int i = 0; i < coords.Length; i++)
            {
                coords[i].X -= 1;
                board[coords[i].X, coords[i].Y] = colourHexCode;
            }
        else //else just re-colour without shifting left
            for (int i = 0; i < coords.Length; i++)
            {
                board[coords[i].X, coords[i].Y] = colourHexCode;
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
    public bool MoveRight(string[,] board)
    {
        bool canMove = true;

        //first, check if any block has hit the far right of the grid
        foreach (Point coord in coords)
            if (coord.X == board.GetLength(0))
                return false; //if so, return false

        //turn all blocks in the active shape white
        foreach (Point coord in coords)
            board[coord.X, coord.Y] = "FFFFFF";

        //check if a block is already filled by another shape.  
        foreach (Point coord in coords)
            if (board[coord.X + 1, coord.Y] != "FFFFFF")
                canMove = false; //If so, set canMove to false

        //if can move, shift all coords left and re-colour
        if (canMove)
            for (int i = 0; i < coords.Length; i++)
            {
                coords[i].X += 1;
                board[coords[i].X, coords[i].Y] = colourHexCode;
            }
        else //else just re-colour without shifting left
            for (int i = 0; i < coords.Length; i++)
            {
                board[coords[i].X, coords[i].Y] = colourHexCode;
            }

        if (canMove)
            return true;
        else
            return false;
    }

    protected bool CheckIfCanMove(string[,] board)
    {
        //turn all blocks in the active shape white

        foreach (Point coord in coords)
        {
            if (board[coord.X, coord.Y] != "FFFFFF")
                return false; //If so, set canMove to false
        }
        return true;
    }
}