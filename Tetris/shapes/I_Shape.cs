﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

/// <summary>
/// Summary description for I_Shape
/// </summary>
public class I_Shape : Shape
{
    public I_Shape(int topMiddleXCord, int topMiddleYCord)
        : base()
    {
        colourHexCode = "00FFFF";
        coords = new Point[] { new Point(topMiddleXCord - 2, topMiddleYCord), new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord + 1, topMiddleYCord) };
    }

    override public bool Rotate(string[,] board)
    {
        Point[] previousPosition = coords;
        
        foreach (Point coord in coords)
            board[coord.X, coord.Y] = "FFFFFF";

        switch (rotation)
        {
                
            case Rotation.North:
                rotation = Rotation.East;
                coords[0].X += 2; coords[0].Y += 2;
                coords[1].X += 1; coords[1].Y += 1;
                coords[3].X -= 1; coords[3].Y -= 1;
                break;

            case Rotation.East:
                rotation = Rotation.South;
                coords[0].X += 2; coords[0].Y -= 2;
                coords[1].X += 1; coords[1].Y -= 1;
                coords[3].X += 1; coords[3].Y -= 1;
                break;

            case Rotation.South:
                rotation = Rotation.West;
                coords[0].X -= 2; coords[0].Y -= 2;
                coords[1].X -= 1; coords[1].Y -= 1;
                coords[3].X += 1; coords[3].Y += 1;
                break;

            case Rotation.West:
                rotation = Rotation.North;
                coords[0].X -= 2; coords[0].Y += 2;
                coords[1].X -= 1; coords[1].Y += 1;
                coords[3].X += 1; coords[3].Y -= 1;
                break;
        }


        if (base.CheckIfCanMove(board))
        {
            try
            {
                foreach (Point coord in coords)
                    board[coord.X, coord.Y] = "00FFFF";
                return true;
            }
            catch (Exception)
            {
                coords = previousPosition;
                foreach (Point coord in coords)
                    board[coord.X, coord.Y] = "00FFFF";
                return false;
            }

        }
        else
        {
            coords = previousPosition;
            foreach (Point coord in coords)
                board[coord.X, coord.Y] = "00FFFF";
            return false;
        }
    }


}