using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisual : MonoBehaviour
{
    private GridObject gridObject;
    [SerializeField] private SpaceWaypoint[] spaceWaypoints; 
    // Start is called before the first frame update
    void Start()
    {
        if(!gridObject.GetSpaceWaypoints().ContainsKey(WaypointType.Space))
        {
            List<BaseWaypoint> spaceWaypoints = new List<BaseWaypoint>();
            gridObject.GetSpaceWaypoints().Add(WaypointType.Space, spaceWaypoints);
        }

        for(int i = 0; i < spaceWaypoints.Length; i++)
        {
            gridObject.GetSpaceWaypoints()[WaypointType.Space].Add(spaceWaypoints[i]);
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
