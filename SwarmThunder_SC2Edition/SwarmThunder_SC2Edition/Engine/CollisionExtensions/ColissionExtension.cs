using SFML.Graphics;

public static class ColissionExtension
{
    public static bool CheckCollision(this Shape circle1, Shape circle2)
    {
        FloatRect rect1 = circle1.GetGlobalBounds();
        FloatRect rect2 = circle2.GetGlobalBounds();

        return rect1.Intersects(rect2);
    }
}