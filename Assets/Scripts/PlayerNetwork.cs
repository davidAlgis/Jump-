using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class PlayerNetwork : NetworkBehaviour
{
    [SyncVar]
    private float m_rectCameraY = 0.0f;

    [SyncVar]
    private int m_level;

    [SyncVar]
    private bool m_finishTheLevel = false;

    [SerializeField]
    private GameObject m_crossDeath = default;

    private SaveSpawn m_saveSpawn;
    [SerializeField]
    //[SyncVar]
    private List<Transform> m_spawnPosition = new List<Transform>();

    private float m_oldrectCameraY = 0.0f;

    [SerializeField]
    [SyncVar]
    private int m_idPlayer = 1;

    [SerializeField]
    private PlayerUI m_myPlayerUI;
    //public int IdPlayer { get => m_idPlayer; set => m_idPlayer = value; 
    public bool FinishTheLevel { get => m_finishTheLevel; set => m_finishTheLevel = value; }



    private void Start()
    {
        m_saveSpawn = GameObject.FindGameObjectWithTag("SaveSpawn").GetComponent<SaveSpawn>();
        m_idPlayer = 1;
        m_level = 0;
        UIManager.Instance.debugMessageOnScreen(m_idPlayer.ToString());
        if (isServer)
            RpcChangeCamera();

        m_spawnPosition = m_saveSpawn.SpawnsP2;
        //m_spawnPosition.Add(transform.position);

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
        if (m_oldrectCameraY != m_rectCameraY)
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

    [ClientRpc]
    void RpcChangeCamera()
    {
        Debug.Log("rpc");
        m_idPlayer = 2;
        m_spawnPosition = m_saveSpawn.SpawnsP1;
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

    [Server]
    public void MoveTo(Vector3 newPosition)
    {
        print("move to :" + newPosition.ToString());
        transform.position = newPosition; 
        RpcMoveTo(newPosition);
    }

    public void playerDie()
    {
        StartCoroutine(die());
    }

    public void playerWin()
    {
        StartCoroutine(win());
    }

    IEnumerator die()
    {
        print("die");
        Player player = gameObject.GetComponent<Player>();
        GameObject crossOnDeath = Instantiate(m_crossDeath, player.GroundedTransform.position, Quaternion.identity);
        
        if(isLocalPlayer)    
            m_myPlayerUI.showHideScreen(false);

        yield return new WaitForSeconds(2.0f);

        if (isLocalPlayer)
            m_myPlayerUI.showHideScreen(true);

        Destroy(crossOnDeath);
        player.StopMove = false;
        MoveTo(m_spawnPosition[m_level].position);
    }

    IEnumerator win()
    {
        print("win");
        if (isLocalPlayer)
            m_myPlayerUI.showHideScreen(false);

        yield return new WaitForSeconds(2.0f);

        if (isLocalPlayer)
            m_myPlayerUI.showHideScreen(true);

        m_level++;
        print(m_level);
        if (m_level <= m_spawnPosition.Count)
            MoveTo(m_spawnPosition[m_level].position);
        else
            print("the end");
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<Player>().enabled = true;
    }
}
