using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectContext
{
    private static ProjectContext instance;

    public static ProjectContext Instance
    {
        get{
            if(instance == null)
            instance = new ProjectContext();
            return instance;
        }
    }

    public IConfigService ConfigService {get; private set;}

    public IMapFunctionalService MapFunctionalService {get; private set;}
    public IMapVisualService MapVisualService {get; private set;}

    public IUnitService UnitService {get; private set;}

    public void Initialize(MapConfig mapConfig, UnitConfig unitConfig)
    {
        ConfigService = new ConfigService(mapConfig, unitConfig);

        MapFunctionalService = new MapFunctionalService(ConfigService);
        MapVisualService = new MapVisualService(MapFunctionalService, ConfigService);

        UnitService = new UnitService(MapFunctionalService, ConfigService);
    }
}
