using System;

/// <summary>
/// A class representing the player of the game
/// </summary>
public sealed class Player
{
    // Fields
    private String _name; // the player's name

    // Methods
    /// <summary>
    /// Constructor that initializes a new instance of the <see cref="Player"/> class.
    /// </summary>
    public Player()
    {
        //no action is necessary
    }

    // Properties

    /// <summary>
    /// Gets or sets the player name.
    /// </summary>
    /// <value>
    /// The player name.
    /// </value>
    public String Name
    {
        get
        {
            return _name;
        }
        set 
        {
            _name = value;
        }
    }
}