using UnityEngine;
using System.Collections;

public class Disperse : MonoBehaviour
{
    PlankBrain brain;
    public Vector3 target;
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
        //if dispersed far enough
        if(Vector3.Distance(target, transform.position) > 10)
        {
            brain.PopState();
            GetComponent<Flee>().enabled = false;
            brain.PushState(2);
        }
        else
        {
            GetComponent<Flee>().enabled = true;
        }
    }
}
