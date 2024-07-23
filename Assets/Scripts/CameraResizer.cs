using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    [SerializeField] private GridGenerator grid;
    private int _gridHeight, _gridWidth;
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    public void ResizeCamera()
    {
        print(_camera.aspect);
        _gridWidth = grid.WidthSize;
        _gridHeight = grid.HeightSize;

        float horizontalRatio = _gridWidth / (_camera.pixelWidth / 2f);
        float verticalRatio = _gridHeight / (float) _camera.pixelHeight;

        Debug.Log(_gridWidth + " " + _gridHeight + " " + _camera.pixelWidth + " " + _camera.pixelHeight);

        if (verticalRatio > horizontalRatio)
        {
            _camera.orthographicSize = _gridHeight / 2;
        }
        else {
            _camera.orthographicSize = _gridWidth / _camera.aspect;
        }

        //var size = _gridWidth > _gridHeight ? _gridWidth : _gridHeight;
        //_camera.orthographicSize = size / 2;

    }
}
