using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.TryGetComponent(out Player player))
                player.StopMove = true;
            else
                Debug.LogError("Unable to find any Player componement on player");

            if (collision.gameObject.TryGetComponent(out PlayerNetwork playerNetwork))
            {
                playerNetwork.FinishTheLevel = true;
                playerNetwork.playerWin();
            }
            else
                Debug.LogError("Unable to find any PlayerNetwork on player");
            //print("You win");
            //UIManager.Instance.enableDisableMessageOnScreen("Gagner !!", true);
            //GameManager.Instance.reloadLevel();
        }
    }

}
