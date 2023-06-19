using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResizeGridLayout : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    [SerializeField] private int minSize = 15;
    [SerializeField] private int maxSize = 200;

    int currentSize;

    private void Awake() {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();        
    }

    private void Start()
    {
        StartCoroutine(ResizeCellSize());
    }

    IEnumerator ResizeCellSize()
    {
        gridLayoutGroup.cellSize = new Vector2(maxSize, maxSize);
        yield return new WaitForFixedUpdate();

        while(!rectTransform.IsTotalVisible(Camera.main))
        {
            gridLayoutGroup.cellSize *= 0.95f; //Reduces 5%

            if(gridLayoutGroup.cellSize.x <= minSize)
            {
                gridLayoutGroup.cellSize = new Vector2(minSize, minSize);
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}