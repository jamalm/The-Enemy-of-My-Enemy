using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
    public Vector3 force = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public float mass = 1;
    public float maxspeed = 5.0f;


    SteeringBehaviour[] behaviours;
	// Use this for initialization
	void Start () {
        behaviours = GetComponents<SteeringBehaviour>();
        
	}
	
	// Update is called once per frame
	void Update () {
        //get force
        force = CalculateForces();
        //set up new accelration
        Vector3 newAcc = force / mass;
        //calc smoothing rate by clamping
        float smoothRate = Mathf.Clamp(9.0f * Time.deltaTime, 0.15f, 0.4f) / 2.0f;
        //set acceleration to new acceleration smmothly
        acceleration = Vector3.Lerp(acceleration, newAcc, smoothRate);

        //calc velocity
        velocity += acceleration * Time.deltaTime;
        //clamp velocity by maxspeed
        velocity = Vector3.ClampMagnitude(velocity, maxspeed);
        //for banking correctly
        Vector3 globalUp = new Vector3(0, 0.2f, 0);
        Vector3 accelUp = acceleration * 0.05f;
        Vector3 bankUp = accelUp + globalUp;
        smoothRate = Time.deltaTime * 3.0f;
        Vector3 tempUp = transform.up;
        tempUp = Vector3.Lerp(tempUp, bankUp, smoothRate);
        //damping the velocity
        if(velocity.magnitude > 0.0001f)
        {
            transform.forward = velocity;
            transform.forward.Normalize();
            transform.LookAt(transform.position + transform.forward, tempUp);
            velocity *= 0.99f;
        }
        transform.position += velocity * Time.deltaTime;
	}
    Vector3 CalculateForces()
    {
        force = Vector3.zero;

        foreach(SteeringBehaviour b in behaviours)
        {
            if(b.isActiveAndEnabled)
            {
                force += b.Calculate();
            }
        }
        return force;
    }

    public Vector3 SeekForce(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= maxspeed;
        return desired - velocity;
    }
}
