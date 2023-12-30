using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int currentHP;
    public int maxHP;
    public int coins;
    public bool hasKey;
    public int damage = 1;

    // the layers you can collide 
    public LayerMask moveLayerMask;

    [SerializeField] private SpriteRenderer sprite;

    private void Move(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.0f, moveLayerMask);

        if (hit.collider == null)
        {
            transform.position += new Vector3(dir.x, dir.y, 0);
            // Moves the enemy when the player moves
            EnemyManager.instance.OnPlayerMove();
            // Moves the map
            Generator.instance.OnPlayerMove();
        }
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.up);
        }
    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.down);
        }
    }
    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.right);
        }
    }
    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.left);
        }
    }

    public void OnAttackUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TryAttack(Vector2.up);
        }
    }

    public void OnAttackLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TryAttack(Vector2.left);
        }
    }

    public void OnAttackRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TryAttack(Vector2.right);
        }
    }

    public void OnAttackDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TryAttack(Vector2.down);
        }
    }

    private void TryAttack(Vector2 dir)
    {
        //  only detect layer number 9
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.0f, 1 << 7);

        if (hit.collider != null)
        {
            hit.transform.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        StartCoroutine(DamageFlash());
        UI.instance.UpdateHearts(currentHP);

        if (currentHP <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator DamageFlash()
    {
        Color defaultColor = sprite.color;
        sprite.color = Color.white;

        yield return new WaitForSeconds(0.25f);

        sprite.color = defaultColor;
    }

    IEnumerator GameOver()
    {
        // TODO play death musci
        yield return new WaitForSeconds(0.1f);

        sprite.flipY = true;
        sprite.color = Color.gray;

        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UI.instance.UpdateCoins(coins);
        // TODO: update UI
    }

    public bool AddHealth(int amount)
    {
        currentHP += amount;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
            return false;
        }

        UI.instance.UpdateHearts(currentHP);
        return true;
    }
}
