using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public interface IMapFrontendGenerationService 
{
    GridSystem<GridTile> gridSystem {get;}

    Transform tilePrefab {get;}
    Transform coordinatesPrefab {get;}
    
    void InitializeGridMap();
}
public class MapFrontendGenerationService : IMapFrontendGenerationService
{
    public GridSystem<GridTile> gridSystem {get;}
    public Transform tilePrefab {get;}
    public Transform coordinatesPrefab {get;}

    private IMapBackendGenerationService MapBackendGenerationService;

    public MapFrontendGenerationService(IMapBackendGenerationService MapBackendGenerationService, IConfigService ConfigService)
    {
        this.MapBackendGenerationService = MapBackendGenerationService;
        tilePrefab = ConfigService.MapData.hexPrefab;
        coordinatesPrefab = ConfigService.MapData.coordintaesPrefab;

        gridSystem = MapBackendGenerationService.CreateGridSystem<GridTile>((GridSystem<GridTile> g, GridPosition gridPosition) => new GridTile(gridPosition));
        
    }

    public void InitializeGridMap()
    {
        GridTile[,] gridTileArray = gridSystem.GetGridObjectArray();

        for (int x = 0; x < gridTileArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridTileArray.GetLength(1); z++)
            {
                GridPosition gridPosition = gridTileArray[x,z].GetGridPosition();
                Transform gridTileTransform = GameObject.Instantiate(tilePrefab, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);

                GridTile gridTile = gridTileTransform.GetComponent<GridTile>();
                gridTileArray[x,z] = gridTile;
            }
        }
    }
}
