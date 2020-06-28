using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class PlayerNetwork : NetworkBehaviour
{
    [SyncVar]
    private float m_rectCameraY = 0.0f;

    [SerializeField]
    private GameObject m_crossDeath = default; 

    [SerializeField]
    [SyncVar]
    private Vector3 m_spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);

    private float m_oldrectCameraY = 0.0f;

    [SerializeField]
    [SyncVar]
    private int m_idPlayer;

    [SerializeField]
    private PlayerUI m_myPlayerUI;
    public int IdPlayer { get => m_idPlayer; set => m_idPlayer = value; }
    public Vector3 SpawnPosition { get => m_spawnPosition; set => m_spawnPosition = value; }

    [ClientRpc]
    void RpcChangeCamera()
    {
        Debug.Log("rpc");
        GameObject PlayerCamera;
        if (DebugTool.tryFindGOChildren(gameObject, "PlayerCamera", out PlayerCamera))
            if (PlayerCamera.TryGetComponent(out Camera camera))
            {
                Rect saveOldRect = camera.rect;
                m_rectCameraY = 0.5f;
                camera.rect = new Rect(0.0f, 0.5f, saveOldRect.width, saveOldRect.height);

            }
    }

    [ClientRpc]
    void RpcMoveTo(Vector3 newPosition)
    {
        transform.position = newPosition; 
    }

    [ClientRpc]
    void RpcprintCrossOnDeath()
    {

    }

    [Server]
    public void MoveTo(Vector3 newPosition)
    { 
        transform.position = newPosition; 
        RpcMoveTo(newPosition);
    }

    public void playerDie()
    {
        StartCoroutine(die());

    }

    /*
    [Command]
    void CmdRespawnSvr()
    {
        gameObject.transform.position = m_spawnPosition;
    }

    public void respawn()
    {
       
        CmdRespawnSvr();
    }*/

    private void Start()
    {
        if (isServer)
            RpcChangeCamera();

        m_spawnPosition = transform.position;

        if (isLocalPlayer)
        {
            GameObject PlayerUI;
            if (DebugTool.tryFindGOChildren(gameObject, "PlayerUI", out PlayerUI))
            {
                if (PlayerUI.TryGetComponent(out PlayerUI playerUI))
                {
                    m_myPlayerUI = playerUI;
                    m_myPlayerUI.showHideScreen(true);
                }
                else
                    Debug.LogError("Unable to find PlayerUI component on Player UI");
            }
        } 
    }

    private void Update()
    {
        //TODO : need a clean up
        //avoid updating at each frame
        if(m_oldrectCameraY != m_rectCameraY)
        {
            GameObject PlayerCamera;
            if (DebugTool.tryFindGOChildren(gameObject, "PlayerCamera", out PlayerCamera))
                if (PlayerCamera.TryGetComponent(out Camera camera))
                {
                    Rect saveOldRect = camera.rect;
                    camera.rect = new Rect(0.0f, m_rectCameraY, saveOldRect.width, saveOldRect.height);
                }
            m_oldrectCameraY = m_rectCameraY;
        }
    }

    IEnumerator die()
    {
        Player player = gameObject.GetComponent<Player>();
        GameObject crossOnDeath = Instantiate(m_crossDeath, player.GroundedTransform.position, Quaternion.identity);
        
        if(isLocalPlayer)    
            m_myPlayerUI.showHideScreen(false);

        yield return new WaitForSeconds(2.0f);


        if (isLocalPlayer)
            m_myPlayerUI.showHideScreen(true);

        Destroy(crossOnDeath);
        player.StopMove = false;
        MoveTo(m_spawnPosition);
    }

    public override void OnStartLocalPlayer()
    {
        //to avoid controlling the other player
        GetComponent<Player>().enabled = true;

        if (TryGetComponent(out NetworkIdentity networkId))
            m_idPlayer = (int)networkId.netId.Value;
    }
}
