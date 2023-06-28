using SFML.Graphics;
using SFML.System;

public class Camera
{
    public View camera;
    public float ZoomIncrement = .2f;
    public float maxZoom = 1.03f;
    
    private Window window
    {
        get => Game.instance.window;
    }

    public Camera()
    {
        SetupCamera();
    }
    
    public void SetupCamera()
    {
        camera = new (new FloatRect(window.GetWindowCenter().X, window.GetWindowCenter().Y, GameSettings.windowWidth, GameSettings.windowHeight));
        camera.Zoom(0.5f);
    }

    public void Zoom(float zoom)
    {
        if(zoom<maxZoom)
            camera.Zoom(zoom);
    }

    public void MoveCamera(Vector2f newPosition)
    {
        camera.Center = newPosition;
    }

}