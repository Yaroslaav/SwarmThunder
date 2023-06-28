using SFML.Graphics;
using SFML.System;

public struct GameObjArgs
{
    public Vector2f size;
    public Vector2f Position;
    public Texture texture;
    public Color fillColor;
    public Color borderColor;
}
public class GameObject : Transformable, IDrawable, IUpdatable
{
    public int ZPosition { get; set; } = 0;

    protected Texture texture { get; set; }

    private List<Component> _components = new ();

    public Action OnSelect;
    public Action OnDeSelect;


    public Shape GetShape() => (GetOriginalShape());
    public virtual Vector2f size { get; set; }

    protected virtual Shape GetOriginalShape()
    {
        return null;
    }

    public virtual void Awake(GameObjArgs args)
    {
        
    }
    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        UpdateComponents();
    }

    public T AddComponent<T>() where T : Component
    {
        if (GetComponent<T>() != null)
        {
            return GetComponent<T>();
        }

        T component = Activator.CreateInstance<T>();
        _components.Add(component);
        
        return component;
    }


    private void UpdateComponents()
    {
        foreach (var component in _components)
        {
            component.Update();
        }
    }

    public T GetComponent<T>() where T : Component
    {
        T componentInstance = Activator.CreateInstance<T>();

        foreach (var component in _components)
        {
            if (componentInstance.GetType() == component.GetType())
                return component as T;
        }

        return null;
    }
}