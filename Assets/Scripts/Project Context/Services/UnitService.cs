using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public interface IUnitService
{
    List<Ship> listOfShips {get; set;}
    Ship selectedShip {get; set;}
    

    void SpawnShip(Transform unitPrefab, GridPosition gridPosition, PlayerType playerType);
    bool TrySelectUnit();
    void MoveShip();
}
public class UnitService : IUnitService
{
    public List<Ship> listOfShips {get; set;}
    public Ship selectedShip {get; set;}

    private Vector3 shipSpawnOffset;

    private IMapFunctionalService MapFunctionalService;
    private IConfigService ConfigService;
    private IMouseService MouseService;
    private IPlayerService PlayerService;
    

    public UnitService(IMapFunctionalService MapFunctionalService, IConfigService ConfigService, IMouseService MouseService, IPlayerService PlayerService)
    {
        this.MapFunctionalService = MapFunctionalService;
        this.ConfigService = ConfigService;
        this.MouseService = MouseService;
        this.PlayerService = PlayerService;
        listOfShips = new List<Ship>();

        shipSpawnOffset = new Vector3(0,3,0);
    }
    

    public void SpawnShip(Transform unitPrefab, GridPosition gridPosition, PlayerType playerType)
    {
        GridObject gridObjectToSpawn = MapFunctionalService.gridSystem.GetGridObject(gridPosition);

        SpaceWaypoint availableWaypoint = gridObjectToSpawn.GetAvailableSpaceWaypoint();

        if(availableWaypoint == null)
        {
            return;
        }

        Transform newShip = GameObject.Instantiate(unitPrefab, availableWaypoint.transform.position + shipSpawnOffset, Quaternion.identity);
        SetMaterialsForSpawn(newShip, playerType);
        gridObjectToSpawn.AddShip(newShip.GetComponent<Ship>());
        availableWaypoint.AddShip();    //it just toggles true false
        AddShip(newShip);

        newShip.GetComponent<Ship>().SetCurrentGridPosition(gridPosition);
        newShip.GetComponent<Ship>().SetCurrentSpaceWaypoint(availableWaypoint);
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

    public void AddShip(Transform unitPrefab)
    {
        listOfShips.Add(unitPrefab.GetComponent<Ship>());

        //Debug.Log("Unit added to list of all units");
        /*foreach(var unit in listOfShips)
        {
            Debug.Log(unit);
        }*/
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

    public void MoveShip()
    {
        if(selectedShip == null)
        {
            return;
        }

        GridPosition mousePosition = MapFunctionalService.gridSystem.GetHexGridPosition(MouseService.GetMouseWorldPosition());

        selectedShip.GetMoveAction().Move(mousePosition);
    }

    public void SetMaterialsForSpawn(Transform unit, PlayerType playerType)
    {
        Renderer[] childRenderers = unit.GameObject().GetComponentsInChildren<Renderer>();

        Material materialToSpawn = PlayerService.GetPlayer(playerType).GetPlayerMaterial();

        foreach(Renderer renderer in childRenderers)
        {
            renderer.material = materialToSpawn;
        }
    }
}
