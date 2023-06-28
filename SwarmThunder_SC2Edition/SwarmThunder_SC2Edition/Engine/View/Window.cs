using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Window
{
    public RenderWindow renderWindow;
    /*private static View _camera
    {
        get => Game.instance.mainCamera.camera;
    }*/

    public Window()
    {
        SetWindow();
    }
    
    public void Draw(List<IDrawable> drawableObjects)
    {
        DispatchEvents();
        Clear();
        //renderWindow.SetView(_camera);
        
        for (int i = 0; i < drawableObjects.Count; i++)
        {
            renderWindow.Draw(drawableObjects[i].GetShape());
        }
        renderWindow.Display();
    }
    public void SetWindow()
    {
        renderWindow = new RenderWindow(new VideoMode(GameSettings.windowWidth, GameSettings.windowHeight), GameSettings.windowTitle);
        renderWindow.SetFramerateLimit(600);
        Clear();
    }

    public void DispatchEvents() => renderWindow.DispatchEvents();
    public void Clear() => renderWindow.Clear(Color.White);
    public void Close()
    {
        Clear();
        renderWindow.Close();
    }
    public Vector2f MapPixelToCoords(Vector2i position) => renderWindow.MapPixelToCoords(position);


    public Vector2i GetMousePosition() => Mouse.GetPosition(renderWindow);
    

}