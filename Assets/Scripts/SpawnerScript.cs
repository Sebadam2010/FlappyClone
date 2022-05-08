using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject pipe_prefab;
    public float object_speed = 2.5f;

    public float spawn_delay = 2f;

    private GameObject upper_midpoint_bound;
    private GameObject lower_midpoint_bound;

    private Vector3 random_position;

    private GameObject pipe_gameObject;
    private PipeScript pipe_script;

    private void Awake()
    {
        upper_midpoint_bound = GameObject.FindGameObjectWithTag("Upper_Midpoint_Boundary");
        lower_midpoint_bound = GameObject.FindGameObjectWithTag("Lower_Midpoint_Boundary");
    }

    private void Start()
    {
        StartCoroutine("spawnPipe");
    }

    private void FixedUpdate()
    {
        //if (spawn_pipe)
            
    }

    private IEnumerator spawnPipe()
    {
        random_position = new Vector3(transform.position.x, Random.Range(lower_midpoint_bound.transform.position.y, upper_midpoint_bound.transform.position.y), 0);
        
        pipe_gameObject = Instantiate(pipe_prefab, random_position, Quaternion.identity);
        pipe_script = pipe_gameObject.GetComponent<PipeScript>();
        pipe_script.setSpeed(object_speed);

        yield return new WaitForSeconds(spawn_delay);

        StartCoroutine("spawnPipe");

    }

}
