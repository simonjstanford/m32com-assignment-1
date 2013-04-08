using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

/// <summary>
/// Summary description for I_Shape
/// </summary>
public class T_Shape : Shape
{
    public T_Shape(int topMiddleXCord, int topMiddleYCord)
        : base()
    {
        colourHexCode = "800080";
        Reposition(topMiddleXCord, topMiddleYCord);
        base.Type = ShapeTypes.T;
    }

    override public void Reposition(int topMiddleXCord, int topMiddleYCord)
    {
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord + 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord - 1) };
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
                coords[3].X -= 1; coords[3].Y += 1;
                break;

            case Rotation.East:
                rotation = Rotation.South;
                coords[0].X += 1; coords[0].Y -= 1;
                coords[2].X -= 1; coords[2].Y += 1;
                coords[3].X += 1; coords[3].Y += 1;
                break;

            case Rotation.South:
                rotation = Rotation.West;
                coords[0].X -= 1; coords[0].Y -= 1;
                coords[2].X += 1; coords[2].Y += 1;
                coords[3].X += 1; coords[3].Y -= 1;
                break;

            case Rotation.West:
                rotation = Rotation.North;
                coords[0].X -= 1; coords[0].Y += 1;
                coords[2].X += 1; coords[2].Y -= 1;
                coords[3].X -= 1; coords[3].Y -= 1;
                break;
        }

        return base.moveRotate(board, oldRotation, oldCoords);
    }

}