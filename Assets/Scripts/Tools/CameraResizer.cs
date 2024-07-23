using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    [SerializeField] private GridGenerator grid;
    private int _gridHeight, _gridWidth;
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
    }

    public void ResizeCamera()
    {
        _gridWidth = grid.WidthSize;
        _gridHeight = grid.HeightSize;

        float horizontalRatio = _gridWidth / (_camera.pixelWidth / 2f);
        float verticalRatio = _gridHeight / (float)_camera.pixelHeight;

        //Resizes camera based on which value is higher to ensure the maze fits. 
        if (verticalRatio > horizontalRatio) _camera.orthographicSize = _gridHeight / 2;
        else _camera.orthographicSize = _gridWidth / _camera.aspect;
    }
}
