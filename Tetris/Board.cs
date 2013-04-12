using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Game board
/// </summary>
public class Board
{
    #region Class Fields 

    public string[][] _board; //the jagged array representing the game board.  Contains the hexidecimal colour codes 
    private Shape _activeShape; //the shape that is currently moving down the board
    private Shape _nextShape; //the next shape that will appear on the board
    private int _boardWidth; //the board width
    private int _boardHeight; //the board height
    private Game _game; //the game object

    #endregion

    /// <summary>
    /// Constructor that initializes a new instance of the <see cref="Board"/> class.
    /// </summary>
    public Board()
    {
        //set the size of the board
        _boardHeight = 25;
        _boardWidth = 12;

        startGame(); //then create it
    }

    #region Class Properties

    /// <summary>
    /// Gets the next shape.
    /// </summary>
    /// <value>The next shape that will appear on the board</value>
    public Shape NextShape
    {
        get
        {
            return _nextShape;
        }
    }

    /// <summary>
    /// Gets or sets the Game object
    /// </summary>
    /// <value>The current Game object</value>
    public Game Game
    {
        get
        {
            return _game;
        }
        set
        {
            _game = value;
        }
    }

    #endregion

    #region Shape Movement Methods

    /// <summary>
    /// Moves the active shape left.
    /// </summary>
    /// <returns>True if the shape was moved successfully, false if not</returns>
    public bool MoveActiveShapeLeft()
    {
        return _activeShape.MoveLeft(_board); //return true if shape moved, false otherwise
    }

    /// <summary>
    /// Moves the active shape right.
    /// </summary>
    /// <returns>True if the shape was moved successfully, false if not</returns>
    public bool MoveActiveShapeRight()
    {
        return _activeShape.MoveRight(_board); //return true if shape moved, false otherwise
    }

    /// <summary>
    /// Moves the active shape down one row.
    /// </summary>
    /// <returns>True if the shape was moved successfully, false if not</returns>
    public bool MoveActiveShapeDown()
    {
        //try and move the shape down the board one row
        if (_activeShape.MoveDown(_board))
        {
            //if it has not hit the bottom or landed on another shape, return true
            return true;
        }
        else
        {
            //if it cant move down...
            _board = ToArray(); //update the array with the latest positions
            CheckFullRows(); //check for any full rows
            _nextShape.Reposition(_boardWidth / 2 - 1, _boardHeight - 1); //move the next shape onto the game board
            _activeShape = _nextShape; //and change the references, so that the player can move it
            _nextShape = getRandomShape(2, 2); //create a new random shape that will appear next

            //check to see if the new shape can move at all
            if (!_activeShape.CanMoveDown(_board))
            {
                //if not, then it is game over
                Game.State = State.GameOver;
            }
            return false; //as the shape hasn't moved, return false
        }
    }

    /// <summary>
    /// Drops the active shape all the way to the bottom.  Executes when the player presses the down button
    /// </summary>
    /// <returns></returns>
    public bool DropBlock()
    {
        //call MoveActiveShapeDown() until it returns false 
        //(i.e. the shape has hit the bottom of the game board or another shape)
        bool flag = false;
        while (MoveActiveShapeDown())
        {
            flag = true;
        }
        return flag; //return the result
    }

    /// <summary>
    /// Rotates the active shape.
    /// </summary>
    /// <returns>True if the shape was moved successfully, false if not</returns>
    public bool RotateActiveShape()
    {
        return _activeShape.Rotate(_board); //return true if shape moved, false otherwise
    }

    #endregion

    #region Row Checking Methods

    /// <summary>
    /// Checks for full rows on the game board, and remove them
    /// </summary>
    public void CheckFullRows()
    {
        //for each row
        for (int i = 0; i < _boardHeight; i++)
        {
            bool flag = true; //bool to flag if the row needs to be removed
            //check each cell.  Assume that the row is full and needs to be removed
            for (int j = 0; j < _boardWidth; j++)
            {
                //check each cell in the row.  If a cell doesn't have a colour code in it then the row is not complete
                if (String.IsNullOrEmpty(_board[j][i]))
                {
                    //so mark the row as incomplete and start iterating through the next row
                    flag = false;
                    break;
                }
            }
            if (flag) //if the row is complete
            {
                RemoveRow(i); //remove the row and move all rows above it down one level
                _game.Score += 10; //increase the score
                //and recursively call this method again. This needs to be done to make sure you check all the rows: 
                //As all rows above this complete row have moved down, you need to check the rows again, or else a complete row might be missed
                CheckFullRows();
            }
        }
    }

    /// <summary>
    /// Removes the row and moves all rows above down.
    /// </summary>
    /// <param name="rowNumber">The row number to remove.</param>
    /// <returns></returns>
    public bool RemoveRow(int rowNumber)
    {
        try
        {
            //for each row form row to be removed to the _board height.....
            for (int i = rowNumber; i <= _boardHeight - 1; i++)
            {
                for (int j = 0; j < _boardWidth; j++)
                {
                    //move cell down
                    _board[j][i] = _board[j][i + 1];
                    //empty old cell
                    _board[j][i + 1] = String.Empty;
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion

    #region Other Game Methods

    /// <summary>
    /// Starts the game by creating a new board and placing a random shape object onto it.
    /// Also finds another random shape that will appear on the board next
    /// </summary>
    private void startGame()
    {
        _board = new string[_boardWidth][]; //instantiate the board array
        for (int i = 0; i < _boardWidth; i++) //instantiate each cell in the array, but make them empty
        {
            _board[i] = new string[_boardHeight];
            for (int j = 0; j < _boardHeight; j++)
            {
                _board[i][j] = string.Empty;
            }
        }

        _activeShape = getRandomShape(_boardWidth / 2, _boardHeight - 1); //create a new random shape and place it at the top/middle of board
        _nextShape = getRandomShape(2, 2); //create another random shape that will appear next on the board, place it in a separate array
    }

    /// <summary>
    /// Gets the next random shape.  Returns a sub class shape object using the base Shape class reference to enable Polymorphism
    /// </summary>
    /// <param name="topMiddleXCord">The top middle X cord.</param>
    /// <param name="topMiddleYCord">The top middle Y cord.</param>
    /// <returns></returns>
    private Shape getRandomShape(int topMiddleXCord, int topMiddleYCord)
    {
        Random random = new Random(); //create a new random object

        switch (random.Next(0, 7)) //get a random number between 1 and 6
        {
            //return a different shape subclass for each random number 
            case 0:
                return new I_Shape(topMiddleXCord, topMiddleYCord);
            case 1:
                return new J_Shape(topMiddleXCord, topMiddleYCord);
            case 2:
                return new L_Shape(topMiddleXCord, topMiddleYCord);
            case 3:
                return new O_Shape(topMiddleXCord, topMiddleYCord);
            case 4:
                return new S_Shape(topMiddleXCord, topMiddleYCord);
            case 5:
                return new T_Shape(topMiddleXCord, topMiddleYCord);
            case 6:
                return new Z_Shape(topMiddleXCord, topMiddleYCord);
        }

        return null; //return null if this fails
    }

    /// <summary>
    /// Turns the board into an array, with a new Shape object at the top
    /// </summary>
    /// <returns></returns>
    public string[][] ToArray()
    {
        return _activeShape.AddShapeToBoard(_board);
    }

    #endregion
}