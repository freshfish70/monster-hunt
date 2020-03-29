﻿/// <summary>
/// The constants for the different level states
/// </summary>
public enum LEVEL_STATE {

    /// <summary>
    /// Exit the current level
    /// </summary>
    EXIT,

    /// <summary>
    /// Start the play mode in game
    /// </summary>
    PLAY,

    /// <summary>
    /// Game pause
    /// </summary>
    PAUSE,

    /// <summary>
    /// Go to game over screen
    /// </summary>
    GAME_OVER,

    /// <summary>
    /// Start the hunting mode in game
    /// </summary>
    HUNTING,

    /// <summary>
    /// Reloads the current scene
    /// </summary>
    RELOAD,

    GAME_WON,

    /// <summary>
    /// State to be ran at start of level
    /// </summary>
    START
}