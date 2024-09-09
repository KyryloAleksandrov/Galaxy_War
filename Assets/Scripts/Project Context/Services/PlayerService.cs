using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerService
{
    List<Player> players {get; set;}
    Player activePlayer {get; set;}
    Player GetPlayer(PlayerType playerType);
}
public class PlayerService : IPlayerService
{
    public List<Player> players {get; set;}
    public Player activePlayer {get; set;}

    public PlayerService(IConfigService ConfigService, IMapFunctionalService mapFunctionalService)
    {
        players = new List<Player>();
        foreach(var playerData in ConfigService.playerDatas)
        {
            players.Add(new Player(playerData.playerType, playerData.playerName, playerData.playerMaterial, mapFunctionalService.gridSystem.GetGridObject(new GridPosition(playerData.homeXCoordinate, playerData.homeZCoordinate))));
        }
    }

    public Player GetPlayer(PlayerType playerType)
    {
        foreach(var player in players)
        {
            if(player.GetPlayerType() == playerType)
            {
                return player;
            }
        }
        Debug.LogAssertion("No player with this player type " + playerType + " is found");
        return null;
    }
}
