using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    [SerializeField] private LayerMask hexGridLayerMask;
    private GridSystem gridSystem;
    private GridVisual lastGridVisual;
    // Start is called before the first frame update
    void Start()
    {
        gridSystem = ProjectContext.Instance.MapFunctionalService.gridSystem;
    }

    // Update is called once per frame
    void Update()
    {

        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        HighlightOnHover(GetFirstLayerMask());
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

    public void HighlightHex()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isOnGrid = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, hexGridLayerMask);
        if(isOnGrid)
        {
            GridPosition currentGridPosition = gridSystem.GetHexGridPosition(raycastHit.point);
            if(gridSystem.IsInBounds(currentGridPosition))
            {
                if(lastGridVisual != null)
                {
                    lastGridVisual.DeHighlight();
                }

                lastGridVisual = gridSystem.GetGridObject(currentGridPosition).GetGridVisual();
                if(lastGridVisual != null)
                {
                    lastGridVisual.Highlight();
                }
            }
        }
        else
        {
            if(lastGridVisual != null)
            {
                lastGridVisual.DeHighlight();
            }
        }

    }

    public void HighlightOnHover(LayerMask layerMask)
    {
         if(layerMask != hexGridLayerMask)
        {
            if(lastGridVisual != null)
            {
                lastGridVisual.DeHighlight();
            }
        }
        else if(layerMask == hexGridLayerMask)
        {
            HighlightHex();
            if(Input.GetMouseButtonDown(0))
            {
                ClickOnHex();
            }
        }
    }

    public LayerMask GetFirstLayerMask()
    {
        LayerMask hitLayerMask = default;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            int layer = hit.collider.gameObject.layer;
            hitLayerMask = 1 << layer;
        }

        return hitLayerMask;
    }
}
