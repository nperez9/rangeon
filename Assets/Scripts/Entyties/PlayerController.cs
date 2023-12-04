using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int currentHP;
    public int maxHP;
    public int coins;
    public bool hasKey;

    // the layers you can collide 
    public LayerMask moveLayerMask;

    private SpriteRenderer sprite;
    
    private void Move (Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.0f, moveLayerMask);

        if (hit.collider == null)
        {
            transform.position += new Vector3(dir.x, dir.y, 0);
        }else
        {
            Debug.Log("HITT");
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

    }

    public void OnAttackLeft(InputAction.CallbackContext context)
    {

    }

    public void OnAttackRight(InputAction.CallbackContext context)
    {

    }

    public void OnAttackDown(InputAction.CallbackContext context)
    {

    }
}
