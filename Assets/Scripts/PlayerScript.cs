using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //[Range(0, 10)]
    public float jump_height = 4;

    private Animator animator;
    private Rigidbody2D rb;

    private PlayerControls controls;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        initControls();
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(Vector2.up * jump_height, ForceMode2D.Impulse);

        animator.SetTrigger("FlapTrigger");
    }

    private void initControls()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Player.Jump.performed += Jump;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pipe")
        {
            Death();
        }
    }

    private void Death()
    {
        controls.Disable();
    }
}
