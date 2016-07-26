using UnityEngine;
using System.Collections;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook {

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        Player player = gamePlayer.GetComponent<Player>();

        player.playerName = lobby.playerName;
        player.playerColor = lobby.playerColor;
    }
}
