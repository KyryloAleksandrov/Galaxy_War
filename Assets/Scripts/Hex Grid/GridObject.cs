using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridObject {
    //private GridSystem<GridObject> gridSystem;
    private GridPosition gridPosition;

    public GridObject(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

}

