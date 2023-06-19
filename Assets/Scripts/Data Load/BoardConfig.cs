
[System.Serializable]
public class BoardConfig
{
    public int Width;
    public int Height;
    public int TotalMines;
    public int TotalTime;
    public bool RandomGeneration;
    public int[] MinePositions;

    public BoardConfig(int width, int height, int totalMines){
        this.Width = width;
        this.Height = height;
        this.TotalMines = totalMines;
    }
}