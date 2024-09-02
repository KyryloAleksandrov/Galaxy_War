using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUnitService
{
    List<Ship> listOfShips {get; set;}

    Action<Transform, UnitType> OnUnitSpawn {get; set;}

    void SpawnUnit(Transform unitPrefab, GridPosition gridPosition);
}
public class UnitService : IUnitService
{
    public List<Ship> listOfShips {get; set;}

    public Action<Transform, UnitType> OnUnitSpawn {get; set;}

    private Vector3 shipSpawnOffset;

    private IMapFunctionalService MapFunctionalService;
    private IConfigService ConfigService;

    public UnitService(IMapFunctionalService MapFunctionalService, IConfigService ConfigService)
    {
        this.MapFunctionalService = MapFunctionalService;
        this.ConfigService = ConfigService;
        listOfShips = new List<Ship>();

        shipSpawnOffset = new Vector3(0,3,0);

        OnUnitSpawn += AddUnit;
    }
    

    public void SpawnUnit(Transform unitPrefab, GridPosition gridPosition)
    {
        GridObject gridObjectToSpawn = MapFunctionalService.GridSystem.GetGridObject(gridPosition);

        SpaceWaypoint availableWaypoint = gridObjectToSpawn.GetAvailableSpaceWaypoint();

        GameObject.Instantiate(unitPrefab, availableWaypoint.transform.position + shipSpawnOffset, Quaternion.identity);
        gridObjectToSpawn.AddUnit(unitPrefab, GetUnitType(unitPrefab));
        availableWaypoint.OnShipSpawn(unitPrefab, GetUnitType(unitPrefab));
        OnUnitSpawn?.Invoke(unitPrefab, GetUnitType(unitPrefab));
    }

    private UnitType GetUnitType(Transform unitPrefab)
    {
        foreach(var unitData in ConfigService.UnitDatas)
        {
            foreach(var unit in unitData.units)
            {
                if(unitPrefab == unit)
                {
                    return unitData.unitType;
                }
            }
        }
        return default;
    }

    public void AddUnit(Transform unitPrefab, UnitType unitType)
    {
        listOfShips.Add(unitPrefab.GetComponent<Ship>());

        Debug.Log("Unit added to list of all units");
        foreach(var unit in listOfShips)
        {
            Debug.Log(unit);
        }
    }
}
