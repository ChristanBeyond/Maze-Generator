using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GridGenerator : MonoBehaviour
{
    //TODO: Check if I can store the grid information in a scriptable object
    
    public int widthSize, heightSize;
    [SerializeField] private Cell mazeCell;
    public Cell[,] _cells;
    public Cell _initialCell;

    public void Start()
    {
        _cells = new Cell[widthSize, heightSize];
    }

    public void OnGenerateGrid()
    {
        StartCoroutine(GenerateGrid());
    }

    //temporary solution
    private IEnumerator GenerateGrid()
    {
        ResetMaze();
        for (int y = 0; y < heightSize; y++)
        {
            for (int x = 0; x < widthSize; x++)
            {
                Cell newCell = Instantiate(mazeCell, gameObject.transform);
                newCell.cellPosition = new Vector3Int(x, y); //temporary check to see if grid generates correctly
                newCell.transform.position = new(x, y);
                _cells[x,y] = newCell;
                print($"{x} & {y}");
                print($"cellPosition {mazeCell.cellPosition}");
                print($"Object position {mazeCell.transform.position}");
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        var randomX = Random.Range(0, widthSize - 1);
        var randomY = Random.Range(0, heightSize - 1);
        _initialCell = _cells[randomX, randomY];
        print(_initialCell.transform.position);
    }

    //TODO: Write cheaper way to reset the grid
    private void ResetMaze()
    {
        if (_cells[0,0] != null)
        {
            for (int y = 0; y < _cells.GetLength(0); y++)
            {
                for (int x = 0; x < _cells.GetLength(1); x++)
                {
                    Destroy(_cells[x, y].gameObject);
                }
            }
            //Clears the array completely
            _cells = new Cell[widthSize, heightSize];
        }
    }
}
