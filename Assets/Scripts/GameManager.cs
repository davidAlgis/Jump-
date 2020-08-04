using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;

    private int nbrOfPlayer = 0;
    [SerializeField]
    private LevelGeneration m_levelGenerator = null;

    [SerializeField]
    private GameObject m_currentCamera;


    #region getter
    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            return m_instance;
        }
    }

    public int NbrOfPlayer { get => nbrOfPlayer; set => nbrOfPlayer = value; }
    public GameObject CurrentCamera { get => m_currentCamera; set => m_currentCamera = value; }
    public LevelGeneration LevelGenerator { get => m_levelGenerator; set => m_levelGenerator = value; }


    #endregion

    public void Awake()
    {
        if(m_levelGenerator = null)
            Debug.LogError("Unable to find any level generation");
        
    }

    public void enableDisableCurrentCamera(bool enable)
    {
        if (m_currentCamera.TryGetComponent(out Camera camera))
            camera.enabled = enable;
        else
            Debug.LogError("Unable to find any camera component attached to " + m_currentCamera);

       
    }

    public void reloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public int getActualSceneIndex()
    {
        int getIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Scene index = " + getIndex);
        return getIndex;
    }

}
