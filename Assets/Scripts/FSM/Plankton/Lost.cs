using UnityEngine;
using System.Collections;

public class Lost : MonoBehaviour
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
        if(brain.leader != null && !brain.leader.GetComponent<PredBrain>().isSleeping())
        {
            brain.PopState();
        }
        else
        {
            GetComponent<Wander>().enabled = true;
        }
    }
}
