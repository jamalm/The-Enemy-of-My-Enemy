using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : SteeringBehaviour {

    public GameObject targetObj;
    Vector3 target;
    float slowingDistance = 50;
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
        
        Vector3 desired = target - transform.position;
        float distance = desired.magnitude;
        //5 is slowing distance
        float ramped = maxspeed * (distance / slowingDistance);
        float clamped = Mathf.Min(ramped, maxspeed);
        desired = clamped * (desired / distance);
        return desired - boid.velocity;
    }
}
