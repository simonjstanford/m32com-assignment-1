using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

/// <summary>
/// Summary description for I_Shape
/// </summary>
public class Z_Shape : Shape
{
    public Z_Shape(int topMiddleXCord, int topMiddleYCord)
        : base()
    {
        colourHexCode = "FF0000";
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord -1 ), new Point(topMiddleXCord + 1, topMiddleYCord - 1) };
        base.Type = ShapeTypes.Z;
    }

    override public void Reposition(int topMiddleXCord, int topMiddleYCord) 
    {
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord - 1), new Point(topMiddleXCord + 1, topMiddleYCord - 1) };
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
                    coords[0].X += 1; coords[0].Y += 1;
                    coords[2].X -= 1; coords[2].Y += 1;
                    coords[3].X -= 2; coords[3].Y += 0;
                    break;

                case Rotation.East:
                    rotation = Rotation.South;
                    coords[0].X += 1; coords[0].Y -= 1;
                    coords[2].X += 1; coords[2].Y += 1;
                    coords[3].X += 0; coords[3].Y += 2;
                    break;

                case Rotation.South:
                    rotation = Rotation.West;
                    coords[0].X -= 1; coords[0].Y -= 1;
                    coords[2].X += 1; coords[2].Y -= 1;
                    coords[3].X += 2; coords[3].Y -= 0;
                    break;

                case Rotation.West:
                    rotation = Rotation.North;
                    coords[0].X -= 1; coords[0].Y += 1;
                    coords[2].X -= 1; coords[2].Y -= 1;
                    coords[3].X += 0; coords[3].Y -= 2;
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