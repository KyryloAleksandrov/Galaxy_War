using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    public IMapBackendGenerationService mapBackendGenerationService;
    public IMapFrontendGenerationService mapFrontendGenerationService;


    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        mapBackendGenerationService = ProjectContext.Instance.MapBackendGenerationService;
        mapFrontendGenerationService = ProjectContext.Instance.MapFrontendGenerationService;
    }

    // Start is called before the first frame update
    void Start()
    {
        mapFrontendGenerationService.InitializeGridMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
