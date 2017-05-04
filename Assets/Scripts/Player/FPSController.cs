using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {
    public Camera camera;
    public float speed = 5;
    public GameObject leader;
    public int Spheres = 0;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        camera.transform.position = transform.position;
        camera.transform.rotation = transform.rotation;
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * Time.deltaTime * speed;
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * speed;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
        FindLeader();
    }

    void Die()
    {
        Time.timeScale = 0.0f;
    }

    void FindLeader()
    {

        GameObject[] templeaders = GameObject.FindGameObjectsWithTag("Predator");
        for(int i=0;i<templeaders.Length;i++)
        {
            if(leader == null)
            {
                leader = templeaders[i];
            }
            else if(Vector3.Distance(templeaders[i].transform.position, transform.position) < Vector3.Distance(leader.transform.position, transform.position))
            {
                leader = templeaders[i];
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision Detected!" + other.gameObject);
        if (other.gameObject.tag.Equals("Collectable"))
        {
            Spheres++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Predator") && Spheres > 0)
        {
            other.gameObject.GetComponent<PredBrain>().energy += 50;
            Spheres--;
        }
    }
}
