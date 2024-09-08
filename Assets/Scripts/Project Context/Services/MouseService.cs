using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IMouseService
{
    LayerMask hexGridLayerMask {get; set;}
    LayerMask shipLayerMask {get; set;}
    void IdleCursor();
    LayerMask GetFirstLayerMask();
    Vector3 GetMouseWorldPosition();
}
public class MouseService : IMouseService
{
    public LayerMask hexGridLayerMask {get; set;}
    public LayerMask shipLayerMask {get; set;}
    private GridSystem gridSystem;
    private GridVisual lastGridVisual;

    public MouseService(IMapFunctionalService mapFunctionalService, ILayerMasksService layerMasksService)
    {
        gridSystem = mapFunctionalService.gridSystem;
        hexGridLayerMask = layerMasksService.hexGridMask;
        shipLayerMask = layerMasksService.shipsMask;
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, hexGridLayerMask);
        return raycastHit.point;
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

        /*Debug.Log(gridObject.ToString());
        foreach(var spaceWaypoint in gridObject.GetSpaceWaypoints())
        {
            Debug.Log(spaceWaypoint.GetHasShip());
        }*/

        /*List<GridPosition> neigbours = ProjectContext.Instance.MapFunctionalService.GetNeighbourGridPositions(currentGridPosition, 3);
        foreach(var position in neigbours)
        {
            GridVisual neigbourHex = gridSystem.GetGridObject(position).GetGridVisual();
            neigbourHex.Highlight();
            Debug.Log(position.ToString());
        }*/
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

    public void IdleCursor()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        HighlightOnHover(GetFirstLayerMask());
    }
}
