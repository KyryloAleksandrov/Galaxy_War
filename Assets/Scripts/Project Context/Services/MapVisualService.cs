using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IMapVisualService
{
    Transform HexPrefab {get;}
    GridPosition[,] GridPositions {get; set;}
  
}
public class MapVisualService : IMapVisualService
{
    public Transform HexPrefab {get;}
    public GridPosition[,] GridPositions { get; set; }

    private IMapFunctionalService MapFunctionalService;

    public MapVisualService(IMapFunctionalService MapFunctionalService)
    {
        this.MapFunctionalService = MapFunctionalService;
    }
}
