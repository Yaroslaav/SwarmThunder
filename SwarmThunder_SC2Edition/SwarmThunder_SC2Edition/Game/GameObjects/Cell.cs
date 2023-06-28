using SFML.Graphics;
using SFML.System;

public enum CellType
{
    None,
    Water,
    Ship,
    Hit,
    HitInShip,
}
public class Cell : GameObject
{
    private CellType _type;
    private Field parentField;

    public CellType type
    {
        get => _type;
        set
        {
            _type = value;
            if(shape == null)
                return;
            switch (value)
            {
                case CellType.Water:
                    
                    shape.Texture = null;
                    shape.FillColor = Color.Black;
                    break;
                case CellType.Ship:
                    if (parentField.type == FieldType.Enemy)
                    {
                        shape.Texture = null;
                        shape.FillColor = Color.Green;
                    }
                    else
                    {
                        shape.FillColor = Color.Green;
                        
                    }
                    break;
                case CellType.Hit:
                    shape.Texture = new Texture("GreenPoint.png");
                    shape.FillColor = Color.Green;
                    break;
                case CellType.HitInShip:
                    shape.Texture = new Texture("RedCrossHair.png");
                    shape.FillColor = Color.Red;
                    break;
            }
        }
    }

    public RectangleShape shape;
    
    public Cell()
    {
        type = CellType.Water;
    }
    public override void Awake(GameObjArgs args)
    {

        OnSelect += Select;
        
        OnDeSelect += () => shape.OutlineColor = args.borderColor;
        OnDeSelect += DeSelect;
        
        shape = new();
        shape.Size = args.size;
        shape.Origin = new Vector2f(shape.Size.X / 2, shape.Size.Y / 2);
        shape.OutlineThickness = 2;

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
        
    }

    public void Setup(Field _field)
    {
        parentField = _field;
    }
    private void Select()
    {
        shape.OutlineColor = Color.Red;

        if (Game.instance.selectedCell != null && Game.instance.selectedCell != this)
        {
            Game.instance.selectedCell.OnDeSelect?.Invoke();
        }
        
        
        Game.instance.SetSelectedSell(this);
    }

    private void DeSelect()
    {
        
    }

    protected override Shape GetOriginalShape()
    {
        return shape;
    }

    public void SetType(CellType type) => this.type = type;
}