﻿using System;

/// <summary>
/// The Player of the game
/// </summary>
public sealed class Player
{
    // Fields
    private String _name;

    // Methods
    public Player()
    {
    }

    // Properties
    public String Name
    {
        get
        {
            return this._name;
        }
        set 
        {
            this._name = value;
        }
    }
}