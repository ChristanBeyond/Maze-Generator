using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //TODO: Reset the color here.
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool _visited;
    public bool CellIsVisited
    {
        get => _visited;
        set
        {
            if (_visited != value) spriteRenderer.color = Color.red;
            _visited = value;
        }
    }
    public Vector3Int cellPosition;
}
