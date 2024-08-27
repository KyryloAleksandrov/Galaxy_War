using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;

    private Dictionary<WaypointType, List<BaseWaypoint>> listOfWaypoints;
    private Dictionary<UnitType, List<BaseUnit>> listOfUnits;
    private const int SPACE_WAYPOINT_LIMIT = 8;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        listOfWaypoints = new Dictionary<WaypointType, List<BaseWaypoint>>();
        listOfUnits = new Dictionary<UnitType, List<BaseUnit>>();

        ProjectContext.Instance.UnitService.OnUnitSpawn += AddUnit;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public Dictionary<WaypointType, List<BaseWaypoint>> GetSpaceWaypoints()
    {
        return listOfWaypoints;
    }
    /*public void AddSpaceWaypoint(SpaceWaypoint spaceWaypoint)
    {
        spaceWaypoints.Add(spaceWaypoint);
    }*/
    
    public SpaceWaypoint GetAvailableSpaceWaypoint()
    {
        SpaceWaypoint availableWaypoint = null;

        if(!listOfWaypoints.ContainsKey(WaypointType.Space))
        {
            Debug.LogAssertion("No space waypoints detected on the grid object " + ToString());
            return null;
        }

        foreach(var spaceWaypoint in listOfWaypoints[WaypointType.Space])
        {
            if(!spaceWaypoint.GetHasShip())
            {
                availableWaypoint = (SpaceWaypoint)spaceWaypoint;
            }
        }

        return availableWaypoint;
    }

    public void AddUnit(Transform unitPrefab, UnitType unitType)
    {
        if (!listOfUnits.ContainsKey(unitType))
        {
            List<BaseUnit> units = new List<BaseUnit>();
            listOfUnits.Add(unitType, units);
        }

        listOfUnits[unitType].Add(unitPrefab.GetComponent<BaseUnit>());

        Debug.Log("Unit added to grid Object");
        foreach(var unit in listOfUnits[unitType])
        {
            Debug.Log(unit);
        }
    }
}
