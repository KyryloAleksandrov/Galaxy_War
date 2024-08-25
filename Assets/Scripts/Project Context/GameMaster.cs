using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    private IMapFunctionalService mapFunctionalService;
    private IMapVisualService mapVisualService;

    private GridSystem gridSystem;
    private GridObject[,] gridObjects;
    private GridVisual[,] gridVisualArray;

    [SerializeField] private Transform coordinatesPrefab;
    [SerializeField] private Transform hexPrefab;

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

        gridSystem = mapFunctionalService.GridSystem;
        gridObjects = gridSystem.GetGridObjectArray();
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeGridMap();
        gridSystem.DisplayCoordinates(coordinatesPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeGridMap()
    {
        gridVisualArray = new GridVisual[gridObjects.GetLength(0), gridObjects.GetLength(1)];

        for (int x = 0; x < gridObjects.GetLength(0); x++)
        {
            for (int z = 0; z < gridObjects.GetLength(1); z++)
            {
                GridPosition gridPosition = gridObjects[x,z].GetGridPosition();

                Transform gridVisualTransform = Instantiate(hexPrefab, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);

                GridVisual gridVisual = gridVisualTransform.GetComponent<GridVisual>();
                gridVisualArray[x,z] = gridVisual;

                GridObject gridObject = gridSystem.GetGridObject(gridPosition);
                gridVisualArray[x,z].SetGridObject(gridObject);
            }
        }
    }
}
