using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private const float HEX_X_OFFSET_MULTIPLIER = .5f;
    private const float HEX_Z_OFFSET_MULTIPLIER = .75f;

    private int width;
    private int height;
    private float hexSize;

    private GridObject[,] gridObjectArray;
    private List<GridPosition> gridPositionsList;
    private List<Vector3Int> neighbourHexesList;

    public GridSystem(int width, int height, float hexSize)
    {
        this.width = width;
        this.height = height;
        this.hexSize = hexSize;

        gridObjectArray = new GridObject[width, height];
        gridPositionsList = new List<GridPosition>();

        for(int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x,z] = new GridObject(this, gridPosition);
                gridPositionsList.Add(gridPosition);
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return
            new Vector3(gridPosition.x, 0, 0) * hexSize +
            new Vector3(0, 0, gridPosition.z) * hexSize * HEX_Z_OFFSET_MULTIPLIER +
            ((gridPosition.z % 2) == 1 ? new Vector3(1, 0, 0) * hexSize * HEX_X_OFFSET_MULTIPLIER : Vector3.zero);
    }
    public GridPosition GetHexGridPosition(Vector3 worldPosition)
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
            if(Vector3.Distance(worldPosition, GetWorldPosition(new GridPosition(neighbourHex.x, neighbourHex.z))) < 
               Vector3.Distance(worldPosition, GetWorldPosition(new GridPosition(closestGridPosition.x, closestGridPosition.z))))
               {
                    closestGridPosition = neighbourHex;
               }
        }
        return new GridPosition(closestGridPosition.x, closestGridPosition.z);
    }

    public void DisplayCoordinates(Transform coordinatesPrefab)
    {
        for (int x = 0; x < width; x++){
            for (int z = 0; z < height; z++){
                GridPosition gridPosition = new GridPosition(x, z);
                Transform coordinates = GameObject.Instantiate(coordinatesPrefab, GetWorldPosition(gridPosition), Quaternion.identity);

                MapGridCoordinates mapGridCoordinates = coordinates.GetComponent<MapGridCoordinates>();
                mapGridCoordinates.SetCoordinates(GetGridObject(gridPosition));
            }
        }
    }
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        if(IsInBounds(gridPosition))
        {
            return gridObjectArray[gridPosition.x, gridPosition.z];
        }
        else
        {
            return null;
        }
    }
    public bool IsInBounds(GridPosition gridPosition)
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
    public GridObject[,] GetGridObjectArray()
    {
        return gridObjectArray;
    }
    public List<GridPosition> GetGridPositionsList()
    {
        return gridPositionsList;
    }
}
