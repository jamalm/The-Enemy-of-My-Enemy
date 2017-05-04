using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEngine : MonoBehaviour {

    public static SpawnEngine engine;
    public GameObject Predator;
    public GameObject Prey;
    public GameObject Plankton;
    public GameObject EnergySphere;

    public List<GameObject> planks;
    public List<GameObject> prey;
    public List<GameObject> predators;

    public int NumberOfPredators = 2;
    public int NumberOfPrey = 10;
    public int NumberOfPlankton = 100;
    public int NumberOfSpheres = 5;
    public float PredatorSpawnRadius = 200;
    public float PreySpawnRadius = 300;
    public float PlanktonSpawnRadius = 50;
    public float SphereSpawnRadius = 50;
    Random rand = new Random();

    private void Awake()
    {
        engine = this;
    }

	// Use this for initialization
	void Start () {
        planks = new List<GameObject>();
        prey = new List<GameObject>();
        predators = new List<GameObject>();
        SpawnPredators();
        SpawnPrey();
        SpawnPlankton();
        SpawnSpheres();
	}
	
	// Update is called once per frame
	void Update () {
        WinCondition();
	}

    void SpawnPredators()
    {
        for(int i=0;i<NumberOfPredators;i++)
        {
            predators.Add(Instantiate(Predator, new Vector3(Random.Range(-PredatorSpawnRadius, PredatorSpawnRadius), Random.Range(-PredatorSpawnRadius, PredatorSpawnRadius), Random.Range(-PredatorSpawnRadius, PredatorSpawnRadius)), Quaternion.identity));
        }
    }

    void SpawnPrey()
    {
        for(int i=0;i<NumberOfPrey;i++)
        {
            prey.Add(Instantiate(Prey, new Vector3(Random.Range(-PreySpawnRadius, PreySpawnRadius), Random.Range(-PreySpawnRadius, PreySpawnRadius), Random.Range(-PreySpawnRadius, PreySpawnRadius)), Quaternion.identity));
        }
    }
    void SpawnPlankton()
    {
        for(int i=0;i<NumberOfPlankton;i++)
        {
            planks.Add(Instantiate(Plankton, new Vector3(Random.Range(-PlanktonSpawnRadius, PlanktonSpawnRadius), Random.Range(-PlanktonSpawnRadius, PlanktonSpawnRadius), Random.Range(-PlanktonSpawnRadius, PlanktonSpawnRadius)), Quaternion.identity));
        }
    }

    void WinCondition()
    {
        if(planks.Count == 0 || prey.Count == 0)
        {
            Debug.Log("YOU WIN!");
            Time.timeScale = 0.0f;
        }
    }

    void SpawnSpheres()
    {
        for(int i=0;i<NumberOfSpheres;i++)
        {
            Instantiate(EnergySphere, new Vector3(Random.Range(-SphereSpawnRadius, SphereSpawnRadius), Random.Range(-SphereSpawnRadius, SphereSpawnRadius), Random.Range(-SphereSpawnRadius, SphereSpawnRadius)), Quaternion.identity);
        }
    }
}