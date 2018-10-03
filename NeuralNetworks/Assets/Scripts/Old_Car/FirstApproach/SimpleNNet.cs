using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNNet : MonoBehaviour {

    /*Perceptron per;
    Point[] points;
    GameObject[] visualPoints;

    GameObject n_line;
    GameObject perceptron_line;

    [Header("Draw variables")]
    public GameObject marcador;
    public GameObject Line;
    public Material correct;
    public Material incorect;
    public GameObject visualContainer;

    // Use this for initialization
    void Start () {

        per = new Perceptron(3);
        points =new Point[200];

        //Visual feedback
        visualPoints = new GameObject[200];
        n_line = Instantiate(Line, Vector3.zero, Quaternion.identity, visualContainer.transform);
        n_line.GetComponent<LineRenderer>().material.color = Color.magenta;
        n_line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(-100, -100));
        n_line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(100, 100));

        for (int i=0;i<points.Length;i++)
        {
            points[i] = new Point();

            //Visual feed-back
            visualPoints[i] = Instantiate(marcador, new Vector3(points[i].x, points[i].y, 0), Quaternion.identity,visualContainer.transform);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < points.Length; i++)
            {
                float[] inp = new float[3] { points[i].x, points[i].y, points[i].bias };
                int error = per.Train(inp, points[i].label);
                
                // Visual feedback
                if (error != 0) 
                    visualPoints[i].GetComponent<Renderer>().material = incorect;
                else
                    visualPoints[i].GetComponent<Renderer>().material = correct;
            }
        }
    }*/
}
