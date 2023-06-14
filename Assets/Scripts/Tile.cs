public class Tile
{
    public int Row {get; private set;}
    public int Column {get; private set; }

    public bool IsMine { get; private set; }
    public bool IsMarked { get; private set; }
    public bool IsOpened { get; private set; }
    public int NeighborMineCount { get; private set; }

    public Tile(int row, int column, bool isMine)
    {
        Row = row;
        Column = column;
        IsMine = isMine;
        IsMarked = false;
        IsOpened = false;
        NeighborMineCount = 0;
    }

    public bool ToggleMark()
    {
        if (!IsOpened)
            IsMarked = !IsMarked;
        return IsMarked;
    }

    public void Open()
    {
        IsOpened = true;
    }

    public void SetNeighborMineCount(int count)
    {
        NeighborMineCount = count;
    }
}