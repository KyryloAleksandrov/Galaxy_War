using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Ship : BaseUnit
{
    private SpaceWaypoint currentSpaceWaypoint;
    private int move;

    [SerializeField] private MoveAction moveAction;

    List<GridPosition> hexesInMoveRange;
    List<GridPosition> availableMoves;      //filter hexesInMoveRange with move restrictions and add only available positions to this list
    
    // Start is called before the first frame update
    void Start()
    {
        unitType = UnitType.Ship;
    
        //currentGridPosition = ProjectContext.Instance.MapFunctionalService.gridSystem.GetHexGridPosition(transform.position);
        move = 2;
        hexesInMoveRange = new List<GridPosition>();
        availableMoves = new List<GridPosition>();

        Deselect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Select()
    {
        base.Select();
        SetAvailableMovesList();
        foreach(var position in availableMoves)
        {
            ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridObject(position).GetGridVisual().Highlight();
        }
        
        Debug.Log(currentGridPosition);
        Debug.Log(currentSpaceWaypoint);
    }

    public override void Deselect()
    {
        base.Deselect();
        if(hexesInMoveRange.Count > 0)
        {
            foreach(var position in hexesInMoveRange)
            {
                ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridObject(position).GetGridVisual().DeHighlight();
            }
        }
    }

    public void SetAvailableMovesList()
    {
        availableMoves.Clear();
        hexesInMoveRange = ProjectContext.Instance.MapFunctionalService.GetNeighbourGridPositions(currentGridPosition, move);
        foreach(var position in hexesInMoveRange)
        {
            GridObject toTest = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridObject(position);
            if(toTest.GetAvailableSpaceWaypoint() != null && !toTest.IsFullForThePlayer(playerType))
            {
                availableMoves.Add(position);
            }
        }
    }
    public List<GridPosition> GetAvailableMovesList()
    {
        SetAvailableMovesList();
        return availableMoves;
    }

    public int GetMove()
    {
        return move;
    }

    public SpaceWaypoint GetCurrentSpaceWaypoint()
    {
        return currentSpaceWaypoint;
    }
    public void SetCurrentSpaceWaypoint(SpaceWaypoint spaceWaypoint)
    {
        this.currentSpaceWaypoint = spaceWaypoint;
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }
}
