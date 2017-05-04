using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour {
    public GameObject targetObj;
    Vector3 target;

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
        {
            target = targetObj.transform.position;
        }
	}

    public override Vector3 Calculate()
    {
        return boid.SeekForce(target);
    }
}
