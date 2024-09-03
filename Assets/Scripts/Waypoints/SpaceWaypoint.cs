using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWaypoint : BaseWaypoint
{
    private Ship ship;
    void Awake()
    {
        waypointType = WaypointType.Space;
        hasShip = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        //ProjectContext.Instance.UnitService.OnUnitSpawn += OnShipSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        
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
