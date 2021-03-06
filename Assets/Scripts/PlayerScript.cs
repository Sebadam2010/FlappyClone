using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //[Range(0, 10)]
    //public float jump_height = 2.75f;
    public float jump_height = 5.5f;
    private bool jump = false;

    private Vector3 v3ToTop = new Vector3(0, 0, 35);
    private Vector3 v3ToBottom = new Vector3(0, 0, -35);
    private Vector3 v3Current = new Vector3(0, 0, 0);

    private GameObject score_manager;
    private ScoreManager score_manager_script;

    private GameObject parent;
    private Animator animator;
    private Rigidbody2D rb;

    private float player_width;
    private float player_height;

    private PlayerControls controls;

    private GameObject post_game_ui;
    private UIScript post_game_ui_script;

    //Camera Settings
    private Camera camera;

    //Audio
    private AudioSource audio_source;

    //Tutorial 
    public bool tutorial_ongoing = true;

    //UI
    private UIScript ui_script;

    //Pipe Spawner
    private SpawnerScript spawner_script;



    private void Awake()
    {
        parent = transform.parent.gameObject;

        score_manager = GameObject.FindGameObjectWithTag("ScoreManager");
        score_manager_script = score_manager.GetComponent<ScoreManager>();
        animator = transform.parent.gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        post_game_ui = GameObject.FindGameObjectWithTag("Canvas");
        post_game_ui_script = post_game_ui.GetComponent<UIScript>();

        player_height = GetComponent<SpriteRenderer>().bounds.size.y;
        player_width = GetComponent<SpriteRenderer>().bounds.size.x;

        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        audio_source = GetComponent<AudioSource>();



        initControls();
    }
    private void Start()
    {


        v3Current = transform.eulerAngles;

        Vector3 world_point_top_left = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height/2, 5));
         world_point_top_left.x += player_width / 2;

        parent.SetActive(false);
        parent.transform.position = world_point_top_left;
        parent.SetActive(true); // issue with rb causes me to have to do this (inactive then active)

        ui_script = GameObject.FindWithTag("Canvas").GetComponent<UIScript>();
        spawner_script = GameObject.FindWithTag("Spawner").GetComponent<SpawnerScript>();


    }

    private void Update()
    {
        
            

            if (rb.velocity.y < 0)
            {
                v3Current = new Vector3(
                Mathf.LerpAngle(v3Current.x, v3ToBottom.x, 0.085f),
                Mathf.LerpAngle(v3Current.y, v3ToBottom.y, 0.085f),
                Mathf.LerpAngle(v3Current.z, v3ToBottom.z, 0.085f));
                transform.eulerAngles = v3Current;

            }
            else if (rb.velocity.y > 0)
            {
                v3Current = new Vector3(
                Mathf.LerpAngle(v3Current.x, v3ToTop.x, 0.085f),
                Mathf.LerpAngle(v3Current.y, v3ToTop.y, 0.085f),
                Mathf.LerpAngle(v3Current.z, v3ToTop.z, 0.085f));
                transform.eulerAngles = v3Current;
            }
        
        

        
    }

    private void FixedUpdate()
    {

        if (tutorial_ongoing)
        {
            rb.velocity = new Vector2(0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
            
        if (jump)
        {
            if (tutorial_ongoing)
            {
                ui_script.toggleTutorialImage(false);
                rb.constraints = RigidbodyConstraints2D.None;
                spawner_script.StartCoroutine("spawnPipe");
                rb.velocity = new Vector2(0, jump_height);
                tutorial_ongoing = false;
            }

            rb.velocity = new Vector2(0, jump_height);
            audio_source.PlayOneShot(audio_source.clip, 0.2f);
            //rb.AddForce(Vector2.up * jump_height, ForceMode2D.Impulse);
            jump = false;
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        jump = true;
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
