using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecover : MonoBehaviour {

    public float time_stoped;
    public float max_time_alive;
    float max_time_stoped;
    float min_velocity;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        max_time_stoped = 2.0f;
        min_velocity = 0.1f;
        max_time_alive = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {

        max_time_alive -= Time.deltaTime;

        if(rb.velocity.magnitude < min_velocity)
        {
            time_stoped += Time.deltaTime;
        }
        else
        {
            time_stoped = 0.0f;
        }

        if (time_stoped >= max_time_stoped || max_time_alive <= 0.0f )
        {
            GameObject.Find("Creator").GetComponent<GeneticProcess>().NextElementInGeneration();
        }


    }
}
