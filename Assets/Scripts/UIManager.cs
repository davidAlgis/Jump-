using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;



    public static UIManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.Find("MainUI").GetComponent<UIManager>();
            }
            return m_instance;
        }
    }


    public void enableDisableMessageOnScreen(string message, bool enable)
    {
        print("enable text message");
        GameObject toEnableGO;
        if (DebugTool.tryFindGOChildren(gameObject, "Message", out toEnableGO, LogType.Warning))
        {

            if (toEnableGO.TryGetComponent(out Text textToEnable))
            {
                textToEnable.text = message;
                textToEnable.enabled = enable;
            }
        }
    }



}
