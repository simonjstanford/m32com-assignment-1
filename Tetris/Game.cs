using System;

/// <summary>
/// The game and its status
/// </summary>
public sealed class Game
{
    // Fields
    private Board board;
    private int score;
    private State state;

    // Methods
    public Game()
    {
        board = new Board();

    }

    private void CheckFullRows()
    {
    }

    public bool DropBlock()
    {
        return false;
    }

    private void GetNextBlock()
    {
    }

    public bool MoveBlockDown()
    {
        return board.MoveActiveShapeDown();
    }

    public bool MoveBlockLeft()
    {
        return board.MoveActiveShapeDown();
    }

    public bool MoveBlockRight()
    {
        return board.MoveActiveShapeRight();
    }

    public void Restart()
    {
    }

    public bool RotateBlock()
    {
        return board.RotateActiveShape();
    }

}