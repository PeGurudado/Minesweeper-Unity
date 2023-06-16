using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float totalTime = 120f; // Total time in seconds
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        LoadTimerValue();        
    }

    void LoadTimerValue(){
        if(ConfigurationLoader.LoadedBoardConfig != null && ConfigurationLoader.LoadedBoardConfig.totalTime > 0)
            totalTime = ConfigurationLoader.LoadedBoardConfig.totalTime; // Tries to load timer value from Json data

        currentTime = totalTime;
    }

    private void Update()
    {
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            RestartScene();
        }

        if(Input.GetKeyDown(KeyCode.R))
            RestartScene();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = "Time: "+string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}