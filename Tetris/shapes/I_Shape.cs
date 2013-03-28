using System;
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
        Reposition(topMiddleXCord, topMiddleYCord);
        base.Type = ShapeTypes.I;
    }

    override public void Reposition(int topMiddleXCord, int topMiddleYCord)
    {
        coords = new Point[] { new Point(topMiddleXCord - 2, topMiddleYCord), new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord + 1, topMiddleYCord) };
    }

    override public bool Rotate(string[][] board)
    {
        bool canMove = true;
        Point[] oldCoords = new Point[4];
        oldCoords = coords.ToArray();
        Rotation oldRotation = new Rotation();
        oldRotation = rotation;

        if (canMove)
        {
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
                    coords[0].X -= 2; coords[0].Y -= 2;
                    coords[1].X -= 1; coords[1].Y -= 1;
                    coords[3].X += 1; coords[3].Y += 1;
                    break;

                case Rotation.South:
                    rotation = Rotation.West;
                    coords[0].X += 2; coords[0].Y += 2;
                    coords[1].X += 1; coords[1].Y += 1;
                    coords[3].X -= 1; coords[3].Y -= 1;
                    break;

                case Rotation.West:
                    rotation = Rotation.North;
                    coords[0].X -= 2; coords[0].Y -= 2;
                    coords[1].X -= 1; coords[1].Y -= 1;
                    coords[3].X += 1; coords[3].Y += 1;
                    break;
            }

        }
        foreach (Point coord in coords)
        {
            //is the block still on the board
            if (((coord.Y > board[board.Length - 1].Length) || (coord.X > board.Length)) || ((coord.Y < 0) || (coord.X < 0)))
            {
                coords = oldCoords;
                rotation = oldRotation;
                return false;
            }

            //check if a block is already filled by another shape.  
            if (!String.IsNullOrEmpty(board[coord.X][coord.Y]))
            {
                coords = oldCoords;
                rotation = oldRotation;
                return false;
            }
        }
        return canMove;
    }
}