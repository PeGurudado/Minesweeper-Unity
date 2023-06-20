using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TileController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mineCountText;
    [SerializeField] private Sprite[] tileSprites;
    [SerializeField] private Image tileImage; 

    private RectTransform rectTransform;
    private BoardController boardController;
    private Tile tile;

    public void Initialize(Tile tile, BoardController boardController)
    {
        this.tile = tile;       
        this.boardController = boardController;      
    }

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
    }

    public Tile GetTile(){
        return tile;
    }

    public void UpdateTileVisuals()
    {        
        if(tile.IsOpened)
        {
            if(tile.IsMine){
                tileImage.sprite = tileSprites[2]; // Opened tile sprite
                mineCountText.text = "";
            }
            else{
                tileImage.sprite = tileSprites[0]; // Opened tile sprite
                mineCountText.text = tile.NeighborMineCount > 0 ? tile.NeighborMineCount.ToString() : "";
            }
        }
        else if (tile.IsMarked)
        {
            tileImage.sprite = tileSprites[1]; // Flag sprite
            mineCountText.text = "";
        }
        else
        {
            tileImage.sprite = tileSprites[3]; // Closed tile sprite
            mineCountText.text = "";
        }
    }

    private void Update() {
        if(GameManager.Instance.IsGameNotRunning()) return;

        if (Input.GetMouseButtonDown(0))
        {
            if(IsMouseOverTile())
                LeftClick();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if(IsMouseOverTile())
                RightClick();
        }
    }

    public void LeftClick()
    {
        if (!tile.IsMarked && !tile.IsOpened)
        {
            tile.Open();
            if (!tile.IsMine && tile.NeighborMineCount == 0)
                OpenNeighbors();
        }
        UpdateTileVisuals();
        GameManager.Instance.CheckGameStatus();
    }

    public void RightClick()
    {
        if (!tile.IsOpened)
        {
            boardController.AddMarkedMines( tile.ToggleMark()? 1 : -1); // Toggle tile mark and update marked mines value at Board Controller
            UpdateTileVisuals();
        }
        GameManager.Instance.CheckGameStatus();
    }

    public void OpenNeighbors()
    {
        if (tile.NeighborMineCount == 0)
        {
            int row = tile.Row;
            int col = tile.Column;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighborRow = row + i;
                    int neighborCol = col + j;

                    if (boardController.IsValidPosition(neighborRow, neighborCol))
                    {
                        TileController neighborController = boardController.GetTileController(neighborRow, neighborCol);

                        if (!neighborController.tile.IsOpened && !neighborController.tile.IsMarked)
                        {
                            neighborController.LeftClick();
                        }
                    }
                }
            }
        }
    }

    public List<TileController> GetNeighborTileControllers()
    {
        List<TileController> neighbors = new List<TileController>();

        int row = tile.Row;
        int col = tile.Column;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int neighborRow = row + i;
                int neighborCol = col + j;

                if (boardController.IsValidPosition(neighborRow, neighborCol) &&
                    (neighborRow != row || neighborCol != col))
                {
                    TileController neighborController = boardController.GetTileController(neighborRow, neighborCol);
                    neighbors.Add(neighborController);
                }
            }
        }

        return neighbors;
    }

    private bool IsMouseOverTile()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 tilePosition = Camera.main.WorldToScreenPoint(transform.position);

        // Convert mouse position to the same scale as tile position
        mousePosition = new Vector3(mousePosition.x, mousePosition.y, tilePosition.z);

        // Get the tile size from the RectTransform, make the tile click size a little smaller to avoid multiple tile clicks
        Vector2 tileSize = rectTransform.sizeDelta * 0.65f; 

        // Calculate the bounds of the tile
        Bounds tileBounds = new Bounds(tilePosition, new Vector3(tileSize.x, tileSize.y, 0f));

        // Check if the mouse position is within the tile bounds
        return tileBounds.Contains(mousePosition);
    }
}