using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.TryGetComponent(out Player player))
                player.StopMove = true;
            else
                Debug.LogError("Unable to find any Player componement on player");

            if (collision.gameObject.TryGetComponent(out PlayerNetwork playerNetwork))
                playerNetwork.playerDie();
            else
                Debug.LogError("Unable to find any PlayerNetwork on player");
        }
    }
}
