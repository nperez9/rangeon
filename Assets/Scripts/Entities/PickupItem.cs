using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Coin,
    Health
}

public class PickupItem : MonoBehaviour
{
    public PickupType type;
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.PLAYER))
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            switch (type)
            {
                case PickupType.Coin:
                    player.AddCoins(value);
                    Destroy(gameObject);
                    break;
                case PickupType.Health:
                    bool isHealed = player.AddHealth(value);
                    if (isHealed)
                    {
                        Destroy(gameObject);
                    }
                    break;
            }

        }
    }
}
