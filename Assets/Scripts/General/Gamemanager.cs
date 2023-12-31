using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameoverScreen;
    [SerializeField] private GameObject congratulationsScreen;

    [SerializeField] private BoardController boardController;
    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; private set; }
    public bool IsGameWon { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void CheckGameStatus()
    {             
        if(boardController.HasOpenedAnyMine())// Game over condition
            GameOver();
        else if (boardController.AllTilesOpenedWithoutMines() && boardController.HasMarkedAllMines())// Game win condition                
            GameWin();           
    }

    public bool IsGameNotRunning(){
        return IsGameOver || IsGameWon;
    }

    public void GameOver()
    {
        IsGameOver = true;
        boardController.OpenAllBoardMines();
        gameoverScreen.SetActive(true);
    }

    private void GameWin()
    {
        IsGameWon = true;
        congratulationsScreen.gameObject.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
