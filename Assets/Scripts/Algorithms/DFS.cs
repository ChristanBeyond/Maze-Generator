using System.Collections.Generic;
using UnityEngine;

namespace Algorithms
{
    public class DFS : MonoBehaviour
    {
        [SerializeField] private GameObject initialCell;
        private GameObject[,] _cellArray; //I think it would be best to retrieve this info from a scriptable game object? 
        private Stack<GameObject> _checkedCellsStack = new Stack<GameObject>();
        
        /*
         *     Choose the initial cell, mark it as visited and push it to the stack
                While the stack is not empty
                Pop a cell from the stack and make it a current cell
                If the current cell has any neighbours which have not been visited
                Push the current cell to the stack
                Choose one of the unvisited neighbours
                Remove the wall between the current cell and the chosen cell
                Mark the chosen cell as visited and push it to the stack
         */
        
        


    }
}
