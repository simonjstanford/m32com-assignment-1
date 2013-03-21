using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

/// <summary>
/// Summary description for I_Shape
/// </summary>
public class S_Shape : Shape
{
    public S_Shape(int topMiddleXCord, int topMiddleYCord)
        : base()
    {
        colourHexCode = "00FF00";
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord - 1), new Point(topMiddleXCord, topMiddleYCord - 1), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord + 1, topMiddleYCord) };


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
                coords[0].X += 0; coords[1].Y += 2;
                coords[1].X -= 1; coords[1].Y += 1;
                coords[3].X -= 1; coords[3].Y -= 1;
                break;

            case Rotation.East:
                rotation = Rotation.South;
                coords[0].X += 2; coords[1].Y += 0;
                coords[1].X += 1; coords[1].Y += 1;
                coords[3].X -= 1; coords[3].Y += 1;
                break;

            case Rotation.South:
                rotation = Rotation.West;
                coords[0].X += 0; coords[1].Y -= 2;
                coords[1].X += 1; coords[1].Y -= 1;
                coords[3].X += 1; coords[3].Y += 1;
                break;

            case Rotation.West:
                rotation = Rotation.North;
                coords[0].X -= 2; coords[1].Y += 0;
                coords[1].X -= 1; coords[1].Y -= 1;
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