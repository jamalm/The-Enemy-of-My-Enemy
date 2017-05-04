using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {
    public PredBrain brain;
	// Use this for initialization
	void Start () {
        brain = GetComponent<PredBrain>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Run()
    {   
        if (Vector3.Distance(brain.food.transform.position, transform.position) <3)
        {
            gameObject.GetComponent<Pursue>().enabled = false;
            brain.food.GetComponent<PreyBrain>().Die();
            brain.FindFood();
            brain.PopState();
            brain.hunger = 0;
            
        }
        else if (brain.energy < 1)
        {
            gameObject.GetComponent<Pursue>().enabled = false;
            brain.PopState();
            brain.PushState(2);
        }
        else if(brain.hunger > 60)
        {
            gameObject.GetComponent<Pursue>().enabled = false;
            brain.PopState();
            brain.PushState(2);
        }
        else
        {
            GetComponent<Pursue>().enabled = true;
            GetComponent<Boid>().maxspeed = 20;
            brain.energy -= 10f * Time.deltaTime;
            gameObject.GetComponent<Pursue>().targetObj = brain.food.GetComponent<Boid>();
        }
    }
}
