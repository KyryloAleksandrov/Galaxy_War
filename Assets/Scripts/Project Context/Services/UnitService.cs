using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public interface IUnitService
{
    List<Ship> listOfShips {get; set;}
    Ship selectedShip {get; set;}

    Action<Transform, UnitType> OnUnitSpawn {get; set;}
    

    void SpawnUnit(Transform unitPrefab, GridPosition gridPosition);
    bool TrySelectUnit();
}
public class UnitService : IUnitService
{
    public List<Ship> listOfShips {get; set;}
    public Ship selectedShip {get; set;}

    public Action<Transform, UnitType> OnUnitSpawn {get; set;}

    private Vector3 shipSpawnOffset;

    private IMapFunctionalService MapFunctionalService;
    private IConfigService ConfigService;
    private IMouseService MouseService;

    public UnitService(IMapFunctionalService MapFunctionalService, IConfigService ConfigService, IMouseService MouseService)
    {
        this.MapFunctionalService = MapFunctionalService;
        this.ConfigService = ConfigService;
        this.MouseService = MouseService;
        listOfShips = new List<Ship>();

        shipSpawnOffset = new Vector3(0,3,0);

        OnUnitSpawn += AddUnit;
    }
    

    public void SpawnUnit(Transform unitPrefab, GridPosition gridPosition)
    {
        GridObject gridObjectToSpawn = MapFunctionalService.GridSystem.GetGridObject(gridPosition);

        SpaceWaypoint availableWaypoint = gridObjectToSpawn.GetAvailableSpaceWaypoint();

        if(availableWaypoint == null)
        {
            return;
        }

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

        //Debug.Log("Unit added to list of all units");
        foreach(var unit in listOfShips)
        {
            Debug.Log(unit);
        }
    }

    public bool TrySelectUnit()
    {
        //LayerMask unitLayerMask = MouseService.GetFirstLayerMask();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, MouseService.shipLayerMask))
        {
            if(selectedShip != null)
            {
                selectedShip.Deselect();
            }

            if(raycastHit.transform.TryGetComponent<Ship>(out Ship ship))
            {
                if(ship == selectedShip)
                {
                    selectedShip.Deselect();
                    selectedShip = null;
                    return true;
                }
                selectedShip = ship;
                selectedShip.Select();
                return true;
            }
        }
        return false;
    }
}
