using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : MonoBehaviour {

    PreyBrain brain;
	// Use this for initialization
	void Start () {
        brain = GetComponent<PreyBrain>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Run()
    {
        if(brain.predator != null)
        {
            
            //flee if the predator is close and is not sleeping
            if (Vector3.Distance(transform.position, brain.predator.transform.position) < brain.safeDistance && !brain.predator.GetComponent<PredBrain>().isSleeping())
            {
                gameObject.GetComponent<Seek>().enabled = false;
                brain.PushState(0);
            }
            //if food is found
            else if (Vector3.Distance(brain.food.transform.position, transform.position) < 1)
            {
                gameObject.GetComponent<Seek>().enabled = false;
                if (brain.food.GetComponent<FPSController>())
                {
                    brain.food.SendMessage("Die");
                }
                else
                {
                    brain.food.GetComponent<PlankBrain>().Die();
                }
                brain.FindFood();
                brain.PopState();
                brain.PushState(2);
                brain.hunger = 0;
            }
            else
            {
                gameObject.GetComponent<Seek>().enabled = true;
                gameObject.GetComponent<Seek>().targetObj = brain.food;
                GetComponent<Boid>().maxspeed = 10;
                if (brain.hunger > brain.threshold)
                {
                    GetComponent<Boid>().maxspeed = 12;
                }
            }
        }
        //if food is found
        else if (Vector3.Distance(brain.food.transform.position, transform.position) < 2)
        {
            gameObject.GetComponent<Seek>().enabled = false;
            if(brain.food.GetComponent<FPSController>())
            {
                brain.food.SendMessage("Die");
            }else
            {
                brain.food.GetComponent<PlankBrain>().Die();
            }
            brain.FindFood();
            brain.PopState();
            brain.PushState(2);
            brain.hunger = 0;
        }
        else
        {
            gameObject.GetComponent<Seek>().enabled = true;
            gameObject.GetComponent<Seek>().targetObj = brain.food;
            GetComponent<Boid>().maxspeed = 10;
            if(brain.hunger > brain.threshold)
            {
                GetComponent<Boid>().maxspeed = 12;
            }
        }
    }
}
