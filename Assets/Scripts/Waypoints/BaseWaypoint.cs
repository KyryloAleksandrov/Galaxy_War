using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWaypoint : MonoBehaviour
{
    protected WaypointType waypointType;
    protected GridObject gridObject;
    protected bool hasShip;

    protected virtual void Awake()
    {
        hasShip = false;        //watch for possible conflicts with other classes
    }

    public virtual WaypointType GetWaypointType()
    {
        return waypointType;
    }
    public virtual void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
    public virtual GridObject GetGridObject()
    {
        return gridObject;
    }

    public virtual bool GetHasShip()
    {
        return hasShip;
    }
    public virtual void SetHasShip(bool hasShip)
    {
        this.hasShip = hasShip;
    }
}
