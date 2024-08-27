using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private LayerMask hexGridLayerMask;
    private GridSystem gridSystem;
    // Start is called before the first frame update
    void Start()
    {
        gridSystem = ProjectContext.Instance.MapFunctionalService.GridSystem;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ClickOnHex();
        }
    }
    public void ClickOnHex()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isOnGrid = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, hexGridLayerMask);
        
        if(!isOnGrid)
        {
            return;
        }

        GridPosition currentGridPosition = gridSystem.GetHexGridPosition(raycastHit.point);

        GridObject gridObject = gridSystem.GetGridObject(currentGridPosition);

        Debug.Log(gridObject.ToString());
        foreach(var spaceWaypoint in gridObject.GetSpaceWaypoints())
        {
            Debug.Log(spaceWaypoint.GetHasShip());
        }
    }
}
