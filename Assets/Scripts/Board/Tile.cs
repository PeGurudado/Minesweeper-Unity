using System.Collections.Generic;

public class Tile
{
    public int Row { get; private set; }
    public int Column { get; private set; }

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
    }

    public bool ToggleMark()
    {
        if (!IsOpened)
        {
            IsMarked = !IsMarked;
            return IsMarked;
        }
        return false;
    }

    public void Open()
    {
        IsOpened = true;
    }

    public void SetNeighborMineCount(int count)
    {
        NeighborMineCount = count;
    }

    public IEnumerable<Tile> GetNeighborTiles(Tile[,] tiles)
    {
        int rows = tiles.GetLength(0);
        int columns = tiles.GetLength(1);

        for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
        {
            for (int colOffset = -1; colOffset <= 1; colOffset++)
            {
                int neighborRow = Row + rowOffset;
                int neighborCol = Column + colOffset;

                // Skip the current tile
                if (neighborRow == Row && neighborCol == Column)
                    continue;

                if (neighborRow >= 0 && neighborRow < rows && neighborCol >= 0 && neighborCol < columns)
                {
                    yield return tiles[neighborRow, neighborCol];
                }
            }
        }
    }
}