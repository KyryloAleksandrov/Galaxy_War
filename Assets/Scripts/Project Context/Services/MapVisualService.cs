using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IMapVisualService
{
    //GridPosition[,] GridPositions {get; set;}   //not sure if I need this one  
    GridVisual[,] gridVisualArray {get; set;}
    Transform hexPrefab {get;}
    Transform coordinatesPrefab {get;}
    
    void InitializeGridMap(Transform hexPrefab);
}
public class MapVisualService : IMapVisualService
{
    //public GridPosition[,] GridPositions { get; set; }
    public GridVisual[,] gridVisualArray { get; set; }
    public Transform hexPrefab {get;}
    public Transform coordinatesPrefab {get;}

    private IMapFunctionalService MapFunctionalService;

    public MapVisualService(IMapFunctionalService MapFunctionalService, IConfigService ConfigService)
    {
        this.MapFunctionalService = MapFunctionalService;
        hexPrefab = ConfigService.MapData.hexPrefab;
        coordinatesPrefab = ConfigService.MapData.coordintaesPrefab;
    }

    public void InitializeGridMap(Transform hexPrefab)
    {
        GridObject[,] gridObjectsArray = MapFunctionalService.gridSystem.GetGridObjectArray();
        gridVisualArray = new GridVisual[gridObjectsArray.GetLength(0), gridObjectsArray.GetLength(1)];

        for (int x = 0; x < gridObjectsArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridObjectsArray.GetLength(1); z++)
            {
                GridPosition gridPosition = gridObjectsArray[x,z].GetGridPosition();

                Transform gridVisualTransform = GameObject.Instantiate(hexPrefab, MapFunctionalService.gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);

                GridVisual gridVisual = gridVisualTransform.GetComponent<GridVisual>();
                gridVisualArray[x,z] = gridVisual;

                GridObject gridObject = MapFunctionalService.gridSystem.GetGridObject(gridPosition);
                gridObject.SetGridVisual(gridVisual);
                gridVisual.SetGridObject(gridObject);
                gridObject.AddSpaceWaypoints(gridVisual);
            }
        }
    }
}
