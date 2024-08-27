using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;

    private List<SpaceWaypoint> spaceWaypoints;
    private Dictionary<UnitType, List<BaseUnit>> listOfUnits;
    private const int SPACE_WAYPOINT_LIMIT = 8;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        spaceWaypoints = new List<SpaceWaypoint>();
        listOfUnits = new Dictionary<UnitType, List<BaseUnit>>();

        
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
