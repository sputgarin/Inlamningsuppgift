using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformCollide : MonoBehaviour
{

    public GameObject player;

   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        player.transform.parent = transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player)
        {
            player.transform.parent = null;

        }
    }

}
