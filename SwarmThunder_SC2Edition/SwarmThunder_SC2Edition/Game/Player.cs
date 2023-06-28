
public class Player
{
    public int score;
    public Field field;

    public bool canShoot;

    public Cell selectedCell;

    public Player()
    {
        score = 0;
    }

    public void SetSelectedSell(Cell cell)
    {
        selectedCell = cell;
    }

    public void Shoot(Player enemy)
    {
        if(selectedCell == null || !canShoot)
            return;
        if(field.cells.Contains(selectedCell))
            return;
        if(!enemy.field.cells.Contains(selectedCell))
            return;

        switch (selectedCell.type)
        {
            case CellType.Ship:
                score++;
                break;
            case CellType.Water:
                break;
            default:
                return;
        }

        enemy.field.OnHit(selectedCell);
        
        Game.instance.OnShoot();

        selectedCell = null;
    }
}