using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float totalTime = 600f; // Total time in seconds
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        LoadTimerValue();        
    }

    void LoadTimerValue(){
        if(ConfigurationLoader.LoadedBoardConfig != null && ConfigurationLoader.LoadedBoardConfig.TotalTime > 0)
            totalTime = ConfigurationLoader.LoadedBoardConfig.TotalTime; // Tries to load timer value from Json data

        currentTime = totalTime;
    }

    private void Update()
    {
        if(GameManager.Instance.IsGameNotRunning()) return;

        if (currentTime > 1f)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = "Time: "+string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}