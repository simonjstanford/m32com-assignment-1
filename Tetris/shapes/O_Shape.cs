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
    /// <summary>
    /// Initializes a new instance of the <see cref="O_Shape"/> class.
    /// </summary>
    /// <param name="topMiddleXCord">The top middle X cord.</param>
    /// <param name="topMiddleYCord">The top middle Y cord.</param>
    public O_Shape(int topMiddleXCord, int topMiddleYCord)
        : base()
    {
        colourHexCode = "FFFF00";
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord - 1, topMiddleYCord - 1), new Point(topMiddleXCord, topMiddleYCord - 1) };
        base.Type = ShapeTypes.O;
    }

    /// <summary>
    /// Used when moving the shape from next shape board to game board
    /// </summary>
    /// <param name="topMiddleXCord"></param>
    /// <param name="topMiddleYCord"></param>
    override public void Reposition(int topMiddleXCord, int topMiddleYCord)
    {
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord - 1, topMiddleYCord - 1), new Point(topMiddleXCord, topMiddleYCord - 1) };
    }

    /// <summary>
    /// Rotates the shape
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    override public bool Rotate(string[][] board)
    {
        return true; //O can always rotate
    }

}