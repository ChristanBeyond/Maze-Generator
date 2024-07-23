using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Algorithms
{
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    }

    public class DFS : MonoBehaviour
    {
        [SerializeField] private GridGenerator generator;
        [SerializeField] private UnityEvent OnGenerateWhileGenerating;

        private Cell _initialCell, _currentCell;
        private readonly Stack<Cell> _checkedCellsStack = new();
        private Direction _direction;
        private Cell[,] _cellGrid;
        private bool _isMazeGenerating;


        public void StartGeneratingMaze()
        {
            if (_isMazeGenerating)
            {
                OnGenerateWhileGenerating.Invoke();
                return;
            }
            StartCoroutine(GenerateMaze());
        }

        //Method that makes use of the Depth-First search algorithm to generate the maze
        private IEnumerator GenerateMaze()
        {
            _isMazeGenerating = true;

            _cellGrid = generator.Cells;
            _initialCell = generator.InitialCell;

            //Mark visited Cell as visited
            _initialCell.CellIsVisited = true;
            _checkedCellsStack.Push(_initialCell);

            while (_checkedCellsStack.Count != 0)
            {
                //Pop Current Cell
                _currentCell = _checkedCellsStack.Pop();

                //Check and choose a random unvisited neighbour
                var nextCell = GetValidNeighbourCell(_currentCell);

                if (nextCell != null)
                {
                    _checkedCellsStack.Push(_currentCell);
                    var cellPosition = nextCell.ArrayPosition;
                    _cellGrid[cellPosition.x, cellPosition.y].CellIsVisited = true;
                    _direction = GetDirectionBetweenCells(_currentCell, nextCell);
                    RemoveWallBetweenCells(_direction, _currentCell, nextCell);
                    _checkedCellsStack.Push(nextCell);
                }
                //If a new cell hasn't been found, this means this cell has already been checked and will change color again
                else _currentCell.CellIsRechecked = true;

                yield return new WaitForFixedUpdate();
            }

            _isMazeGenerating = false;
        }

        //Method used to check neighbouring cells of the parameter cell
        private Cell GetValidNeighbourCell(Cell cell)
        {
            Vector2Int currentPos = cell.ArrayPosition;

            var topCell = currentPos.y + 1 < generator.HeightSize ? _cellGrid[currentPos.x, currentPos.y + 1] : null;
            var bottomCell = currentPos.y - 1 >= 0 ? _cellGrid[currentPos.x, currentPos.y - 1] : null;
            var leftCell = currentPos.x - 1 >= 0 ? _cellGrid[currentPos.x - 1, currentPos.y] : null;
            var rightCell = currentPos.x + 1 < generator.WidthSize ? _cellGrid[currentPos.x + 1, currentPos.y] : null;

            List<Cell> unvisitedCells = new List<Cell>();

            if (topCell != null && !topCell.CellIsVisited) unvisitedCells.Add(topCell);
            if (bottomCell != null && !bottomCell.CellIsVisited) unvisitedCells.Add(bottomCell);
            if (leftCell != null && !leftCell.CellIsVisited) unvisitedCells.Add(leftCell);
            if (rightCell != null && !rightCell.CellIsVisited) unvisitedCells.Add(rightCell);

            var newCell = unvisitedCells.Count > 0 ? unvisitedCells[Random.Range(0, unvisitedCells.Count)] : null;

            return newCell;
        }

        private Direction GetDirectionBetweenCells(Cell currentCell, Cell newCell)
        {
            if (newCell.ArrayPosition.y > currentCell.ArrayPosition.y) return Direction.Top;
            else if (newCell.ArrayPosition.y < currentCell.ArrayPosition.y) return Direction.Bottom;
            else if (newCell.ArrayPosition.x < currentCell.ArrayPosition.x) return Direction.Left;
            else return Direction.Right;
        }

        private void RemoveWallBetweenCells(Direction dir, Cell currentCell, Cell newCell)
        {
            switch (dir)
            {
                case Direction.Top:
                    currentCell.RemoveWall(Direction.Top);
                    newCell.RemoveWall(Direction.Bottom);
                    break;
                case Direction.Bottom:
                    currentCell.RemoveWall(Direction.Bottom);
                    newCell.RemoveWall(Direction.Top);
                    break;
                case Direction.Left:
                    currentCell.RemoveWall(Direction.Left);
                    newCell.RemoveWall(Direction.Right);
                    break;
                case Direction.Right:
                    currentCell.RemoveWall(Direction.Right);
                    newCell.RemoveWall(Direction.Left);
                    break;
            }
        }
    }
}
