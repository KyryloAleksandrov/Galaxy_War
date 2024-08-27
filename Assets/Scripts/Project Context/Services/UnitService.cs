using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUnitService
{
    Dictionary<UnitType, List<BaseUnit>> listOfUnits {get; set;}

    Action<Transform, UnitType> OnUnitSpawn {get; set;}

    void SpawnUnit(Transform unitPrefab, GridPosition gridPosition);
}
public class UnitService : IUnitService
{
    public Dictionary<UnitType, List<BaseUnit>> listOfUnits {get; set;}

    public Action<Transform, UnitType> OnUnitSpawn {get; set;}

    private Vector3 shipSpawnOffset;

    private IMapFunctionalService MapFunctionalService;
    private IConfigService ConfigService;

    public UnitService(IMapFunctionalService MapFunctionalService, IConfigService ConfigService)
    {
        this.MapFunctionalService = MapFunctionalService;
        this.ConfigService = ConfigService;
        listOfUnits = new Dictionary<UnitType, List<BaseUnit>>();

        shipSpawnOffset = new Vector3(0,3,0);

        OnUnitSpawn += AddUnit;
    }
    

    public void SpawnUnit(Transform unitPrefab, GridPosition gridPosition)
    {
        GridObject gridObjectToSpawn = MapFunctionalService.GridSystem.GetGridObject(gridPosition);

        SpaceWaypoint availableWaypoint = gridObjectToSpawn.GetAvailableSpaceWaypoint();

        GameObject.Instantiate(unitPrefab, availableWaypoint.transform.position + shipSpawnOffset, Quaternion.identity);
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
        if (!listOfUnits.ContainsKey(unitType))
        {
            List<BaseUnit> units = new List<BaseUnit>();
            listOfUnits.Add(unitType, units);
        }

        listOfUnits[unitType].Add(unitPrefab.GetComponent<BaseUnit>());

        Debug.Log("Unit added to list of all units");
        foreach(var unit in listOfUnits[unitType])
        {
            Debug.Log(unit);
        }
    }
}
