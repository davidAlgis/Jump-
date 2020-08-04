using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_hideScreen;
    [SerializeField]
    private GameObject m_clickTextGO;
    [SerializeField]
    private bool m_printClick = true;


    public void showHideScreen(bool enable)
    {
        m_hideScreen.SetActive(enable);
    }



    public void Update()
    {
        if(m_printClick)
        {
            if ((Input.GetButtonDown("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
            {
                if(m_clickTextGO.TryGetComponent(out Text text))
                    UIManager.Instance.fadeOut(text);
                if(DebugTool.tryFindGOChildren(m_clickTextGO, "ClickToJump2", out GameObject go, LogType.Error))
                    if(go.TryGetComponent(out Text text2))
                        UIManager.Instance.fadeOut(text2);
                m_printClick = false;
            }
        }
    }
}
