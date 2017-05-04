using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankBrain : FSM {
    public enum State
    {
        Follow,
        Disperse,
        Lost

    }
    public Stack<State> stack = new Stack<State>();
    bool moodUpdated;
    public GameObject[] allies;
    public GameObject leader;
    public State CurrentMood;
    Follow follow;
    Disperse disperse;
    Lost lost;

    public void FindLeader()
    {
        if(leader == null || leader.GetComponent<PredBrain>().isSleeping())
        {
            GameObject[] templeaders = GameObject.FindGameObjectsWithTag("Predator");
            if(templeaders.Length == 0)
            {
                //do nothing, push lost
                PushState(2);
            }
            
            else
            {
                int indexer = UnityEngine.Random.Range(0, templeaders.Length);
                leader = templeaders[indexer];
            }
        }

    }


    public override void PopState()
    {
        moodUpdated = false;
        stack.Pop();
    }

    public override void PushState(int newState)
    {
        moodUpdated = false;
        stack.Push((State)newState);
    }
    public State GetState()
    {
        return stack.Peek();
    }


    public override void UpdateState()
    {
        if(stack.Count.Equals(0))
        {
            PushState(0);
        }
        switch(GetState())
        {
            case State.Follow:
                {
                    MoodChange(Color.green);
                    follow.Run();
                    break;
                }
            case State.Disperse:
                {
                    MoodChange(Color.black);
                    disperse.Run();
                    break;
                }
            case State.Lost:
                {
                    MoodChange(Color.yellow);
                    lost.Run();
                    break;
                }
        }
        CurrentMood = GetState();
    }

    // Use this for initialization
    void Start () {
        disperse = GetComponent<Disperse>();
        follow = GetComponent<Follow>();
        lost = GetComponent<Lost>();
	}

    // Update is called once per frame
    void Update() {
        FindLeader();
        UpdateState();
    }

    public override void Die()
    {
        SpawnEngine.engine.planks.Remove(gameObject);
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

