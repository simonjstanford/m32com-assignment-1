﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

/// <summary>
/// Summary description for J_Shape
/// </summary>
public class L_Shape : Shape
{
    public L_Shape(int topMiddleXCord, int topMiddleYCord)
        : base()
    {
        colourHexCode = "FFA500";
        Reposition(topMiddleXCord, topMiddleYCord);
        base.Type = ShapeTypes.L;
    }

    override public void Reposition(int topMiddleXCord, int topMiddleYCord)
    {
        coords = new Point[] { new Point(topMiddleXCord - 1, topMiddleYCord), new Point(topMiddleXCord, topMiddleYCord), new Point(topMiddleXCord + 1, topMiddleYCord), new Point(topMiddleXCord - 1, topMiddleYCord - 1) };
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
                    coords[1].X -= 1; coords[1].Y -= 1;
                    coords[2].X -= 2; coords[2].Y -= 2;
                    coords[3].X -= 1; coords[3].Y += 1;
                    break;

                case Rotation.East:
                    rotation = Rotation.South;
                    coords[1].X -= 1; coords[1].Y += 1;
                    coords[2].X -= 2; coords[2].Y += 2;
                    coords[3].X += 1; coords[3].Y += 1;
                    break;

                case Rotation.South:
                    rotation = Rotation.West;
                    coords[1].X += 1; coords[1].Y += 1;
                    coords[2].X += 2; coords[2].Y += 2;
                    coords[3].X += 1; coords[3].Y -= 1;
                    break;

                case Rotation.West:
                    rotation = Rotation.North;
                    coords[1].X += 1; coords[1].Y -= 1;
                    coords[2].X += 2; coords[2].Y -= 2;
                    coords[3].X -= 1; coords[3].Y -= 1;
                    break;
            }
        }
        foreach (Point coord in coords)
        {
            //is the block still on the board
            if ((coord.Y > board[board.Length - 1].Length - 1) || (coord.X > board.Length - 1) || (coord.Y < 0) || (coord.X < 0))
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