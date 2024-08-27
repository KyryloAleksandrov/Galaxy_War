using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UnitConfig", menuName = "MyConfigs/UnitConfig")]
public class UnitConfig : ScriptableObject
{
    public UnitData[] unitDatas;
}

[Serializable]
public struct UnitData
{
   public Transform[] units;
   public UnitType unitType; 
}
