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
    /// <summary>
    /// Initializes a new instance of the <see cref="Z_Shape"/> class.
    /// </summary>
    /// <param name="topMiddleXCord">The top middle X cord.</param>
    /// <param name="topMiddleYCord">The top middle Y cord.</param>
    public Z_Shape(int topMiddleXCord, int topMiddleYCord)
        : base()
    {
        colourHexCode = "FF0000";
        Reposition(topMiddleXCord, topMiddleYCord);
        base.Type = ShapeTypes.Z;
    }

    /// <summary>
    /// Used when moving the shape from next shape board to game board
    /// </summary>
    /// <param name="topMiddleXCord"></param>
    /// <param name="topMiddleYCord"></param>
    override public void Reposition(int topMiddleXCord, int topMiddleYCord)
    {
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord - 1), new Point(topMiddleXCord + 1, topMiddleYCord - 1) };
    }

    /// <summary>
    /// Rotates the shape
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
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

        return base.moveRotate(board, oldRotation, oldCoords);
    }
}