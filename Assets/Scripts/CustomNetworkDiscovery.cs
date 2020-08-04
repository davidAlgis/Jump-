using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomNetworkDiscovery : NetworkDiscovery
{
    private Button m_hostButton;
    private Button m_joinButton;
    //private bool m_serverIsJoin = false;
    int minPort = 10000;
    int maxPort = 10010;
    int defaultPort = 10000;


    void Start()
    {
        m_hostButton = GameObject.Find("HostButton").GetComponent<Button>();
        m_joinButton = GameObject.Find("JoinButton").GetComponent<Button>();
        
        m_joinButton.onClick.AddListener(startClient);
        m_hostButton.onClick.AddListener(startServer);
        //Application.runInBackground = true;
        //startServer();
    }

    public void startClient()
    {
        Initialize();
        
        StartAsClient();
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log("Server Found");
       /* if(m_serverIsJoin == false)
        {*/
            
        gameObject.GetComponent<CustomNetworkManager>().networkAddress = fromAddress;
        gameObject.GetComponent<CustomNetworkManager>().networkPort = 7777;
        gameObject.GetComponent<CustomNetworkManager>().StartClient();
        print("stop broadcast");
        StopBroadcast();
        //m_serverIsJoin = true;
        //}
    }


    //Call to create a server
    public void startServer()
    {
        int serverPort = createServer();
        if (serverPort != -1)
        {
            Debug.Log("Server created on port : " + serverPort);
            broadcastData = serverPort.ToString();
            Initialize();
            gameObject.GetComponent<CustomNetworkManager>().StartHost();
            StartAsServer();
        }
        else
        {
            Debug.Log("Failed to create Server");
        }
    }

    //Creates a server then returns the port the server is created with. Returns -1 if server is not created
    private int createServer()
    {
        int serverPort = -1;
        //Connect to default port
        //gameObject.GetComponent<CustomNetworkManager>().networkPort = defaultPort;
        bool serverCreated = NetworkServer.Listen(defaultPort);
        if (serverCreated)
        {
            serverPort = defaultPort;
            Debug.Log("Server Created with default port : " + defaultPort);
        }
        else
        {
            Debug.Log("Failed to create with the default port");
            //Try to create server with other port from min to max except the default port which we trid already
            for (int tempPort = minPort; tempPort <= maxPort; tempPort++)
            {
                //Skip the default port since we have already tried it
                if (tempPort != defaultPort)
                {
                    //Exit loop if successfully create a server
                    if (NetworkServer.Listen(tempPort))
                    {
                        serverPort = tempPort;
                        break;
                    }

                    //If this is the max port and server is not still created, show, failed to create server error
                    if (tempPort == maxPort)
                    {
                        Debug.LogError("Failed to create server");
                    }
                }
            }
        }
        return serverPort;
    }

}
