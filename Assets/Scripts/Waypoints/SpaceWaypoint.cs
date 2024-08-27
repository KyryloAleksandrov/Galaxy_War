using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWaypoint : BaseWaypoint
{
    private Ship ship;
    // Start is called before the first frame update
    void Start()
    {
        waypointType = WaypointType.Space;
        ProjectContext.Instance.UnitService.OnUnitSpawn += OnShipSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShipSpawn(Transform unitPrefab, UnitType unitType)
    {
        if(unitType == UnitType.Ship)
        {
            ship = unitPrefab.GetComponent<Ship>();
            hasShip = true;
            Debug.Log("Waypoint now has ship");
            if(ship != null)
            {
                Debug.Log(ship);
            }
        }
    }
}
