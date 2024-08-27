using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWaypoint : MonoBehaviour
{
    protected WaypointType waypointType;
    protected bool hasShip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool GetHasShip()
    {
        return hasShip;
    }
}
