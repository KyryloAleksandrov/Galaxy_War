using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BaseUnit : MonoBehaviour
{
    protected UnitType unitType;
    [SerializeField] protected MeshRenderer selectedVisual;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual UnitType GetUnitType()
    {
        return unitType;
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
