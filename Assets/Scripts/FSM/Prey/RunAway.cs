using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : MonoBehaviour {
    
    PreyBrain brain;
	// Use this for initialization
	void Start () {
        brain = GetComponent<PreyBrain>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //checks for escape condition
    public void Run()
    {

        //if the predator is further than safe distance OR if the predator is asleep
        if(Vector3.Distance(brain.predator.transform.position, transform.position) > brain.safeDistance || brain.predator.GetComponent<PredBrain>().isSleeping())
        {
            //disable fleeing
            gameObject.GetComponent<Flee>().enabled = false;
            brain.PopState();
        }
        //always enable flee otherwise
        else 
        {
            gameObject.GetComponent<Flee>().enabled = true;
            GetComponent<Flee>().targetObj = brain.predator;
            GetComponent<Boid>().maxspeed = 10;
        }
    }
}
