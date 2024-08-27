using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConfigService
{
    MapData MapData {get;}
    UnitData[] UnitDatas {get;}
}
public class ConfigService : IConfigService
{
    public MapData MapData {get;}
    public UnitData[] UnitDatas {get;}

    public ConfigService(MapConfig mapConfig, UnitConfig unitConfig)
    {
        MapData = mapConfig.MapData;
        UnitDatas = unitConfig.unitDatas;
    }
}
