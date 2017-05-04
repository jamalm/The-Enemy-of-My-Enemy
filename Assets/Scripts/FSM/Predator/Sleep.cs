using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour {

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
        if (brain.hunger < 60)
            StartCoroutine(brain.Rest());
        else
            StartCoroutine(brain.Dead());
    }
}
