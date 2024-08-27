using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;

    private List<SpaceWaypoint> spaceWaypoints;
    private List<Ship> listOfShips;
    private const int SPACE_WAYPOINT_LIMIT = 8;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        spaceWaypoints = new List<SpaceWaypoint>();
        listOfShips = new List<Ship>();
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public List<SpaceWaypoint> GetSpaceWaypoints()
    {
        return spaceWaypoints;
    }
    /*public void AddSpaceWaypoint(SpaceWaypoint spaceWaypoint)
    {
        spaceWaypoints.Add(spaceWaypoint);
    }*/
    
    public SpaceWaypoint GetAvailableSpaceWaypoint()
    {
        foreach(var spaceWaypoint in spaceWaypoints)
        {
            if(spaceWaypoint.GetHasShip() == false)
            {
                return spaceWaypoint;
            }
        }
        Debug.LogAssertion("No space waypoint for hex " + ToString());
        return null;
    }

    public void AddUnit(Transform unitPrefab, UnitType unitType)
    {
        listOfShips.Add(unitPrefab.GetComponent<Ship>());

        Debug.Log("Unit added to grid Object");
        foreach(var unit in listOfShips)
        {
            Debug.Log(unit);
        }
    }
}
