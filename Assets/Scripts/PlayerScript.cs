using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //[Range(0, 10)]
    public float jump_height = 2.75f;

    private GameObject score_manager;
    private ScoreManager score_manager_script;

    private Animator animator;
    private Rigidbody2D rb;

    private PlayerControls controls;

    private GameObject post_game_ui;
    private UIScript post_game_ui_script;

    private void Awake()
    {
        score_manager = GameObject.FindGameObjectWithTag("ScoreManager");
        score_manager_script = score_manager.GetComponent<ScoreManager>();
        animator = transform.parent.gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        post_game_ui = GameObject.FindGameObjectWithTag("Canvas");
        post_game_ui_script = post_game_ui.GetComponent<UIScript>();

        initControls();
    }
    private void Start()
    {
        
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
        if (collision.tag == "Pipe" || collision.tag == "DestroyTriggerPlayer")
        {
            Death();
        }
        else if(collision.tag == "PipeScoreTrigger")
        {
            print("Colliding with pipescoretrigger");
            score_manager_script.setScore(score_manager_script.getScore() + 1);
        }
    }

    private void Death()
    {
        animator.SetTrigger("DeathTrigger");
        controls.Disable();
        post_game_ui_script.PostGameMenu();


    }
}
