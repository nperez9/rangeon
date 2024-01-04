using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance = null;
    [SerializeField] List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        instance = this;
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void OnPlayerMove()
    {
        // Corrutine to prevent enemy moves inside the player
        StartCoroutine(MoveEnemies());
    }

    IEnumerator MoveEnemies()
    {
        // Waits until the end of frames
        yield return new WaitForFixedUpdate();

        foreach(Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.Move();
            }
        }

    }
}
