using System.Collections;
using System.Collections.Generic;
using Algorithms;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject wallLeft, wallRight, wallTop, wallBottom;
    private bool _visited;
    public bool CellIsVisited
    {
        get => _visited;
        set
        {
            if (_visited != value) spriteRenderer.color = Color.red;
            _visited = value;
        }
    }

    public void DisableWall(Direction wallToRemove)
    {
        switch (wallToRemove)
        {
            case Direction.Top:
                wallTop.gameObject.SetActive(false);
                break;
            case Direction.Left:
                wallLeft.gameObject.SetActive(false);
                break;
            case Direction.Bottom:
                wallBottom.gameObject.SetActive(false);
                break;
            case Direction.Right:
                wallRight.gameObject.SetActive(false);
                break;
            
        }
    }

 
}
