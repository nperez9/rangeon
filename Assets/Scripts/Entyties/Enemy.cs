using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerController player;
    public int health;
    public int damage;
    public GameObject deathDropPrefab;
    public SpriteRenderer sr;

    public LayerMask moveLayerMask;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void Move()
    {
        if (Random.value < 0.5f)
            return;
        Vector3 dir = Vector3.zero;
        bool canMove = false;
        while (canMove == false)
        {
            // Same logic as Player
            dir = GetRandomDirection();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.0f, moveLayerMask);
            if (hit.collider == null)
                canMove = true;
        }
      
        transform.position += dir;
    }
    Vector3 GetRandomDirection()
    {
        int ran = Random.Range(0, 4);
        if (ran == 0)
            return Vector3.up;
        else if (ran == 1)
            return Vector3.down;
        else if (ran == 2)
            return Vector3.left;
        else if (ran == 3)
            return Vector3.right;
        return Vector3.zero;
    }

    IEnumerator DamageFlash()
    {
        Color defaultColor = sr.color;
        sr.color = Color.white;

        yield return new WaitForSeconds(0.25f);

        sr.color = defaultColor;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // intanciate the drop if it has one
            if (deathDropPrefab != null)
            {
                if (deathDropPrefab != null)
                {
                    Instantiate(deathDropPrefab, transform.position, Quaternion.identity);
                }
            }

            Destroy(gameObject);
        }

        StartCoroutine(DamageFlash());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
