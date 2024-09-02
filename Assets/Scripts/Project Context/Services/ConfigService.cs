using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConfigService
{
    MapData MapData {get;}
    UnitData[] UnitDatas {get;}
    LayerMasksData[] LayerMasksDatas {get;}
}
public class ConfigService : IConfigService
{
    public MapData MapData {get;}
    public UnitData[] UnitDatas {get;}
    public LayerMasksData[] LayerMasksDatas {get;}

    public ConfigService(MapConfig mapConfig, UnitConfig unitConfig, LayerMasksConfig layerMasksConfig)
    {
        MapData = mapConfig.MapData;
        UnitDatas = unitConfig.unitDatas;
        LayerMasksDatas = layerMasksConfig.layerMasksDatas;
    }
}
