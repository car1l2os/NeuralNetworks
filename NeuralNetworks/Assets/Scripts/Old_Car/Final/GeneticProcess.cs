using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticProcess : MonoBehaviour {

    int elements_per_generation = 10;
    int how_many_generations;
    float[] generation_results;

    GameObject[] actual_generation;
    GameObject element_active;
    int index_element_active;
    GameObject goal;


    [Header("GameObjects")]
    public GameObject element;

	// Use this for initialization
	void Start () {

        generation_results = new float[elements_per_generation];
        actual_generation = new GameObject[elements_per_generation];
        goal = GameObject.Find("GoalPoint");

        for(int i=0;i<elements_per_generation;i++)
        {
            actual_generation[i] = Instantiate(element, GameObject.Find("SpawnPoint").transform.position,Quaternion.identity,GameObject.Find("Generation").transform);
            actual_generation[i].GetComponent<InputReceiver>().setAngles();
            actual_generation[i].GetComponent<NeuralLearning>().Create();
            index_element_active = 0;
            if (i != 0)
                actual_generation[i].SetActive(false);
        }
        element_active = actual_generation[0];
        element_active.GetComponent<NeuralLearning>().Train();
	}

    public void NextElementInGeneration()
    {
        generation_results[index_element_active] = Vector3.Distance(element_active.transform.position, goal.transform.position);
        element_active.SetActive(false);

        if (index_element_active !=  actual_generation.Length-1)
        {
            index_element_active += 1;
            element_active = actual_generation[index_element_active];
            element_active.SetActive(true);
        }
        else
        {
            this.CreateNewGeneration();
        }
        element_active.GetComponent<NeuralLearning>().Train();  
    }

    private void CreateNewGeneration()
    {
        float sum = 0f;
        foreach(float v in generation_results)
        {
            sum += v;
        }

        for(int i=0;i<generation_results.Length;i++)
        {
            generation_results[i] = generation_results[i] / sum; //makes the values in range from 0 to 1
        }

        //Choose by probability one(two) of the game objects of the previous generation.

        /*/for i in range(len(gen) * carryover_percent): //What is this?
            new_gen.append(pick(gen))*/

        //mutate some % of the new generation

    }
}
