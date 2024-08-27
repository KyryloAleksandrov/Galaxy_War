using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MapConfig", menuName = "MyConfigs/MapConfig")]
public class MapConfig : ScriptableObject
{
   public MapData MapData;
}

[Serializable]
public struct MapData
{
    public int width;
    public int height;
    public float hexSize;

    public Transform hexPrefab;
    public Transform coordintaesPrefab;
}