using UnityEngine;
using System.Collections;

public class EnergyCapture : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<FPSController>())
        {
            other.gameObject.GetComponent<FPSController>().Spheres++;
            Destroy(gameObject);
        }
    }
}
