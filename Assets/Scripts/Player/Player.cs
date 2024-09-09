using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //not entirely sure if I need Monobehaviour on player, will see in future
{
    private PlayerType playerType;
    private string playerName;
    private Material playerMaterial;
    private GridObject homeSystem;

    public Player(PlayerType playerType, string playerName, Material playerMaterial, GridObject homeSystem)
    {
        this.playerType = playerType;
        this.playerName = playerName;
        this.playerMaterial = playerMaterial;
        this.homeSystem = homeSystem;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlayerType GetPlayerType()
    {
        return playerType;
    }

    public Material GetPlayerMaterial()
    {
        return playerMaterial;
    }
}
