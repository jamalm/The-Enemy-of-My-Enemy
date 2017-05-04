using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalk : MonoBehaviour {

    PredBrain brain;
	// Use this for initialization
	void Start () {
        brain = GetComponent<PredBrain>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Run()
    {
        if(brain.hunger > brain.threshold || Vector3.Distance(transform.position, brain.food.transform.position) < 5)
        {
            gameObject.GetComponent<Arrive>().enabled = false;
            brain.PushState(1);
        }
        else if(brain.energy < 1)
        {
            gameObject.GetComponent<Arrive>().enabled = false;
            brain.PopState();
            brain.PushState(2);
        }
        else
        {
            GetComponent<Boid>().maxspeed = 3;
            GetComponent<Arrive>().enabled = true;
            GetComponent<Arrive>().targetObj = brain.food;
        }
    }
}
