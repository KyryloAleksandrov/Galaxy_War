using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    protected UnitType unitType;

    public virtual UnitType GetUnitType()
    {
        return unitType;
    }
}
