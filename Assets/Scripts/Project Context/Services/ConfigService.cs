using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IConfigService
{
    MapData MapData {get;}
    UnitData[] UnitDatas {get;}
    LayerMasksData[] LayerMasksDatas {get;}
    PlayerData[] playerDatas {get;}
}
public class ConfigService : IConfigService
{
    public MapData MapData {get;}
    public UnitData[] UnitDatas {get;}
    public LayerMasksData[] LayerMasksDatas {get;}
    public PlayerData[] playerDatas {get;}

    public ConfigService(MapConfig mapConfig, UnitConfig unitConfig, LayerMasksConfig layerMasksConfig, PlayerConfig playerConfig)
    {
        MapData = mapConfig.MapData;
        UnitDatas = unitConfig.unitDatas;
        LayerMasksDatas = layerMasksConfig.layerMasksDatas;
        playerDatas = playerConfig.playerDatas;
    }
}
