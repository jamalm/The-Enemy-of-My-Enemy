using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatCounter : MonoBehaviour {
    public Text[] texts;
    public string[] titles;
    public FPSController player;
	// Use this for initialization
	void Start () {
        /* Order of texts indexes
         * 0. hunger
         * 1. energy
         * 2. Spheres
         */
        texts = GetComponentsInChildren<Text>();
        titles = new string[3];
        titles[0] = "Predator Hunger: ";
        titles[1] = "Predator Energy: ";
        titles[2] = "Energy Spheres: ";
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTexts();
	}

    void UpdateTexts()
    {
        if(player.leader != null)
        {
            texts[0].text = titles[0] + player.leader.GetComponent<PredBrain>().hunger + "/" + player.leader.GetComponent<PredBrain>().threshold;
            texts[1].text = titles[1] + (int)player.leader.GetComponent<PredBrain>().energy ;
            
        }
        else
        {
            texts[0].text = titles[0];
            texts[1].text = titles[1];
        }
        texts[2].text = titles[2] + player.GetComponent<FPSController>().Spheres;
    }
}
