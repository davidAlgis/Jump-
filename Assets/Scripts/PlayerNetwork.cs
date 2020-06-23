using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class PlayerNetwork : NetworkBehaviour
{
    [SyncVar]
    private float m_rectCameraY = 0.0f;

    private float m_oldrectCameraY = 0.0f;

    [SerializeField]
    [SyncVar]
    private int m_idPlayer;

    public int IdPlayer { get => m_idPlayer; set => m_idPlayer = value; }

    [ClientRpc]
    void RpcChangeCamera()
    {
        GameObject PlayerCamera;
        if (DebugTool.tryFindGOChildren(gameObject, "PlayerCamera", out PlayerCamera))
            if (PlayerCamera.TryGetComponent(out Camera camera))
            {
                Rect saveOldRect = camera.rect;
                m_rectCameraY = 0.5f;
                camera.rect = new Rect(0.0f, 0.5f, saveOldRect.width, saveOldRect.height);

            }

    }



    public void Start()
    {
        if (isServer) 
            RpcChangeCamera();
    }

    public void Update()
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

    public override void OnStartLocalPlayer()
    {
        GetComponent<Player>().enabled = true;

        if (TryGetComponent(out NetworkIdentity networkId))
            m_idPlayer = (int)networkId.netId.Value;
    }
}
