using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LayerMasksConfig", menuName = "MyConfigs/LayerMasksConfig")]
public class LayerMasksConfig : ScriptableObject
{
    public LayerMasksData[] layerMasksDatas;
}


[Serializable]
public struct LayerMasksData
{
    public LayerMaskType type;
    public LayerMask layerMask;
}