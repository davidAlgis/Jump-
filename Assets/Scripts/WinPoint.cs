using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            UIManager.Instance.enableDisableMessageOnScreen("Gagner !!", true);
            GameManager.Instance.reloadLevel();
        }
    }

}
