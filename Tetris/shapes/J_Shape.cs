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
        Reposition(topMiddleXCord, topMiddleYCord);
        base.Type = ShapeTypes.J;
    }

    override public void Reposition(int topMiddleXCord, int topMiddleYCord)
    {
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord + 1, topMiddleYCord), new Point(topMiddleXCord + 1, topMiddleYCord - 1) };
    }

    override public bool Rotate(string[][] board)
    {
        Point[] oldCoords = new Point[4];
        oldCoords = coords.ToArray();
        Rotation oldRotation = new Rotation();
        oldRotation = rotation;

        switch (rotation)
        {
            case Rotation.North:
                rotation = Rotation.East;
                coords[0].X += 1; coords[0].Y += 1;
                coords[2].X -= 1; coords[2].Y -= 1;
                coords[3].X -= 2; coords[3].Y += 0;
                break;

            case Rotation.East:
                rotation = Rotation.South;
                coords[0].X += 1; coords[0].Y -= 1;
                coords[2].X -= 1; coords[2].Y += 1;
                coords[3].X += 0; coords[3].Y += 2;
                break;

            case Rotation.South:
                rotation = Rotation.West;
                coords[0].X -= 1; coords[0].Y -= 1;
                coords[2].X += 1; coords[2].Y += 1;
                coords[3].X += 2; coords[3].Y -= 0;
                break;

            case Rotation.West:
                rotation = Rotation.North;
                coords[0].X -= 1; coords[0].Y += 1;
                coords[2].X += 1; coords[2].Y -= 1;
                coords[3].X -= 0; coords[3].Y -= 2;
                break;
        }

        return base.moveRotate(board, oldRotation, oldCoords);
    }
}