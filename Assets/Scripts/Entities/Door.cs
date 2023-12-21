using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.PLAYER))
        {
            if (collision.GetComponent<PlayerController>().hasKey)
            {
                Debug.Log("You win!");
            }
        }
    }
}
