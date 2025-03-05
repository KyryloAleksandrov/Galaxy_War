using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMapBackendGenerationService
{
    GridSystem<T> CreateGridSystem<T>(Func<GridSystem<T>, GridPosition, T> createGridObject);
}
public class MapBackendGenerationService : IMapBackendGenerationService
{
    public int width;
    public int height;
    public float hexSize;
    public MapBackendGenerationService(IConfigService ConfigService)
    {
        var MapData = ConfigService.MapData;

        width = MapData.width;
        height = MapData.height;
        hexSize = MapData.hexSize;

    }
    public GridSystem<T> CreateGridSystem<T>(Func<GridSystem<T>, GridPosition, T> createGridObject)
    {
        return new GridSystem<T>(width, height, hexSize, createGridObject);
    }
}
