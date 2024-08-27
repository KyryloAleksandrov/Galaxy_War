using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMapFunctionalService
{
    GridSystem GridSystem {get;}
}
public class MapFunctionalService : IMapFunctionalService
{
    public GridSystem GridSystem {get; private set;}

    public MapFunctionalService(IConfigService ConfigService)
    {
        var MapData = ConfigService.MapData;
        GridSystem = new GridSystem(MapData.width, MapData.height, MapData.hexSize);
    }
}
