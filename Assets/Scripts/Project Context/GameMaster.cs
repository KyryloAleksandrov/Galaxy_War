using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    private IMapFunctionalService mapFunctionalService;
    private IMapVisualService mapVisualService;

    private IUnitService unitService;
    private IMouseService mouseService;

    private GridSystem gridSystem;
    //private GridObject[,] gridObjects;  //not sure if I need this one  

    private Transform coordinatesPrefab;
    private Transform hexPrefab;

    [SerializeField] private Transform shiptoSpawn;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        mapFunctionalService = ProjectContext.Instance.MapFunctionalService;
        mapVisualService = ProjectContext.Instance.MapVisualService;
        mouseService = ProjectContext.Instance.MouseService;

        unitService = ProjectContext.Instance.UnitService;

        gridSystem = mapFunctionalService.GridSystem;
        //gridObjects = gridSystem.GetGridObjectArray();

        hexPrefab = mapVisualService.hexPrefab;
        coordinatesPrefab = mapVisualService.coordinatesPrefab;

        mapVisualService.InitializeGridMap(hexPrefab);
        gridSystem.DisplayCoordinates(coordinatesPrefab);        
    }

    // Start is called before the first frame update
    void Start()
    {
        //unitService.SpawnUnit(shiptoSpawn, new GridPosition(0, 0));
    }

    // Update is called once per frame
    void Update()
    {

        mouseService.IdleCursor();
        if(Input.GetKeyDown(KeyCode.L))
        {
            unitService.SpawnUnit(shiptoSpawn, new GridPosition(0, 0));
            return;
        }
    }
}
