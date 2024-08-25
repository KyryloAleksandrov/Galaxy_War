using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisual : MonoBehaviour
{
    private GridObject gridObject; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
    public GridObject GetGridObject()
    {
        return gridObject;
    }
}
