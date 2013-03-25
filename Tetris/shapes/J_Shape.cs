using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

/// <summary>
/// Summary description for J_Shape
/// </summary>
public class J_Shape : Shape
{
    public J_Shape(int topMiddleXCord, int topMiddleYCord)
        : base()
    {
        colourHexCode = "0000FF";
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord + 1, topMiddleYCord), new Point(topMiddleXCord + 1, topMiddleYCord - 1) };
        base.Type = ShapeTypes.J;
    }

    override public void Reposition(int topMiddleXCord, int topMiddleYCord)
    {
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord + 1, topMiddleYCord), new Point(topMiddleXCord + 1, topMiddleYCord - 1) };
    }

    override public bool Rotate(string[][] board)
    {
        bool canMove = CheckIfCanMove(board);
        if (canMove)
        {

            switch (rotation)
            {

                case Rotation.North:
                    rotation = Rotation.East;
                    coords[0].X += 2; coords[0].Y += 2;
                    coords[1].X += 1; coords[1].Y += 1;
                    coords[3].X-= 1; coords[3].Y += 1;
                    break;

                case Rotation.East:
                    rotation = Rotation.South;
                    coords[0].X += 2; coords[0].Y -= 2;
                    coords[1].X += 1; coords[1].Y -= 1;
                    coords[3].X += 1; coords[3].Y += 1;
                    break;

                case Rotation.South:
                    rotation = Rotation.West;
                    coords[0].X -= 2; coords[0].Y -= 2;
                    coords[1].X -= 1; coords[1].Y -= 1;
                    coords[3].X += 1; coords[3].Y -= 1;
                    break;

                case Rotation.West:
                    rotation = Rotation.North;
                    coords[0].X -= 2; coords[0].Y += 2;
                    coords[1].X -= 1; coords[1].Y += 1;
                    coords[3].X -= 1; coords[3].Y -= 1;
                    break;
            }
        }
        foreach (Point coord in coords)
        {
            //check if a block is already filled by another shape.  
            if (!String.IsNullOrEmpty(board[coord.X][coord.Y]))
            {
                return false;
            }
        }
        return canMove;
    }
}