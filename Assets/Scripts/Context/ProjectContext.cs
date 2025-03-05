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

    public IMapBackendGenerationService MapBackendGenerationService {get; private set;}
    public IMapFrontendGenerationService MapFrontendGenerationService {get; private set;}

    public void Initialize(MapConfig mapConfig)
    {
        ConfigService = new ConfigService(mapConfig);

        MapBackendGenerationService = new MapBackendGenerationService(ConfigService);
        MapFrontendGenerationService = new MapFrontendGenerationService(MapBackendGenerationService, ConfigService);
        Debug.Log("Initialized");
    }
}
