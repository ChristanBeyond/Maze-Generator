using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    [SerializeField] private GameObject mazeCell;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                mazeCell = Instantiate(mazeCell, this.gameObject.transform);
                mazeCell.transform.position = new(x, y);
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
