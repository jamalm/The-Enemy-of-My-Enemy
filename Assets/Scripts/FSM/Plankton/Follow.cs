using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    PlankBrain brain;
    // Use this for initialization
    void Start()
    {
        brain = GetComponent<PlankBrain>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Run()
    {
        if(brain.leader == null || brain.leader.GetComponent<PredBrain>().isSleeping())
        {
            //disperse
            brain.PushState(1);
            // run away from current position
            GetComponent<Disperse>().target = transform.position;
            GetComponent<Arrive>().enabled = false;
        }
        else
        {
            GetComponent<Arrive>().enabled = true;
            GetComponent<Arrive>().targetObj = brain.leader;
        }
    }
}
