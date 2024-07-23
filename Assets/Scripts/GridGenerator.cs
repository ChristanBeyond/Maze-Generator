using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public enum SpeedSettings { Slow, Medium, Fast, Instant}
public class GridGenerator : MonoBehaviour
{
    //TODO: Check if I can store the grid information in a scriptable object

    
    [SerializeField] private Cell mazeCell;
    [SerializeField] private TMP_InputField widthInput, heightInput;
    [SerializeField] private UnityEvent OnInvalidParameters;
    public Cell[,] _cells;
    public Cell _initialCell;

    private const int MaxCells = 250;
    private const int MinCells = 10;
    
    private SpeedSettings _speedSetting;
    private float _generationSpeed;
    private int _widthSize, _heightSize;
    public int HeightSize => _heightSize;
    public int WidthSize => _widthSize;
    

    public void SetGenerationSpeed(int setting)
    {
        _speedSetting = (SpeedSettings)setting;
    }

    public void Start()
    {
        int.TryParse(widthInput.text, out _widthSize);
        int.TryParse(heightInput.text, out _heightSize);
        _cells = new Cell[_widthSize, _heightSize];
    }

    public void OnGenerateGrid()
    {
        StartCoroutine(GenerateGrid());
    }

    //temporary solution
    private IEnumerator GenerateGrid()
    {
        if(!ResetMaze()) yield break;
        for (int x = 0; x < _widthSize; x++)
        {
            for (int y = 0; y < _heightSize; y++)
            {
                Cell newCell = Instantiate(mazeCell, gameObject.transform);
                newCell.transform.position = new(x, y);
                _cells[x, y] = newCell;

                yield return new WaitForSeconds(0.0001f);
            }
        }

        var randomX = Random.Range(0, _widthSize - 1);
        var randomY = Random.Range(0, _heightSize - 1);
        _initialCell = _cells[randomX, randomY];
        print(_initialCell.transform.position);
    }

    //TODO: Write cheaper way to reset the grid
    private bool ResetMaze()
    {
        if (_cells[0, 0] != null)
        {
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                for (int y = 0; y < _cells.GetLength(1); y++)
                {
                    Destroy(_cells[x, y].gameObject);
                }
            }

        }

        int.TryParse(widthInput.text, out _widthSize);
        int.TryParse(heightInput.text, out _heightSize);
        if (_widthSize >= MinCells && _widthSize <= MaxCells ||  
            _heightSize >= MinCells && _heightSize <= MaxCells )
        {
            //Creates a new array with the new size;
            _cells = new Cell[_widthSize, _heightSize];
            return true;
        }

        OnInvalidParameters.Invoke();
        return false;
    }
}