using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Algorithms
{
    //TODO: Decouple GridGenerator from Algorith, maybe with help of a scriptable object
    public class DFS : MonoBehaviour
    {
        [SerializeField] private Cell initialCell;
        public GridGenerator generator;
        private Stack<Cell> _checkedCellsStack = new Stack<Cell>();
        public Cell[,] cellGrid;

        private Cell _currrentCell;

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
                _currrentCell = _checkedCellsStack.Pop();
                print(_currrentCell.transform.position + "Current");
                //Check and choose a random unvisited neighbour
                var nextCell = CheckNeighbours(_currrentCell);
                // print(nextCell.transform.position + "Next");
                //Push back the currentCell into the stack
                
                if (nextCell != null) 
                {
                    _checkedCellsStack.Push(_currrentCell);
                    var cellPosition = Vector3Int.FloorToInt(nextCell.transform.position);
                    cellGrid[cellPosition.x, cellPosition.y].CellIsVisited = true;
                    _checkedCellsStack.Push(nextCell);
                }
                yield return new WaitForSeconds(0.00001f);
            }
        }
        //TODO: Include function that removes the walls

        private Cell CheckNeighbours(Cell cell)
        {
            Vector3 cellPos = cell.transform.position;
            Vector3Int currentPos = Vector3Int.FloorToInt(cellPos);
            
            print($"uncasted pos {cellPos.x}");
            print($"currentPos {currentPos.x}");
            // var topY = currentPos.y + 1;
            // var bottomY = currentPos.y - 1;
            // var leftX = currentPos.x - 1;
            // var rightX = currentPos.x + 1;
            // print($"Top {topY} Bottom {bottomY} Left {leftX} Right {rightX}");
            //
            var topCell = currentPos.y + 1 < generator.heightSize  ? cellGrid[currentPos.x, currentPos.y + 1] : null;
            // print($"topPos {currentPos.x}");
            // print(topCell.transform.position);
            // print($"topPos {currentPos.x}");
            var bottomCell = currentPos.y -1 >= 0 ? cellGrid[currentPos.x, currentPos.y - 1] : null;
            var leftCell = currentPos.x -1 >= 0 ? cellGrid[currentPos.x - 1, currentPos.y] : null;
            var rightCell = currentPos.x + 1 < generator.widthSize ? cellGrid[currentPos.x + 1, currentPos.x] : null;
            
            List<Cell> unvisitedCells = new List<Cell>();

            if(topCell != null && !topCell.CellIsVisited) unvisitedCells.Add(topCell);
            if(bottomCell != null && !bottomCell.CellIsVisited) unvisitedCells.Add(bottomCell);
            if(leftCell != null && !leftCell.CellIsVisited) unvisitedCells.Add(leftCell);
            if(rightCell != null && !rightCell.CellIsVisited) unvisitedCells.Add(rightCell);
            
            return unvisitedCells.Count > 0 ? unvisitedCells[Random.Range(0, unvisitedCells.Count)] : null;
        }
        
    }
}
