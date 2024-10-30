using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public enum SpeedSettings { Slow, Medium, Fast, Instant }
public class GridGenerator : MonoBehaviour
{
    //TODO: Check if I can store the grid information in a scriptable object


    [SerializeField] private Cell mazeCell;
    [SerializeField] private TMP_InputField widthInput, heightInput;
    [SerializeField] private UnityEvent OnInvalidParameters;
    [SerializeField] private UnityEvent OnGenerateWhileGenerating;

    private Cell[,] _cells;
    private Cell _initialCell;
    private int _widthSize, _heightSize;
    private bool _isGridGenerating;

    private const int MaxCells = 250;
    private const int MinCells = 10;
    public int HeightSize => _heightSize;
    public int WidthSize => _widthSize;
    public Cell[,] Cells => _cells;
    public Cell InitialCell => _initialCell;


    public void Start()
    {
        // This is a poor way of validating input text. If you expect numbers within a field - you should only allow numbers:
        // https://discussions.unity.com/t/allow-only-numbers-to-a-textfield-and-make-it-an-int/126733
        // Which TMP convienently does offer that, right under the actual text in the inspector, Input Field Settings > Content Type > Integer Number
        //int.TryParse(widthInput.text, out _widthSize);
        //int.TryParse(heightInput.text, out _heightSize);
        _cells = new Cell[_widthSize = int.Parse(widthInput.text), _heightSize = int.Parse(heightInput.text)];
    }

    public void StartGeneratingGrid()
    {
        if (_isGridGenerating)
        {
            OnGenerateWhileGenerating.Invoke();
            return;
        }
        StartCoroutine(GenerateGrid());
    }

    private IEnumerator GenerateGrid()
    {
        if (!IsGridCleared()) yield break;

        _isGridGenerating = true;

        for (int x = 0; x < _widthSize; x++)
        {
            for (int y = 0; y < _heightSize; y++)
            {
                Cell newCell = Instantiate(mazeCell, gameObject.transform);
                newCell.transform.position = new(x, y - _heightSize / 2f + .5f);
                newCell.ArrayPosition = new(x, y);
                Cells[x, y] = newCell;
            }
        }

        var randomX = Random.Range(0, _widthSize - 1);
        var randomY = Random.Range(0, _heightSize - 1);
        _initialCell = Cells[randomX, randomY];
        _isGridGenerating = false;
    }

    private bool IsGridCleared()
    {
        if (Cells[0, 0] != null)
        {
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    Destroy(Cells[x, y].gameObject);
                }
            }
        }

        int.TryParse(widthInput.text, out _widthSize);
        int.TryParse(heightInput.text, out _heightSize);

        if (_widthSize >= MinCells && _widthSize <= MaxCells &&
            _heightSize >= MinCells && _heightSize <= MaxCells)
        {
            _cells = new Cell[_widthSize, _heightSize];
            return true;
        }

        OnInvalidParameters.Invoke();
        return false;
    }
}