using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private int shipLimitPerPlayer;
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private GridVisual gridVisual;

    private List<SpaceWaypoint> spaceWaypoints;
    private Dictionary<PlayerType, List<Ship>> listOfShips;
    private const int SPACE_WAYPOINT_LIMIT = 8;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        spaceWaypoints = new List<SpaceWaypoint>();
        listOfShips = new Dictionary<PlayerType, List<Ship>>();

        shipLimitPerPlayer = 4;
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
    
    public Dictionary<PlayerType, List<Ship>> GetlistOfShips()
    {
        return listOfShips;
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

    public void AddShip(Ship ship, PlayerType playerType)
    {
        if(!listOfShips.ContainsKey(playerType))
        {
            List<Ship> shipsOfThePlayer = new List<Ship>();
            listOfShips.Add(playerType, shipsOfThePlayer);
        }
        listOfShips[playerType].Add(ship);
    }

    public void RemoveShip(Ship ship, PlayerType playerType)
    {
        listOfShips[playerType].Remove(ship);
    }

    public bool IsFullForThePlayer(PlayerType playerType)
    {
        if(!listOfShips.ContainsKey(playerType))
        {
            return false;
        }
        if(listOfShips[playerType].Count < shipLimitPerPlayer)
        {
            return false;
        }

        return true;
    }
}
