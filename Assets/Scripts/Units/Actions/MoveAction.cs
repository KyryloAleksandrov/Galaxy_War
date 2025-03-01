using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveAction : BaseAction
{

    [SerializeField] float movingSpeed;
    [SerializeField] float stoppingDistance;
    [SerializeField] float rotatingSpeed;
    [SerializeField] Vector3 shipYOffset;

    private Vector3 destination;

    protected override void Awake()
    {
        base.Awake();
        destination = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        movingSpeed = 8f;
        stoppingDistance = 0.1f;
        rotatingSpeed = 10f;
        shipYOffset = new Vector3(0,3,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }

        Vector3 moveDirection = (destination - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotatingSpeed);

        if(Vector3.Distance(transform.position, destination) > stoppingDistance)
        {
            transform.position += moveDirection * Time.deltaTime * movingSpeed;
        }
        else
        {
            ship.Select();
            isActive = false;
        }
    }

    public void Move(GridPosition gridPosition)
    {
        List<GridPosition> availableMoves = ship.GetAvailableMovesList();

        if(!availableMoves.Contains(gridPosition))
        {
            Debug.Log("Position is not valid");
            return;
        }

        GridObject gridObjectToMove = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridObject(gridPosition);

        SpaceWaypoint availableWaypoint = gridObjectToMove.GetAvailableSpaceWaypoint();

        if(availableWaypoint == null)
        {
            Debug.Log("No free waypoint");
            return;
        }
        //finish writing this
        ship.Deselect();
        GridObject currentGridObject = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridObject(ship.GetCurrentGridPosition());
        currentGridObject.RemoveShip(ship, ship.GetPlayerType());
        ship.GetCurrentSpaceWaypoint().RemoveShip();

        gridObjectToMove.AddShip(ship, ship.GetPlayerType());
        availableWaypoint.AddShip();
        ship.SetCurrentGridPosition(gridPosition);
        
        destination = availableWaypoint.transform.position + shipYOffset;
        isActive = true;
    }

    public List<GridPosition> GetValidGridPositionsList()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();


        return validGridPositions;
    }
}
