using SFML.System;

public static class MathExtensions
{

    public static Vector2f Normalize(this Vector2f direction)
    {
        return direction / MathF.Sqrt((direction.X * direction.X) + (direction.Y * direction.Y));
    }
    
}