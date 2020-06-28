using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;

    [SerializeField]
    private Text m_messageText = default;


    public static UIManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.Find("UIManager").GetComponent<UIManager>();
            }
            return m_instance;
        }
    }

    public Text MessageText { get => m_messageText; set => m_messageText = value; }

    public void enableDisableMessageOnScreen(string message, bool enable)
    {
        m_messageText.enabled = enable;
        m_messageText.text = message;
    }



}
