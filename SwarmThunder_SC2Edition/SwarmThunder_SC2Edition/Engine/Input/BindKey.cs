using SFML.Window;

public class BindKey
{
    public Action OnKeyPress;
    public Action OnKeyDown;
    public Action OnKeyUp;

    private Keyboard.Key key;
    
    private bool _wasPressed;
    private bool _isPressed;

    public BindKey(Keyboard.Key key)
    {
        this.key = key;
    }
    
    public void CheckInput()
    {
        CheckKeyboardInput();
    }

    private void CheckKeyboardInput()
    {
        _isPressed = Keyboard.IsKeyPressed(key);
        if (_wasPressed && !_isPressed)
        {
            OnKeyUp?.Invoke();
        }
        if (_isPressed)
        {
            OnKeyDown?.Invoke();
        }
        if (!_wasPressed)
        {
            if(_isPressed)
                OnKeyPress?.Invoke();
        }
        _wasPressed = _isPressed;
    }
}