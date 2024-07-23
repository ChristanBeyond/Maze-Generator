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
    public enum Direction { Top, Bottom, Left, Right }

    //TODO: Decouple GridGenerator from Algorith, maybe with help of a scriptable object
    public class DFS : MonoBehaviour
    {
        
        
        [SerializeField] private Cell initialCell;
        public GridGenerator generator;
        private readonly Stack<Cell> _checkedCellsStack = new();
        private Direction _direction;
        public Cell[,] cellGrid;
        private Cell _currentCell;

        public void OnGenerateMaze()
        {
            StartCoroutine(GenerateMaze());
        }

        private IEnumerator GenerateMaze()
        {
            cellGrid = generator._cells;
            //Get Initial Cell
            initialCell = generator._initialCell;
            //Mark visited Cell as visited
            initialCell.CellIsVisited = true;
            _checkedCellsStack.Push(initialCell);

            while (_checkedCellsStack.Count != 0)
            {
                //Pop Current Cell
                _currentCell = _checkedCellsStack.Pop();
               
                //Check and choose a random unvisited neighbour
                var nextCell = CheckNeighbours(_currentCell);
                _currentCell.GetComponent<SpriteRenderer>().color = Color.red;
                if (nextCell != null) 
                {
                    _checkedCellsStack.Push(_currentCell);
                    var cellPosition = nextCell.arrayPosition;
                    cellGrid[cellPosition.x, cellPosition.y].CellIsVisited = true;
                    RemoveWallBetweenCells(_direction, _currentCell, nextCell);
                    //Call method to remove the walls by using the direction, get current cell and new cell
                    _checkedCellsStack.Push(nextCell);
                }
                yield return new WaitForFixedUpdate();
            }
        }

        private Cell CheckNeighbours(Cell cell)
        {
            Vector2Int currentPos = cell.arrayPosition;
            
            var topCell = currentPos.y + 1 < generator.HeightSize  ? cellGrid[currentPos.x, currentPos.y + 1] : null;
            var bottomCell = currentPos.y -1 >= 0 ? cellGrid[currentPos.x, currentPos.y - 1] : null;
            var leftCell = currentPos.x -1 >= 0 ? cellGrid[currentPos.x - 1, currentPos.y] : null;
            var rightCell = currentPos.x + 1 < generator.WidthSize ? cellGrid[currentPos.x + 1, currentPos.y] : null;
            
            List<Cell> unvisitedCells = new List<Cell>();

            if(topCell != null && !topCell.CellIsVisited) unvisitedCells.Add(topCell);
            if(bottomCell != null && !bottomCell.CellIsVisited) unvisitedCells.Add(bottomCell);
            if(leftCell != null && !leftCell.CellIsVisited) unvisitedCells.Add(leftCell);
            if(rightCell != null && !rightCell.CellIsVisited) unvisitedCells.Add(rightCell);
            
            print(Random.Range(0, unvisitedCells.Count));
            var newCell = unvisitedCells.Count > 0 ? unvisitedCells[Random.Range(0, unvisitedCells.Count)] : null;

            if (newCell != null)
            {
                if (newCell.arrayPosition.y > currentPos.y)
                {
                    _direction = Direction.Top;
                } else if (newCell.arrayPosition.y < currentPos.y)
                {
                    _direction = Direction.Bottom;
                } else if (newCell.arrayPosition.x < currentPos.x)
                {
                    _direction = Direction.Left;
                }
                else _direction = Direction.Right;
            }
            unvisitedCells.Clear();
            return newCell;
        }
        
        private void RemoveWallBetweenCells(Direction dir, Cell currentCell, Cell newCell){
            
            //based on direction, remove walls.
            switch (dir)
            {
                case Direction.Top:
                    currentCell.DisableWall(Direction.Top);
                    newCell.DisableWall(Direction.Bottom);
                    break;
                case Direction.Bottom:
                    currentCell.DisableWall(Direction.Bottom);
                    newCell.DisableWall(Direction.Top);
                    break;
                case Direction.Left:
                    currentCell.DisableWall(Direction.Left);
                    newCell.DisableWall(Direction.Right);
                    break;
                case Direction.Right:
                    currentCell.DisableWall(Direction.Right);
                    newCell.DisableWall(Direction.Left);
                    break;
            }
        }
    }
}
