using SFML.System;
using SFML.Graphics;
public static class GameObjectFabrik
{
    public static T CreateActor<T>(this Game game, Vector2f size, Texture texture, Vector2f position, Color fillColor, Color borderColor) where T : GameObject,new()
    {
        T t = CreateActor_Internal<T>(size, texture, position, fillColor, borderColor);
        if (t == null)
            return null;
        game.gameObjects.Add(t);
        GameLoop.Instance.RegisterGameObject(t);
        return t;
    }
    private static T CreateActor_Internal<T>(Vector2f size, Texture texture, Vector2f position, Color fillColor, Color borderColor) where T : GameObject, new()
    {
        GameObjArgs args;
        args.size = size;
        args.Position = position;
        args.texture = texture;
        args.borderColor = borderColor;
        args.fillColor = fillColor;
        T t = new T();
        t.Awake(args);
        return t;
    }
}