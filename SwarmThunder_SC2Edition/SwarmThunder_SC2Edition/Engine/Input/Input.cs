using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Input
{
    
    public static Vector2f lastPlayerDirection = new (0,0);
    
    private static Dictionary<string, BindKey> keys = new(0);

    public static Vector2f mousePosition
    {
        get
        {
            if(window == null)
                return new Vector2f();
            
            return window.MapPixelToCoords(window.GetMousePosition());
        } 
    }

    private static Window window
    {
        get => Game.instance?.window;
    }

    public static void CheckInput()
    {
        CheckMouseInput();
        CheckKeys();
    }
    private static void CheckMouseInput()
    {
        if(window == null)
            return;
        Vector2f targetPosition = window.MapPixelToCoords(window.GetMousePosition());
        Vector2f direction = targetPosition;

        lastPlayerDirection = direction;
    }
    
    private static void CheckKeys()
    {
        foreach (BindKey key in keys.Values)
        {
            key.CheckInput();
        }
    }

    public static BindKey AddNewBind(Keyboard.Key key, string name)
    {
        BindKey bindKey = new BindKey(key);
        keys.Add(name, bindKey);
        return bindKey;
    }

    public static BindKey GetBind(string name) => keys[name];

}