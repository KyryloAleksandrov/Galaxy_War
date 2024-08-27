using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWaypoint : MonoBehaviour
{
    private Ship ship;
    private WaypointType waypointType;
    private bool hasShip;
    // Start is called before the first frame update
    void Start()
    {
        waypointType = WaypointType.Space;
        hasShip = false;
        //ProjectContext.Instance.UnitService.OnUnitSpawn += OnShipSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetHasShip()
    {
        return hasShip;
    }
    public void OnShipSpawn(Transform unitPrefab, UnitType unitType)
    {
            hasShip = true;
            Debug.Log("Waypoint now has ship");
            if(ship != null)
            {
                Debug.Log(ship);
            }
    }
}
