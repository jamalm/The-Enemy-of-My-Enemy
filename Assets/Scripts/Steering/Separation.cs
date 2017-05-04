using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : SteeringBehaviour {

    public Boid[] allies;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (maxspeed != boid.maxspeed)
            maxspeed = boid.maxspeed;
    }

    public override Vector3 Calculate()
    {
        Vector3 turnForce = Vector3.zero;
        if(allies != null)
        {
            foreach (Boid other in allies)
            {
                if(other != null)
                {
                    if (other != boid)
                    {
                        Vector3 toEntity = boid.transform.position - other.transform.position;
                        turnForce += (Vector3.Normalize(toEntity) / toEntity.magnitude);
                    }
                }

            }
        }

        return turnForce;
    }

}
