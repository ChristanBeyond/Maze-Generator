#define original
#define fancy
// When changing the definition to use the unoriginal, new references have to be set.
// You can manually modify the references in the prefab file, from line 105 to line 108, should be replaced by:
//  walls:
//  - { fileID: 4711085446607361974}
//  - { fileID: 1614245526219987218}
//  - { fileID: 3962419109086605258}
//  - { fileID: 6450300586028126072}
// As for the fancy definition, it's the same line range, but the replacement is:
//  walls:
//  -direction: Left
//  wall: { fileID: 4711085446607361974}
//  -direction: Right
//  wall: { fileID: 1614245526219987218}
//  -direction: Top
//  wall: { fileID: 3962419109086605258}
//  -direction: Bottom
//  wall: { fileID: 6450300586028126072}

// Unnecessary usings
//using System.Collections;
//using System.Collections.Generic;
using Algorithms;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
#if original
    [SerializeField] private GameObject wallLeft, wallRight, wallTop, wallBottom;
#elif !fancy
    [SerializeField] private GameObject[] walls;
#else 
    // Only thing that's different about this, is the use of structs to still label the directions inside the inspector, instead of getting Element 0, Element 1, etc.
    // Which yes, this could theoretically be improved by using Editor Extensions - but I do not know how to use them, that's a whole different ball game, but could be interesting to look into.
    // Notably, the setup for this would be *much* easier, if you were to make the Directions enum and the hierarchy of the Maze Tile Prefab match.
    // Matching objects in hierarchies make them easily indexable, you could automate the entire process if you do that.
    // By that I mean not even needing to have SerializeFields. Right now not going to make a rewrite of it, but if you're interested I'll give it a shot.
    [System.Serializable]
    private struct Wall
    {
        [HideInInspector] public string direction;
        [SerializeField] public GameObject wall;
    }
    [SerializeField]
    private Wall[] walls;

    [ContextMenu(nameof(SetDirections))]
    private void SetDirections()
    {
        walls = new Wall[4];

        walls[0].direction = Direction.Left.ToString();
        walls[1].direction = Direction.Right.ToString();
        walls[2].direction = Direction.Top.ToString();
        walls[3].direction = Direction.Bottom.ToString();
    }
#endif

    private bool _visited, _rechecked;
    private Vector2Int _arrayPosition;
    public bool CellIsVisited
    {
        get => _visited;
        set
        {
            if (_visited != value) spriteRenderer.color = Color.red;
            _visited = value;
        }
    }
    public bool CellIsRechecked
    {
        get => _rechecked;
        set
        {
            if (_rechecked != value) spriteRenderer.color = Color.green;
            _rechecked = value;
        }
    }

    public Vector2Int ArrayPosition { get => _arrayPosition; set => _arrayPosition = value; }

    public void RemoveWall(Direction wallToRemove)
    {
#if original
        switch (wallToRemove)
        {
            // Indirection calls, your IDE should log that too. Keep an eye on the Messages tab of the Error List. (Assuming VS, with Rider I've no idea).
            // https://github.com/microsoft/Microsoft.Unity.Analyzers/blob/main/doc/UNT0019.md
            case Direction.Top:
                //wallTop.gameObject.SetActive(false);
                wallTop.SetActive(false);
                break;
            case Direction.Left:
                //wallLeft.gameObject.SetActive(false);
                wallLeft.SetActive(false);
                break;
            case Direction.Bottom:
                //wallBottom.gameObject.SetActive(false);
                wallBottom.SetActive(false);
                break;
            case Direction.Right:
                //wallRight.gameObject.SetActive(false);
                wallRight.SetActive(false);
                break;
        }
#elif !fancy
        // A condideration could be made to just use arrays instead of a switch case.
        walls[(int)wallToRemove].SetActive(false);
#else
        walls[(int)wallToRemove].wall.SetActive(false);
#endif
    }
}
