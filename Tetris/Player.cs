using System;

/// <summary>
/// The Player of the game
/// </summary>
public sealed class Player
{
    // Fields
    private String _name;

    // Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class.
    /// </summary>
    public Player()
    {
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