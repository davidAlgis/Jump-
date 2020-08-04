using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class CustomNetworkManager : NetworkLobbyManager
{
    
    public void reloadScene()
    {
        Debug.Log("load demo");
        ServerChangeScene("Demo");
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        print("on server disconnect");
        base.OnServerDisconnect(conn);
    }
    public override void OnServerSceneChanged(string sceneName)
    {
        //print("server load scene");
        //GameManager.Instance.getActualSceneIndex();
        //GameManager.Instance.LevelGenerator.initGeneration();
        base.OnServerSceneChanged(sceneName);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //Debug.Log("--------------------add player on server------------------");
        //GameManager.Instance.LevelGenerator.initGeneration();
        base.OnServerAddPlayer(conn, playerControllerId);
    }

    public override void OnStartClient(NetworkClient lobbyClient)
    {
        base.OnStartClient(lobbyClient);
    }


    public override void OnStartServer()
    {
        print("start sever on port network manager");
        base.OnStartServer();

    }

    public override void OnStartHost()
    {
        base.OnStartHost();
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        print("--------------------add player on server2------------------");
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