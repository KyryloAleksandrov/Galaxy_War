using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private GridVisual gridVisual;

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

    public GridVisual GetGridVisual()
    {
        return gridVisual;
    }
    public void SetGridVisual(GridVisual gridVisual)
    {
        this.gridVisual = gridVisual;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public List<SpaceWaypoint> GetSpaceWaypoints()
    {
        return spaceWaypoints;
    }
    public void AddSpaceWaypoints(GridVisual gridVisual)
    {
        for(int i = 0; i < gridVisual.GetSpaceWaypoints().Length; i++)
        {
            spaceWaypoints.Add(gridVisual.GetSpaceWaypoints()[i]);
        }
    }
    
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

        //Debug.Log("Unit added to grid Object");
        /*foreach(var unit in listOfShips)
        {
            Debug.Log(unit);
        }*/
    }
}
