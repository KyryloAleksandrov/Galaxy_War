using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReGridObject
{
    private ReGridSystem<ReGridObject> gridSystem;
    private ReGridPosition gridPosition;

    public ReGridObject(ReGridSystem<ReGridObject> gridSystem, ReGridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public ReGridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public override string ToString()   //TODO - remove on clean up
    {
        return gridPosition.ToString();
    }
}
