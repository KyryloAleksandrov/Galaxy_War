using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ship : BaseUnit
{
    private int move;

    [SerializeField] private MoveAction moveAction;

    List<GridPosition> possibleMoves;
    
    // Start is called before the first frame update
    void Start()
    {
        unitType = UnitType.Ship;
    
        currentGridPosition = ProjectContext.Instance.MapFunctionalService.gridSystem.GetHexGridPosition(transform.position);
        move = 1;
        possibleMoves = new List<GridPosition>();

        Deselect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Select()
    {
        base.Select();
        possibleMoves = ProjectContext.Instance.MapFunctionalService.GetNeighbourGridPositions(currentGridPosition, move);
        foreach(var position in possibleMoves)
        {
            ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridObject(position).GetGridVisual().Highlight();
        }
        
        //Debug.Log(currentGridPosition);
    }

    public override void Deselect()
    {
        base.Deselect();
        if(possibleMoves.Count > 0)
        {
            foreach(var position in possibleMoves)
            {
                ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridObject(position).GetGridVisual().DeHighlight();
            }
        }
    }
}
