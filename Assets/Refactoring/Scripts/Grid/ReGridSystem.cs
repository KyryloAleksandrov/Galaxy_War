using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReGridSystem<TGridObject>
{
    private const float HEX_X_OFFSET_MULTIPLIER = .5f;
    private const float HEX_Z_OFFSET_MULTIPLIER = .75f;

    private int width;
    private int height;
    private float hexSize;

    private TGridObject[,] gridObjectArray;
    private List<ReGridPosition> gridPositionsList;
    private List<Vector3Int> neighbourHexesList;

    public ReGridSystem(int width, int height, float hexSize, Func<ReGridSystem<TGridObject>, ReGridPosition, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.hexSize = hexSize;

        gridObjectArray = new TGridObject[width, height];
        gridPositionsList = new List<ReGridPosition>();

        for(int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                ReGridPosition gridPosition = new ReGridPosition(x, z);
                gridObjectArray[x,z] = createGridObject(this, gridPosition);
                gridPositionsList.Add(gridPosition);
            }
        }
    }

    public Vector3 GetWorldPosition(ReGridPosition gridPosition)
    {
        return
            new Vector3(gridPosition.x, 0, 0) * hexSize +
            new Vector3(0, 0, gridPosition.z) * hexSize * HEX_Z_OFFSET_MULTIPLIER +
            ((gridPosition.z % 2) == 1 ? new Vector3(1, 0, 0) * hexSize * HEX_X_OFFSET_MULTIPLIER : Vector3.zero);
    }
    public ReGridPosition GetHexGridPosition(Vector3 worldPosition)
    {
        int roughX = Mathf.RoundToInt(worldPosition.x / hexSize);
        int roughZ = Mathf.RoundToInt(worldPosition.z / hexSize / HEX_Z_OFFSET_MULTIPLIER);

        Vector3Int roughXZ = new Vector3Int(roughX, 0, roughZ);


        bool isOddRow = roughZ % 2 == 1;
        neighbourHexesList = new List<Vector3Int>
        {
            roughXZ + new Vector3Int(-1, 0, 0),
            roughXZ + new Vector3Int(+1, 0, 0),

            roughXZ + new Vector3Int(isOddRow ? +1 : -1, 0, +1),
            roughXZ + new Vector3Int(+0, 0, +1),

            roughXZ + new Vector3Int(isOddRow ? +1 : -1, 0, -1),
            roughXZ + new Vector3Int(+0, 0, -1),    
        };

        Vector3Int closestGridPosition = roughXZ;

        foreach (Vector3Int neighbourHex in neighbourHexesList)
        {
            if(Vector3.Distance(worldPosition, GetWorldPosition(new ReGridPosition(neighbourHex.x, neighbourHex.z))) < 
               Vector3.Distance(worldPosition, GetWorldPosition(new ReGridPosition(closestGridPosition.x, closestGridPosition.z))))
               {
                    closestGridPosition = neighbourHex;
               }
        }
        return new ReGridPosition(closestGridPosition.x, closestGridPosition.z);
    }

    /*public void DisplayCoordinates(Transform coordinatesPrefab)
    {
        for (int x = 0; x < width; x++){
            for (int z = 0; z < height; z++){
                ReGridPosition gridPosition = new ReGridPosition(x, z);
                Transform coordinates = GameObject.Instantiate(coordinatesPrefab, GetWorldPosition(gridPosition), Quaternion.identity);

                MapGridCoordinates mapGridCoordinates = coordinates.GetComponent<MapGridCoordinates>(); //refactor MapGridCoordinates
                mapGridCoordinates.SetCoordinates(GetGridObject(gridPosition) as ReGridObject);
            }
        }
    }*/

    public TGridObject GetGridObject(ReGridPosition gridPosition)
    {
        if(IsInBounds(gridPosition))
        {
            return gridObjectArray[gridPosition.x, gridPosition.z];
        }
        else
        {
            return default;
        }
    }
    public bool IsInBounds(ReGridPosition gridPosition)
    {
        if(gridPosition.x >= 0 && gridPosition.x < width && gridPosition.z >= 0 && gridPosition.z < height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    public TGridObject[,] GetGridObjectArray()
    {
        return gridObjectArray;
    }
    public List<ReGridPosition> GetGridPositionsList()
    {
        return gridPositionsList;
    }
}
