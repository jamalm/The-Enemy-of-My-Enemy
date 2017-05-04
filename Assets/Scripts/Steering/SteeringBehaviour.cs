using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour {

    public Boid boid;
    public float maxspeed;
    private void Awake()
    {
        boid = GetComponent<Boid>();
        maxspeed = boid.maxspeed;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (maxspeed != boid.maxspeed)
            maxspeed = boid.maxspeed;
	}
    public abstract Vector3 Calculate();
}
