using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AutoplayController : MonoBehaviour
{
    [SerializeField] private Toggle autoplayToggle;
    [SerializeField] private BoardController boardController;

    [SerializeField] private float autoplayDelay = 0.5f;

    private bool isAutoplayEnabled;
    private bool isPlaying;

    private void Start()
    {
        // Add an event listener to the toggle button
        autoplayToggle.onValueChanged.AddListener(OnAutoplayToggle);
    }

    private void OnAutoplayToggle(bool value)
    {
        isAutoplayEnabled = value;
        if (isAutoplayEnabled)
        {
            StartCoroutine(PlayGame());
        }
        else
            StopAllCoroutines();
    }

    private IEnumerator PlayGame()
    {
        isPlaying = true;

        // Perform autoplay logic here
        int tempCounter = 0;
        while (isPlaying && !GameManager.Instance.IsGameNotRunning())
        {
            bool moveMade = MakeAutoplayMove();
            tempCounter++;
            // If no valid move is available, stop autoplay
            if (!moveMade)
            {
                isPlaying = false;
                autoplayToggle.isOn = false;
            }
            yield return new WaitForSeconds(autoplayDelay);
        }
    }

    private bool MakeAutoplayMove()
    {
        // Get all the closed and unmarked tiles
        List<TileController> closedTiles = new List<TileController>();
        foreach (var tileController in boardController.GetAllTileControllers())
        {
            if (!tileController.GetTile().IsOpened && !tileController.GetTile().IsMarked)
            {
                float bombChance = CalculateBombChance(tileController);
                if(bombChance == 1)// It's for sure a bomb 
                {
                    tileController.RightClick(); // Mark as a bomb
                    return true;
                }
                else if(bombChance == 0)// It's for sure not a bomb
                {
                    tileController.LeftClick(); // Open tile
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Calculate the Bomb Chance in that specific tile.
    /// Works by checking the nBombs on tile number minus already known bombs with a mark, divided to the remaining valid spaces, note this function should only be called on a closed tile 
    /// </summary>
    private float CalculateBombChance(TileController tile)
    {
        float closestTo01Chance = 0.5f;
        foreach (TileController tileNeighbor in tile.GetNeighborTileControllers())
        {
            if (tileNeighbor.GetTile().IsOpened)
            {
                float nBombs = tileNeighbor.GetTile().NeighborMineCount;
                float closeMarks = 0, validSpaces = 0;

                List<TileController> neighborList = tileNeighbor.GetNeighborTileControllers();
                foreach (var item in neighborList)
                {
                    if (item.GetTile().IsMarked)
                        closeMarks++;
                    else if (!item.GetTile().IsOpened)
                        validSpaces++;
                }

                float currentChance = (nBombs - closeMarks) / validSpaces;
                if (currentChance == 1 || currentChance == 0)
                    return currentChance;
            }
        }

        return closestTo01Chance;
    }
}