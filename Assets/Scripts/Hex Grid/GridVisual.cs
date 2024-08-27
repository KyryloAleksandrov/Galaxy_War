using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisual : MonoBehaviour
{
    private GridObject gridObject;
    [SerializeField] private SpaceWaypoint[] spaceWaypoints; 
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        for(int i = 0; i < spaceWaypoints.Length; i++)
        {
            gridObject.GetSpaceWaypoints().Add(spaceWaypoints[i]);
        }
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
