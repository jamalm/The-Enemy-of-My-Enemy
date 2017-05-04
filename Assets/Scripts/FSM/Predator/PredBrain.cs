using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredBrain : FSM {

    public GameObject food;

    public float hunger = 0.0f;//level of hunger
    public float threshold = 100.0f; //threshold for hunger
    public float energy = 100.0f;
    public bool asleep = false;
    public GameObject[] allies;
    public State CurrentMood;

    Chase chase;
    Stalk stalk;
    Sleep sleep;
    public enum State
    {
        Stalk,
        Chase,
        Tired, 
        Default
    }
    bool moodUpdated = false;
    public Stack<State> stack = new Stack<State>();
    
    public override void UpdateState()
    {
        if(stack.Count.Equals(0))
        {
            PushState(0);
        }
        switch(GetState())
        {
            case State.Stalk:
                {
                    MoodChange(Color.green);
                    stalk.Run();
                    break;
                }
            case State.Chase:
                {
                    MoodChange(Color.yellow);
                    chase.Run();
                    break;
                }
            case State.Tired:
                {
                    if(!asleep)
                    {
                        MoodChange(Color.blue);
                        sleep.Run();
                        asleep = true;
                    }
                    break;
                }
        }
        
    }

    // Use this for initialization
    void Start () {
        stalk = GetComponent<Stalk>();
        chase = GetComponent<Chase>();
        sleep = GetComponent<Sleep>();
        food = GameObject.FindGameObjectWithTag("Prey");
        allies = GameObject.FindGameObjectsWithTag("Predator");
        GetComponent<Separation>().allies = new Boid[allies.Length];
        for (int i = 0; i < allies.Length; i++)
        {
            GetComponent<Separation>().allies[i] = allies[i].GetComponent<Boid>();
        }
        StartCoroutine(HungerGain());
    }
	
	// Update is called once per frame
	void Update () {
        FindFood();
        UpdateState();
        CurrentMood = GetState();
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

    public IEnumerator HungerGain()
    {
        while(true)
        {
            hunger++;
            yield return new WaitForSeconds(1);
        }
    }
    public IEnumerator Rest()
    {
        while(energy < 99)
        {
            energy += 5;
            yield return new WaitForSeconds(1);
        }
        
        PopState();
        asleep = false;
        PushState(0);
    }
    public IEnumerator Dead()
    {
        Die();
        while(true)
        {
            yield return new WaitForSeconds(1);
        }
    }

    public void FindFood()
    {
        if(food == null)
        {
            food = GameObject.FindGameObjectWithTag("Prey");
        }
        GameObject chosenPrey = food;
        GameObject[] prey = GameObject.FindGameObjectsWithTag("Prey");
        for(int i=0;i<prey.Length;i++)
        {
            if(chosenPrey != null)
            {
                if (Vector3.Distance(prey[i].transform.position, transform.position) < Vector3.Distance(chosenPrey.transform.position, transform.position))
                {
                    chosenPrey = prey[i];
                }
            }
        }
        food = chosenPrey;
    }

    public State GetState()
    {
        return stack.Peek();
    }

    public bool isSleeping()
    {
        if(stack.Count != 0)
        {
            if (stack.Peek().Equals(State.Tired))
            {
                return true;
            }
            
        }
        return false;
    }

    public override void MoodChange(Color color)
    {
        if(!moodUpdated)
        {
            moodUpdated = true;
            GetComponent<Renderer>().material.color = color;
        }

    }

    public override void Die()
    {
        GetComponent<Renderer>().material.color = Color.black;
        SpawnEngine.engine.predators.Remove(gameObject);
    }
}
