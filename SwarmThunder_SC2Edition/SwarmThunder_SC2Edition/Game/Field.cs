using SFML.Graphics;
using SFML.System;

public enum FieldType
{
    Own, 
    Enemy,
}
public class Field : GameObject
{
    public Cell[,] cells { get; set; }

    public FieldType type;

    private Vector2f firstCellPosition;

    public RectangleShape shape;
    
    public override void Awake(GameObjArgs args)
    {
        shape = new();
        shape.Size = args.size;
        shape.Origin = new Vector2f(shape.Size.X / 2, shape.Size.Y / 2);

        texture = args.texture;

        shape.Position = args.Position;
        Position = args.Position;
        
        if (args.texture != null)
        {
            shape.Texture = args.texture;
        }
        else
        {
            shape.FillColor = args.fillColor;
            shape.OutlineColor = args.borderColor;
        }
        firstCellPosition = new((int)shape.Position.X - GameSettings.fieldWidth + GameSettings.cellSize.X, (int)shape.Position.Y - GameSettings.fieldHeight + GameSettings.cellSize.Y);
    }

    protected override Shape GetOriginalShape()
    {
        return shape;
    }

    public void Setup(FieldType type)
    {
        this.type = type;
        GenerateField();
    }

    private void GenerateField()
    {
        Console.WriteLine(GameSettings.cellSize);
        cells = new Cell[GameSettings.cellsAmountbyY, GameSettings.cellsAmountbyX];
        
        for (int y = 0; y < GameSettings.cellsAmountbyY; y++)
        {
            for (int x = 0; x < GameSettings.cellsAmountbyX; x++)
            {
                int xCellPos = (int)GameSettings.cellSize.X * (x+1) + (int)firstCellPosition.X;
                int yCellPos = (int)GameSettings.cellSize.Y * (y+1) + (int)firstCellPosition.Y;
                Vector2f cellPosition = new Vector2f(xCellPos, yCellPos);

                cells[y,x] = Game.instance.CreateActor<Cell>(GameSettings.cellSize, null, cellPosition, Color.Black, Color.Cyan);
                cells[y,x].Setup(this);
            }
        }

            SpawnShips();
        shape.Size = new();
    }
    private void SpawnShips()
    {
        for (int i = 0; i < GameSettings.maxShipsAmount; i++)
        {
            Cell freeCell = GetRandomFreeCell();
            freeCell.shape.FillColor = Color.Green;
            freeCell.SetType(CellType.Ship);
        }
    }
    public CellType OnHit(Cell cell)
    {
        if(!cells.Contains(cell))
            return CellType.None;

        switch (cell.type)
        {
            case CellType.Water:
                cell.SetType(CellType.Hit);
                break;
            case CellType.Ship:
                cell.SetType(CellType.HitInShip);
                break;
        }

        return cell.type;
    }
    public Cell GetRandomFreeCell()
    {
        int x = Rand.Next(0, 10);
        int y = Rand.Next(0, 10);
        while (cells[y, x].type != CellType.Water)
        {
            x = Rand.Next(0, 10);
            y = Rand.Next(0, 10);
        }
        return cells[y,x];
    }

    public Cell GetRandomHittableCell()
    {
        int x = Rand.Next(0, 10);
        int y = Rand.Next(0, 10);
        while (cells[y, x].type != CellType.Water && cells[y, x].type != CellType.Ship)
        {
            Console.WriteLine($"cell: {cells[y,x].type}");
            x = Rand.Next(0, 10);
            y = Rand.Next(0, 10);
        }
        return cells[y,x];
    }
}