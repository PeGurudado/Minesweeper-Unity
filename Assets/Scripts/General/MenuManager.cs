using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Slider widthSlider;
    [SerializeField] private Slider heightSlider;
    [SerializeField] private Slider minesSlider;
    [SerializeField] private TextMeshProUGUI widthValueText;
    [SerializeField] private TextMeshProUGUI heightValueText;
    [SerializeField] private TextMeshProUGUI minesValueText;
    [SerializeField] private TextMeshProUGUI minesMaxValueText;

    [SerializeField] private int maxMineValue;

    private int maxMineValueCalculated;

    private void Start()
    {
        widthSlider.onValueChanged.AddListener(UpdateWidthValue);
        heightSlider.onValueChanged.AddListener(UpdateHeightValue);
        minesSlider.onValueChanged.AddListener(UpdateMinesValue);

        // Calculate the maximum mine value based on the initial width and height values
        maxMineValueCalculated = CalculateMaxMineValue((int)widthSlider.value, (int)heightSlider.value);
        minesSlider.maxValue = maxMineValueCalculated;
        minesMaxValueText.text = minesSlider.maxValue.ToString();

        // Update the text values initially
        widthValueText.text = widthSlider.value.ToString();
        heightValueText.text = heightSlider.value.ToString();
        minesValueText.text = minesSlider.value.ToString();
    }

    private void UpdateWidthValue(float value)
    {
        int intValue = (int)value;

        // Calculate the maximum mine value based on the new width value
        maxMineValueCalculated = CalculateMaxMineValue(intValue, (int)heightSlider.value);
        minesSlider.maxValue = maxMineValueCalculated;
        minesMaxValueText.text = minesSlider.maxValue.ToString();

        widthValueText.text = intValue.ToString();
    }

    private void UpdateHeightValue(float value)
    {
        int intValue = (int)value;

        // Calculate the maximum mine value based on the new height value
        maxMineValueCalculated = CalculateMaxMineValue((int)widthSlider.value, intValue);
        minesSlider.maxValue = maxMineValueCalculated;
        minesMaxValueText.text = minesSlider.maxValue.ToString();

        heightValueText.text = intValue.ToString();
    }

    private void UpdateMinesValue(float value)
    {
        int intValue = (int)value;
        minesValueText.text = intValue.ToString();
    }

    private int CalculateMaxMineValue(int width, int height)
    {
        int maxMines = width * height;
        return Mathf.Min(maxMines, maxMineValue);
    }

    public void PlayButton(){
        if(ConfigurationLoader.LoadedBoardConfig == null){ // If json exist, it will get these values from him
            ConfigurationLoader.LoadedBoardConfig = new BoardConfig((int) widthSlider.value, (int) heightSlider.value, (int) minesSlider.value);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Loads next scene (Main scene)
    }
}