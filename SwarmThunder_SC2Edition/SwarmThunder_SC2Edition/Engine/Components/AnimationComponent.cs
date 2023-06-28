using SFML.Graphics;
using SFML.System;

public struct AnimationArgs
{
    public Vector2i spriteSize;
    public int milliSecondsBetweenAnimation;
}
public class AnimationComponent : Component
{
    private Texture _texture;
    private Vector2i _spriteSize;
    private int _currentFrame;
    private int _milliSecondsBetweenFrames;
    private float _lastAnimationTime;

    private Shape _shape;
    

    public void Setup(GameObject gameObject, AnimationArgs args)
    {
        Awake();
        this.gameObject = gameObject;
        _spriteSize = args.spriteSize;
        _milliSecondsBetweenFrames = args.milliSecondsBetweenAnimation;
        Start();
    }
    
    public override void Awake()
    {
        _currentFrame = 0;
        _lastAnimationTime = 0;
    }

    public override void Start()
    {
        _shape = gameObject.GetShape();
        _texture = _shape.Texture;
         
        _shape.TextureRect = new IntRect(0, 0, _spriteSize.X, _spriteSize.Y);
        _lastAnimationTime = Time.totalMilliSeconds;
    }

    public override void Update()
    {
        if(_texture == null)
            return;
        TrySetNextFrame();
    }

    private void TrySetNextFrame()
    {
        if (CanChangeAnimationFrame())
        {
            _currentFrame++;
            Vector2i nextFrameStartPosition = new(_spriteSize.X * _currentFrame, 0);
            if (nextFrameStartPosition.X >= _texture.Size.X)
                _currentFrame = 0;
            _shape.TextureRect = new IntRect(_spriteSize.X * _currentFrame, 0, _spriteSize.X, _spriteSize.Y);
            _lastAnimationTime = Time.totalMilliSeconds;

        }
    }
    private bool CanChangeAnimationFrame() => Time.totalMilliSeconds >= _lastAnimationTime + _milliSecondsBetweenFrames;
}