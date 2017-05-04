using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyBrain : FSM {
    public GameObject food;
    public GameObject predator;
    public GameObject[] allies;
    public float safeDistance;
    
    Feed feed;
    RunAway run;
    Graze graze;
    bool moodUpdated = false;

    public float hunger = 0.0f;
    public float threshold = 10.0f;
    public float fear = 50;
    public enum State
    {
        RunAway,
        Feed,
        Graze
    };
    public State CurrentMood;
    public Stack<State> stack = new Stack<State>();
	// Use this for initialization
	void Start () {

        feed = GetComponent<Feed>();
        run = GetComponent<RunAway>();
        graze = GetComponent<Graze>();

        predator = GameObject.FindGameObjectWithTag("Predator");
        food = GameObject.FindGameObjectWithTag("Player");
        allies = GameObject.FindGameObjectsWithTag("Prey");
        GetComponent<Separation>().allies = new Boid[allies.Length];
        for (int i = 0;i<allies.Length;i++)
        {
            GetComponent<Separation>().allies[i] = allies[i].GetComponent<Boid>();
        }

        StartCoroutine(HungerGain());
	}
	
	// Update is called once per frame
	void Update () {
        SpotThreat();
        FindFood();
        TraumaticAssessment();
        UpdateState();
        allies = GameObject.FindGameObjectsWithTag("Prey");
        GetComponent<Separation>().allies = new Boid[allies.Length];
        for (int i = 0; i < allies.Length; i++)
        {
            GetComponent<Separation>().allies[i] = allies[i].GetComponent<Boid>();
        }
        if(!stack.Count.Equals(0))
        {
            CurrentMood = GetState();
        }
        
    }

    public override void UpdateState()
    {
        if (stack.Count.Equals(0))
        {
            PushState(2);
        }
        switch (GetState())
        {
            case State.RunAway:
                {
                    MoodChange(Color.white);
                    run.Run();
                    break;
                }
            case State.Feed:
                {
                    MoodChange(Color.magenta);
                    feed.Run();
                    break;
                }
            case State.Graze:
                {
                    MoodChange(Color.black);
                    graze.Run();
                    break;
                }
        }
        
    }
    public override void PushState(int newState)
    {
        moodUpdated = false;
        stack.Push((State)newState);
    }
    public override void PopState()
    {
        moodUpdated = false;
        stack.Pop();
    }

    public State GetState()
    {
        return stack.Peek();
    }
    void SpotThreat()
    {
        GameObject closestThreat;
        if (predator == null)
        {
            closestThreat = GameObject.FindGameObjectWithTag("Predator");
        }
        else
        {
            closestThreat = predator;
        }
        GameObject[] preds = GameObject.FindGameObjectsWithTag("Predator");
        for (int i = 0; i < preds.Length; i++)
        {
            if (Vector3.Distance(preds[i].transform.position, transform.position) < Vector3.Distance(closestThreat.transform.position, transform.position))
            {
                closestThreat = preds[i];
            }
        }
        predator = closestThreat;
    }


    public void FindFood()
    {
        if (food == null)
        {
            food = GameObject.FindGameObjectWithTag("Player");
        }
        GameObject chosenPrey = food;
        GameObject[] prey = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < prey.Length; i++)
        {
            if (chosenPrey != null)
            {
                if (Vector3.Distance(prey[i].transform.position, transform.position) < Vector3.Distance(chosenPrey.transform.position, transform.position))
                {
                    chosenPrey = prey[i];
                }
            }
        }
        food = chosenPrey;
    }

    void TraumaticAssessment()
    {
        if(predator != null)
        {
            //if the predator starts chasing prey, get scared
            if (predator.GetComponent<PredBrain>().CurrentMood == PredBrain.State.Chase)
            {
                fear = 100;
            }
            else
            {
                fear = 50;
            }
        }
        //minimum safe distance is 5
        if (safeDistance > 5)
            safeDistance = (fear - hunger);
        //set the flee range to be the safedistance
        GetComponent<Flee>().fleeRange = safeDistance;
    }

    IEnumerator HungerGain()
    {
        while (true)
        {
            hunger++;
            yield return new WaitForSeconds(1);
        }
    }

    public override void Die()
    {
        SpawnEngine.engine.prey.Remove(gameObject);
        Destroy(gameObject);
    }

    public override void MoodChange(Color color)
    {
        if (!moodUpdated)
        {
            moodUpdated = true;
            GetComponent<Renderer>().material.color = color;
        }

    }
}
