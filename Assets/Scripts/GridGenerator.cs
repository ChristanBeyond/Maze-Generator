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
        for (int x = 0; x < widthSize; x++)
        {
            for (int y = 0; y < heightSize; y++)
            {
                Cell newCell = Instantiate(mazeCell, gameObject.transform);
                newCell.transform.position = new(x, y);
                _cells[x, y] = newCell;

                yield return new WaitForSeconds(0.0001f);
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
        if (_cells[0, 0] != null)
        {
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                for (int y = 0; y < _cells.GetLength(1); y++)
                {
                    Destroy(_cells[x, y].gameObject);
                }
            }

            //Clears the array completely
            _cells = new Cell[widthSize, heightSize];
        }
    }
}