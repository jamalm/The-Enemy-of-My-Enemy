using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureCounter : MonoBehaviour {
    public Text[] texts;
    public string[] titles;
	// Use this for initialization
	void Start () {
        /* Order of texts indexes
         * 0. predators
         * 1. planktons
         * 2. prey
         */
        texts = GetComponentsInChildren<Text>();
        titles = new string[3];
        titles[0] = "Predators left: ";
        titles[1] = "Plankton left: ";
        titles[2] = "Prey Left: ";
    }

    // Update is called once per frame
    void Update () {
        UpdateTexts();
	}

    void UpdateTexts()
    {
        texts[0].text = titles[0] + SpawnEngine.engine.predators.Count;
        texts[1].text = titles[1] + SpawnEngine.engine.planks.Count;
        texts[2].text = titles[2] + SpawnEngine.engine.prey.Count;
    }
}
