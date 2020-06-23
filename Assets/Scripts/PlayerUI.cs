using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject m_hideScreen;


    public void showHideScreen()
    {
        m_hideScreen.SetActive(true);
    }

}
