using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehaviour {
    public float fleeRange = 9.0f;
    public Vector3 target = Vector3.zero;
    public GameObject targetObj;
	// Use this for initialization
	void Start () {
        if (targetObj != null)
            target = targetObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (maxspeed != boid.maxspeed)
            maxspeed = boid.maxspeed;
        if (targetObj != null)
            target = targetObj.transform.position;
	}

    public override Vector3 Calculate()
    {
        if (Vector3.Distance(transform.position, target) < fleeRange)
        {
            Vector3 desired = transform.position - target;
            desired.Normalize();
            desired *= maxspeed;
            return desired - boid.velocity;
        }
        else return Vector3.zero;
    }
}
