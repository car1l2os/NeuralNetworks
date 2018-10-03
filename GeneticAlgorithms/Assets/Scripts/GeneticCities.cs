using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticCities : MonoBehaviour {

    private GameObject[] cities;
    private int[][] population;
    private int numberOfCities = 10;
    private int elements_per_population = 10;

    private int[] orden;
    private float[] fitness;

    private float bestDistance = float.PositiveInfinity;
    private int[] bestPath;


    //Visual
    private GameObject visualContainer;
    [Header("Prefabs")]
    public GameObject city;
    public GameObject path;

    [Header("Materials")]
    public Material best;
    public Material regular;

    // Use this for initialization
    void Start () {

        //Creation
        visualContainer = GameObject.Find("VisualContainer");
        cities = new GameObject[numberOfCities];
        population = new int[elements_per_population][];
        orden = new int[numberOfCities];
        bestPath = new int[numberOfCities];
        fitness = new float[elements_per_population];

        CreateCities();

        for(int i=0;i<elements_per_population;i++)
        {
            population[i] = (int[]) orden.Clone();
            Mix(population[i], 100);
        }

        CalculateFitness();
        NormalizeFitness();
        DrawBest();


    }

    // Update is called once per frame
    void Update () {

        CalculateFitness();
        NormalizeFitness();
        CreateNewGeneration();
        DrawBest();

	}



    float CalcularDistancia(GameObject[] list, int[] orden)
    {
        float sum = 0.0f;
        for(int i=0;i<list.Length-1;i++)
        {
            int ind1 = orden[i];
            GameObject city1 = cities[ind1];

            int ind2 = orden[i+1];
            GameObject city2 = cities[ind2];

            float distance = Vector3.Distance(city1.transform.position, city2.transform.position);
            sum += distance;
        }
        return sum;
    }
    private void CreateCities()
    {
        for (int i = 0; i < cities.Length; i++)
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(-100.0f, 100.0f), UnityEngine.Random.Range(-100.0f, 100.0f), 0);
            GameObject c = Instantiate(city, position, Quaternion.identity, visualContainer.transform);
            cities[i] = c;
            orden[i] = i;
        }
    }
    private void Mix(int[] vector, int times)
    {
        for(int i=0;i<times;i++)
        {
            int ind1 = UnityEngine.Random.Range(0, vector.Length); //Ultimo posible?
            int ind2 = UnityEngine.Random.Range(0, vector.Length);
            int aux = vector[ind1];
            vector[ind1] = vector[ind2];
            vector[ind2] = aux;
        }
    }
    private void CalculateFitness()
    {
        for (int i = 0; i < population.Length; i++)
        {
            float distance = CalcularDistancia(cities, population[i]);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestPath = population[i];
            }
            fitness[i] = 1.0f / (distance+1.0f); //+1.0 para asegurar que nunca es 0 
        }
    }
    private void NormalizeFitness()
    {
        float sum = 0.0f;
        for (int i = 0; i < fitness.Length; i++)
        {
            sum += fitness[i];
        }
        for (int i = 0; i < fitness.Length; i++)
        {
            fitness[i] = fitness[i] / sum;
        }
    }
    private void CreateNewGeneration()
    {
        int[][] n_population = new int[elements_per_population][];
        for (int i = 0; i < population.Length; i++)
        {
            int[] order = PickWithProb(population,fitness);
            order = Mutate(order,0.2f);
            n_population[i] = order;
        }
        population = n_population;
    }
    private int[] PickWithProb(int[][] vector, float[] probabilidades)
    {
        int idx = 0;
        float random = UnityEngine.Random.Range(0.0f,1.0f);

        while(random > 0.0f)
        {
            random = random - probabilidades[idx];
            idx += 1;
        }
        idx -= 1;
        return (int[]) vector[idx].Clone();
    }
    private int[] Mutate(int[] original, float mutationProb)
    {
        int[] to_return = (int[])original.Clone();

        for(int i=0;i<to_return.Length;i++)
        {
            if(UnityEngine.Random.Range(0.0f,1.0f) < mutationProb)
            {
                int ind1 = UnityEngine.Random.Range(0, to_return.Length);
                int ind2 = UnityEngine.Random.Range(0, to_return.Length);
                int aux = to_return[ind1];
                to_return[ind1] = to_return[ind2];
                to_return[ind2] = aux;
            }
        }

        return to_return;
    }
    private void DrawBest()
    {
        
        GameObject[] to_destroy = GameObject.FindGameObjectsWithTag("Path");
        for (int i = to_destroy.Length-1; i > -1; i--)
            Destroy(to_destroy[i]);


        for (int i = 0; i < bestPath.Length - 1; i++)
        {
            GameObject l = Instantiate(path, Vector3.zero, Quaternion.identity, visualContainer.transform);
            l.GetComponent<LineRenderer>().SetPosition(0, cities[bestPath[i]].transform.position);
            l.GetComponent<LineRenderer>().SetPosition(1, cities[bestPath[i + 1]].transform.position);
        }
        Debug.Log(CalcularDistancia(cities, bestPath));
    }
}
