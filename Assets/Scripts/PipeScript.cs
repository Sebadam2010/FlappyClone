using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public float speed = 0.5f;

    private AudioSource audio_source;

    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        transform.position -= transform.right * speed * Time.deltaTime;
    }

    public void setSpeed(float speed_)
    {
        speed = speed_;
    }

    public float getSpeed()
    {
        return speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //For performance
        
         if (collision.tag == "Player")
        {
            audio_source.Play();
        }
        else if (collision.tag == "DestroyTrigger" || collision.tag == "DestroyTriggerTutorial")
        {
            Destroy(gameObject);
        }
    }

}
