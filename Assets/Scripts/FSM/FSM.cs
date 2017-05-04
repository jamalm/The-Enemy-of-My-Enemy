using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    

    public abstract void UpdateState();
    public abstract void PushState(int newState);
    public abstract void PopState();
    public abstract void Die();
    public abstract void MoodChange(Color color);
}
