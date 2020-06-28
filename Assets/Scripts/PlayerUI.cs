using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject m_hideScreen;


    public void showHideScreen(bool enable)
    {
        m_hideScreen.SetActive(enable);
    }

}
