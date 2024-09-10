using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BaseUnit : MonoBehaviour
{
    protected PlayerType playerType;
    protected UnitType unitType;
    [SerializeField] protected MeshRenderer selectedVisual;

    protected GridPosition currentGridPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual PlayerType GetPlayerType()
    {
        return playerType;
    }
    public virtual void SetPlayerType(PlayerType playerType)
    {
        this.playerType = playerType;
    }

    public virtual UnitType GetUnitType()
    {
        return unitType;
    }
    public virtual GridPosition GetCurrentGridPosition()
    {
        return currentGridPosition;
    }
    public virtual void SetCurrentGridPosition(GridPosition gridPosition)
    {
        this.currentGridPosition = gridPosition;
    }

    public virtual void Deselect()
    {
        selectedVisual.enabled = false;
    }

    public virtual void Select()
    {
        selectedVisual.enabled = true;
    }
}
