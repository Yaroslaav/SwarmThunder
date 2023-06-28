
public static class ContainsCellExtension
{
    public static bool Contains(this Cell[,] cells, Cell cell)
    {
        foreach (var _cell in cells)
        {
            if (_cell == cell)
                return true;
        }

        return false;
    }
}