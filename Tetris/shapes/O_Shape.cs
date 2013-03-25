using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

/// <summary>
/// Summary description for J_Shape
/// </summary>
public class O_Shape : Shape
{
    public O_Shape(int topMiddleXCord, int topMiddleYCord)
        : base()
    {
        colourHexCode = "FFFF00";
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord - 1, topMiddleYCord - 1), new Point(topMiddleXCord, topMiddleYCord - 1) };
        base.Type = ShapeTypes.O;
    }

    override public void Reposition(int topMiddleXCord, int topMiddleYCord)
    {
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord - 1, topMiddleYCord - 1), new Point(topMiddleXCord, topMiddleYCord - 1) };
    }

    override public bool Rotate(string[][] board)
    {
        return true; //O can always rotate
    }

}