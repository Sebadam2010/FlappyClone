using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //[Range(0, 10)]
    public float jump_height = 2.75f;

    private float degrees_of_rotation = 5f;
    private Vector3 v3ToTop = new Vector3(0, 0, 35);
    private Vector3 v3ToBottom = new Vector3(0, 0, -35);
    private Vector3 v3Current = new Vector3(0, 0, 0);

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
        v3Current = transform.eulerAngles;
    }

    private void Update()
    {
        if(rb.velocity.y < 0 )
        {
            v3Current = new Vector3(
            Mathf.LerpAngle(v3Current.x, v3ToBottom.x, 0.085f),
            Mathf.LerpAngle(v3Current.y, v3ToBottom.y, 0.085f),
            Mathf.LerpAngle(v3Current.z, v3ToBottom.z, 0.085f));
            transform.eulerAngles = v3Current;

        } 
        else
        {
            v3Current = new Vector3(
            Mathf.LerpAngle(v3Current.x, v3ToTop.x, 0.085f),
            Mathf.LerpAngle(v3Current.y, v3ToTop.y, 0.085f),
            Mathf.LerpAngle(v3Current.z, v3ToTop.z, 0.085f));
            transform.eulerAngles = v3Current;
        }

        

        


        //transform.rotation = Quaternion.Euler(Vector3.forward * -degrees_of_rotation);
    }

    private void FixedUpdate()
    {
        //transform.Rotate(0, 0, -50 * Time.deltaTime);
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
