using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : SteeringBehaviour {
    public Boid targetObj;
    public Vector3 target;

	// Use this for initialization
	void Start () {
		if(targetObj != null)
        {
            target = targetObj.transform.position;
        }
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
        float distance = Vector3.Distance(target, transform.position);
        float time = distance / maxspeed;

        Vector3 desired = target + (time * targetObj.velocity);
        return boid.SeekForce(desired);
    }
}
