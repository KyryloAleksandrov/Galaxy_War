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

    private GridSystem<GridObject> gridSystem;
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

        gridSystem = mapFunctionalService.gridSystem;
        //gridObjects = gridSystem.GetGridObjectArray();

        hexPrefab = mapVisualService.hexPrefab;
        coordinatesPrefab = mapVisualService.coordinatesPrefab;

        mapVisualService.InitializeGridMap(hexPrefab);
        gridSystem.DisplayCoordinates(coordinatesPrefab);        
    }

    // Start is called before the first frame update
    void Start()
    {
        unitService.SpawnShip(shiptoSpawn, new GridPosition(2, 3), PlayerType.PlayerOne);
        unitService.SpawnShip(shiptoSpawn, new GridPosition(2, 3), PlayerType.PlayerOne);
        unitService.SpawnShip(shiptoSpawn, new GridPosition(2, 3), PlayerType.PlayerOne);
        unitService.SpawnShip(shiptoSpawn, new GridPosition(2, 3), PlayerType.PlayerOne);
        
        unitService.SpawnShip(shiptoSpawn, new GridPosition(1, 3), PlayerType.PlayerTwo);
        unitService.SpawnShip(shiptoSpawn, new GridPosition(1, 3), PlayerType.PlayerTwo);
        unitService.SpawnShip(shiptoSpawn, new GridPosition(1, 3), PlayerType.PlayerTwo);
        unitService.SpawnShip(shiptoSpawn, new GridPosition(1, 3), PlayerType.PlayerTwo);

        unitService.SpawnShip(shiptoSpawn, new GridPosition(2, 2), PlayerType.PlayerTwo);
    }

    // Update is called once per frame
    void Update()
    {
        if(unitService.selectedShip == null)
        {
            mouseService.IdleCursor();
        }
        

        if(Input.GetMouseButtonDown(0))
        {
            if(unitService.TrySelectUnit()) return;
        }

        if(Input.GetMouseButtonDown(1))
        {
            unitService.MoveShip();
        }


        /*if(Input.GetKeyDown(KeyCode.L))     //debug
        {
            unitService.SpawnShip(shiptoSpawn, new GridPosition(0, 0));
            return;
        }*/
    }
}
