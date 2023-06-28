
using SFML.System;

public static class WindowExtensions
{
    public static Vector2u GetWindowCenter(this Window window) => new (GameSettings.windowWidth / 2, GameSettings.windowHeight / 2);

    
}