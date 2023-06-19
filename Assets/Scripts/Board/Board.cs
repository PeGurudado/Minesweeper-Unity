using UnityEngine;

public class Board
{
    private Tile[,] tiles;
    private int width;
    private int height;
    private int totalMines;

    public Board(int width, int height, int[] minePositions)
    {
        this.width = width;
        this.height = height;

        tiles = new Tile[height, width];

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                bool isMine = minePositions[row * width + col] == 1;
                if(isMine) this.totalMines++;
                tiles[row, col] = new Tile(row, col, isMine);
            }
        }

        CalculateNeighborMineCounts();
    }

    public Board(int width, int height, int totalMines)
    {
        this.width = width;
        this.height = height;
        this.totalMines = totalMines;

        tiles = new Tile[height, width];

        GenerateRandomBoard();
    }

    public void GenerateRandomBoard()
    {
        int[,] minePositions = GetMinePositions();

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                bool isMine = minePositions[row, col] == 1;
                tiles[row, col] = new Tile(row, col, isMine);
            }
        }

        CalculateNeighborMineCounts();
    }

    public Tile GetTile(int row, int col)
    {
        return tiles[row, col];
    }

    public int GetTotalMines(){
        return totalMines;
    }

    public int GetNeighborMineCount(int row, int col)
    {
        int count = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int neighborRow = row + i;
                int neighborCol = col + j;

                if (IsValidPosition(neighborRow, neighborCol) && tiles[neighborRow, neighborCol].IsMine)
                {
                    count++;
                }
            }
        }

        return count;
    }

    private int[,] GetMinePositions()
    {
        int[,] minePositions = new int[height, width];
        int minesRemaining = totalMines;

        while (minesRemaining > 0)
        {
            int randomRow = Random.Range(0, height);
            int randomCol = Random.Range(0, width);

            if (minePositions[randomRow, randomCol] == 0)
            {
                minePositions[randomRow, randomCol] = 1;
                minesRemaining--;
            }
        }

        return minePositions;
    }

    private bool IsValidPosition(int row, int col)
    {
        return row >= 0 && row < height && col >= 0 && col < width;
    }

    private void CalculateNeighborMineCounts()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (!tiles[row, col].IsMine)
                {
                    int count = GetNeighborMineCount(row, col);
                    tiles[row, col].SetNeighborMineCount(count);
                }
            }
        }
    }
}