using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "MyConfigs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public PlayerData[] playerDatas;
}

[Serializable]

public struct PlayerData
{
    public PlayerType playerType;
    public string playerName;
    public Material playerMaterial;
    public int homeXCoordinate;
    public int homeZCoordinate;
}