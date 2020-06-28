using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class CustomNetworkManager : NetworkLobbyManager
{
    
    public void reloadScene()
    {
        ServerChangeScene("Demo");
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("--------------------add player on server------------------");
        
        base.OnServerAddPlayer(conn, playerControllerId);
    }

    public override void OnStartClient(NetworkClient lobbyClient)
    {
        //print("on start client");
        base.OnStartClient(lobbyClient);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        print("--------------------add player on server------------------");
        Vector3 posToSpawn;
        if (numPlayers % 2 == 0)
        {
            posToSpawn = new Vector3(-10, 0.5f, 0.0f);
        }
        else
        {

            posToSpawn = new Vector3(-10, -40.5f, 0.0f);

        }

        var player = (GameObject)GameObject.Instantiate(playerPrefab, posToSpawn, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

   
}