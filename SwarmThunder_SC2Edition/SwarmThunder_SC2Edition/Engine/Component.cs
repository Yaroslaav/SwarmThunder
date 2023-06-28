public abstract class Component
{
    protected GameObject gameObject;
    public Action OnDestroy;
    public virtual void Awake()
    {
        
    }

    public virtual void Start()
    {
        
    }
    public virtual void Update()
    {
        
    }

    public virtual void Destroy()
    {
       this.DestroyReference(); 
    }
    public GameObject GetGameObject() => gameObject;
}   