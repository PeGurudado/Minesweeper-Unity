using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class BoardController : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup tileParentGrid;
    [SerializeField] private TileController tilePrefab;

    [SerializeField] private TextMeshProUGUI mineCounterText;

    private int Width;
    private int Height;
    private int totalMines;
    private int markedMines;

    private Board board;
    private TileController[,] tileControllers;


    private void Awake() {
        LoadBoardConfiguration();        
    }

    private void Start()
    {        
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
        tileParentGrid.constraintCount = Height; // Sets same value from board height to grid layout 
    }

    private void InstantiateTiles()
    {
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                TileController newTile = Instantiate(tilePrefab, tileParentGrid.transform);
                tileControllers[row,col] = newTile;

                Tile tile = board.GetTile(row, col);

                newTile.Initialize(tile, this);
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
        return row >= 0 && row < Height && col >= 0 && col < Width;
    }

    public TileController GetTileController(int row, int col)
    {
        return tileControllers[row,col]; 
    }

    private void LoadBoardConfiguration()
    {
        // Use the board configuration 
        Width = ConfigurationLoader.LoadedBoardConfig.Width;
        Height = ConfigurationLoader.LoadedBoardConfig.Height;        
        totalMines = ConfigurationLoader.LoadedBoardConfig.TotalMines;

        tileControllers = new TileController[Height, Width];

        // Generate or load the board based on the configuration
        if (ConfigurationLoader.LoadedBoardConfig.RandomGeneration || ConfigurationLoader.LoadedBoardConfig.MinePositions == null)
        {
            board = new Board(Width, Height, totalMines);
        }
        else
        {
            // Use the mine positions from the board configuration
            board = new Board(Width, Height, ConfigurationLoader.LoadedBoardConfig.MinePositions);
            totalMines = board.GetTotalMines();
        }
    }

    public Tile[,] GetTileControllers()
    {
        Tile[,] tiles = new Tile[Height, Width];

        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                tiles[row, col] = tileControllers[row, col].GetTile();
            }
        }

        return tiles;
    }

    public List<TileController> GetAllTileControllers()
    {
        List<TileController> allTileControllers = new List<TileController>();

        foreach (var tileController in tileControllers)
        {
            allTileControllers.Add(tileController);
        }

        return allTileControllers;
    }

    public bool AllTilesOpenedWithoutMines()
    {
        foreach (var tileController in tileControllers)
        {
            Tile tile = tileController.GetTile();
            
            // Check if any tile without a mine is not opened
            if (!tile.IsOpened && !tile.IsMine)
            {
                return false;
            }
        }

        return true;
    }

    public bool HasOpenedAnyMine(){
        bool hasOpenedMine = false;

        // Check if any tile with a mine is opened
        foreach (var tileController in GetTileControllers())
        {
            if (tileController.IsOpened && tileController.IsMine)
            {
                hasOpenedMine = true;
                break;
            }
        }

        if (hasOpenedMine)
        {            
            return true;
        }

        return false;
    }

    public void OpenAllBoardMines(){
        foreach (var tileController in tileControllers)
        {
            Tile tile = tileController.GetTile();

            if (tile.IsMine)
            {
                tile.Open();
                tileController.UpdateTileVisuals();
            }
        }
    }
}