using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class BoardController : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int totalMines;
    [SerializeField] private GridLayoutGroup tileParentGrid;
    [SerializeField] private TileController tilePrefab;

    [SerializeField] private TextMeshProUGUI mineCounterText;

    private int markedMines;

    private Board board;
    private TileController[,] tileControllers;

    private void Start()
    {
        board = new Board(width, height, totalMines);
        board.GenerateRandomBoard();

        tileControllers = new TileController[height, width];

        SetGridLayoutSize();
        InstantiateTiles();
        UpdateMineCounter();
    }

    public void AddMarkedMines(int value){
        markedMines += value;
        UpdateMineCounter();
    }

    void SetGridLayoutSize(){
        tileParentGrid.constraint = GridLayoutGroup.Constraint.FixedRowCount; // Sets contraint based on Row
        tileParentGrid.constraintCount = height; // Sets same value from board height to grid layout 
    }

    private void InstantiateTiles()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                Vector3 position = new Vector3(col , -row, 0);
                TileController newTile = Instantiate(tilePrefab, position, Quaternion.identity, tileParentGrid.transform);
                tileControllers[row,col] = newTile;

                Tile tile = board.GetTile(row, col);
                tile.SetNeighborMineCount(board.GetNeighborMineCount(row, col)); // Set neighbor mine count

                newTile.Initialize(tile);
            }
        }
    }

    private void UpdateMineCounter()
    {
        int remainingMines = totalMines - markedMines;
        mineCounterText.text = "Remaining mines: "+ remainingMines.ToString();
    }

    public bool IsValidPosition(int row, int col)
    {
        return row >= 0 && row < height && col >= 0 && col < width;
    }

    public TileController GetTileController(int row, int col)
    {
        return tileControllers[row,col]; 
    }
}