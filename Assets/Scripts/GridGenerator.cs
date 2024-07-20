using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private int widthSize, heightSize;
    [SerializeField] private GameObject mazeCell;
    private GameObject[,] _cells;
    private GameObject _initialCell;

    public void Start()
    {
        _cells = new GameObject[widthSize, heightSize];
    }

    //temporary solution
    public void GenerateMaze()
    {
        ResetMaze();
        Start();
        for (int x = 0; x < widthSize; x++)
        {
            for (int y = 0; y < heightSize; y++)
            {
                _cells[x, y] = Instantiate(mazeCell, gameObject.transform);
                mazeCell.transform.position = new(x, y);
            }
        }

        var randomX = Random.Range(0, widthSize - 1);
        var randomY = Random.Range(0, heightSize - 1);
        _initialCell = _cells[randomX, randomY];
        _initialCell.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void ResetMaze()
    {
        if (_cells != null)
        {
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                for (int y = 0; y < _cells.GetLength(1); y++)
                {
                    print(_cells[x, y]);
                    Destroy(_cells[x, y]);
                }
            }
            Array.Clear(_cells, 0, _cells.Length);
        }
    }
}
