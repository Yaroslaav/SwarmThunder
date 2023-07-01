using SFML.Graphics;
using SFML.System;
using SFML.Window;

public enum GameMode
{
    PvP,
    PvAI,
    AIvAI,
}

public enum Turn
{
    Your,
    Enemy,
}
public class Game
{
    public static Game instance { get; private set; }

    public Window window;

    public List<GameObject> gameObjects { get; private set; }


    public Player ownPlayer;
    public Player enemyPlayer;

    public Cell selectedCell;

    private Turn _turn;
    public Turn turn
    {
        get => _turn;

        set
        {
            _turn = value;
            
            if(ownPlayer == null || enemyPlayer == null)
                return;
            switch (value)
            {
                case Turn.Enemy:
                    ownPlayer.canShoot = false;
                    enemyPlayer.canShoot = true;
                    break;
                case Turn.Your:
                    ownPlayer.canShoot = true;
                    enemyPlayer.canShoot = false;
                    break;
            }
        }
    }
    
    public void Start()
    {
        if(instance == null)
            instance = this;
        Time.Start();

        AddBindings();
        
        gameObjects = new();
        window = new();

        ownPlayer = new();
        enemyPlayer = new();
        
        turn = Turn.Your;

        SpawnFields();
        
        window.renderWindow.MouseButtonPressed += (sender, args) => CheckMouseClick();

    }

    public void SpawnFields()
    {
        Vector2f fieldSize = new Vector2f(GameSettings.fieldWidth, GameSettings.fieldHeight);
        
        ownPlayer.field = this.CreateActor<Field>(fieldSize, null, new Vector2f(600,650), Color.Magenta, Color.Transparent);
        ownPlayer.field.Setup(FieldType.Own);
        
        enemyPlayer.field = this.CreateActor<Field>(fieldSize, null, new Vector2f(1480,650), Color.Magenta, Color.Transparent);
        enemyPlayer.field.Setup(FieldType.Enemy);

    }

    public void SetSelectedSell(Cell cell)
    {
        selectedCell = cell;
        switch (turn)
        {
            case Turn.Enemy:
                enemyPlayer.SetSelectedSell(cell);
                break;
            case Turn.Your:
                ownPlayer.SetSelectedSell(cell);
                break;
        }   
    }

    public void CheckMouseClick()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i].GetShape().GetGlobalBounds().Contains(Input.mousePosition.X, Input.mousePosition.Y))
            {
                
                gameObjects[i].OnSelect?.Invoke();
                //Console.WriteLine("click" + gameObjects[i]);
                return;
            }
        }
    }

    public void Update()
    {
        switch (turn)
        { 
            case Turn.Enemy:
                Input.GetBind("Shoot").OnKeyPress -= Shoot;
                Shoot();
                break;
            case Turn.Your:
                Input.GetBind("Shoot").OnKeyPress += Shoot; 
                break;
        }
        CheckScore();
    }


    public void Stop(Winner winner)
    {   
        GameLoop.Instance.Stop();
        for (int i = 0; i < gameObjects.Count; i++)
        {
            UnRegisterGameObject(gameObjects[i]);
        }
        
        Text winnerText = new(winner == Winner.You?"You Won":"Enemy Won", new Font(Path.Combine(Directory.GetCurrentDirectory (), "Fonts", "Arial.otf")), 30);
        winnerText.Position = new Vector2f(window.GetWindowCenter().X, window.GetWindowCenter().Y);
        winnerText.FillColor = winner == Winner.You?Color.Green:Color.Red;
    }
    
    public void UnRegisterGameObject(GameObject gameObject)
    {
        GameLoop.Instance.UnRegisterGameObject(gameObject);
        gameObjects.Remove(gameObject);
    }

    private void AddBindings()
    {
        BindKey shootKey = Input.AddNewBind(Keyboard.Key.Space, "Shoot");

        shootKey.OnKeyPress += Shoot;
    }


    public void Shoot()
    {
        switch (turn)
        {
            case Turn.Enemy:
                enemyPlayer.SetSelectedSell(ownPlayer.field.GetRandomHittableCell());
                enemyPlayer.Shoot(ownPlayer);
                break;
            case Turn.Your:
                ownPlayer.Shoot(enemyPlayer);
                break;
        }
        
    }

    public void OnShoot()
    {
        ChangeTurn();
    }

    private void ChangeTurn()
    {
        if (turn == Turn.Enemy)
            turn = Turn.Your;
        else
            turn = Turn.Enemy;
    }

    private void CheckScore()
    {
        if(ownPlayer.score >= GameSettings.maxShipsAmount)
            Stop(Winner.You);
        else if(enemyPlayer.score >= GameSettings.maxShipsAmount)
            Stop(Winner.Enemy);
    }

}