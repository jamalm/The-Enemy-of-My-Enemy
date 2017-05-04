using UnityEngine;
using System.Collections;

public class Graze : MonoBehaviour
{
    PreyBrain brain;
    // Use this for initialization
    void Start()
    {
        brain = GetComponent<PreyBrain>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Run()
    {
        
        if(brain.hunger > brain.threshold)
        {
            brain.PopState();
            GetComponent<Wander>().enabled = false;
            brain.PushState(1);
        }
        else if(brain.predator != null)
        {
            if (Vector3.Distance(transform.position, brain.predator.transform.position) < brain.safeDistance && !brain.predator.GetComponent<PredBrain>().isSleeping())
            {
                gameObject.GetComponent<Wander>().enabled = false;
                brain.PushState(0);
            }
        }
        else
        {
            GetComponent<Wander>().enabled = true;
            GetComponent<Boid>().maxspeed = 5;
        }
    }
}
