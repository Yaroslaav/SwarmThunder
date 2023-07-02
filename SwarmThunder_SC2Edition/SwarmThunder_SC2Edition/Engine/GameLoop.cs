public class GameLoop
{
    public static GameLoop Instance { get; private set; }

    private Game _game;
    
    
    private bool isPlaying;
    
    private List<IDrawable> drawableObjects = new();
    private List<IUpdatable> updatableObjects = new();
    
    public Rounds rounds { get; private set; }

    public void Start()
    {
        if (Instance == null)
            Instance = this;
        new GameSettings();
        rounds = new Rounds();
        
        _game = new ();
        _game.Start();
        
        _game.window.renderWindow.Closed += (_, _) => isPlaying = false;

        isPlaying = true;
        rounds.StartRounds();
        Loop();
    }
    private void Loop()
    {
        while (isPlaying)
        {
            Time.UpdateTimer();
            _game.window.DispatchEvents();
            
            Input.CheckInput();
            
            // "Game.cs" fields update
            
            
            Update();
            _game.Update();
               
            ReDrawObjects();
        }
        //rounds.TryStartNextRound();
    }

    public void Update()
    {
        for (int i = 0; i < updatableObjects.Count; i++)
        {
            updatableObjects[i].Update();
        }
    }

    public void ReDrawObjects()
    {
        _game.window.Draw(drawableObjects);
    }
    
    

    public void Stop()
    {
        isPlaying = false;
        /*while (true)
        {
            
        }*/

    }
    
    private void StartNextRound()
    {
        rounds.TryStartNextRound();
    }
    

    
    public void RegisterGameObject(GameObject gameObject)
    {
        if (gameObject is IDrawable)
        {
            if (!drawableObjects.Contains(gameObject as IDrawable))
            {
                drawableObjects.Add(gameObject as IDrawable);
            }
        }
        if (gameObject is IUpdatable)
        {
            if (!updatableObjects.Contains(gameObject as IUpdatable))
            {
                updatableObjects.Add(gameObject as IUpdatable);
            }
        }
    }

    public void UnRegisterGameObject(GameObject gameObject)
    {
        if (updatableObjects.Contains(gameObject as IUpdatable))
        {
            updatableObjects.Remove(gameObject as IUpdatable);
        }
        if (drawableObjects.Contains(gameObject as IDrawable))
        {
            drawableObjects.Remove(gameObject as IDrawable);
        }
    }
}