using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;

    private SpaceWaypoint[] spaceWaypoints;
    private const int SPACE_WAYPOINT_LIMIT = 8;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        spaceWaypoints = new SpaceWaypoint[SPACE_WAYPOINT_LIMIT];
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public SpaceWaypoint[] GetSpaceWaypoints()
    {
        return spaceWaypoints;
    }
    /*public void AddSpaceWaypoint(SpaceWaypoint spaceWaypoint)
    {
        spaceWaypoints.Add(spaceWaypoint);
    }*/
}
