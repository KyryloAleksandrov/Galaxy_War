using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private UnitType unitType;
    // Start is called before the first frame update
    void Start()
    {
        unitType = UnitType.Ship;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UnitType GetUnitType()
    {
        return unitType;
    }
}
