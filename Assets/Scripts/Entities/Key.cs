using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.PLAYER))
        {
            collision.GetComponent<PlayerController>().hasKey = true;
            // TODO: Play sound & update UI
            Destroy(gameObject);
        }
    }
}
