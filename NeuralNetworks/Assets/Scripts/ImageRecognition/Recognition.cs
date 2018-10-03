using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recognition : MonoBehaviour
{

    NeuralNetwork nn;

    int count = 0;

    private bool training = false;
    private bool semiCorrupted = false;

    [Header("Textures")]
    public Texture2D[] inputImages;
    public Texture2D[] semiCorruptedImages;
    public Texture2D[] corruptedImages;

    // Use this for initialization
    void Start()
    {
        nn = new NeuralNetwork(64, 16, 3, null, 0.5f, null, 0.5f);

        // 0 0 0    => 0
        // 0 0 1    => 1
        // 0 1 0    => 1
        // 0 1 1    => 0
        // 1 0 0    => 1
        // 1 0 1    => 0
        // 1 1 0    => 0
        // 1 1 1    => 1

        /*for(int i=0;i<15000;i++)
        {
            nn.train(new List<float> { 0.0f, 0.0f, 0.0f }, new List<float> { 0.0f });
            nn.train(new List<float> { 0.0f, 0.0f, 1.0f }, new List<float> { 1.0f });
            nn.train(new List<float> { 0.0f, 1.0f, 0.0f }, new List<float> { 1.0f });
            nn.train(new List<float> { 0.0f, 1.0f, 1.0f }, new List<float> { 0.0f });
            nn.train(new List<float> { 1.0f, 0.0f, 0.0f }, new List<float> { 1.0f });
            nn.train(new List<float> { 1.0f, 0.0f, 1.0f }, new List<float> { 0.0f });
            nn.train(new List<float> { 1.0f, 1.0f, 0.0f }, new List<float> { 0.0f });
            nn.train(new List<float> { 1.0f, 1.0f, 1.0f }, new List<float> { 1.0f });
        }

        Debug.Log(nn.feed_forward(new List<float> { 0.0f, 0.0f, 0.0f })[0]);
        Debug.Log(nn.feed_forward(new List<float> { 0.0f, 0.0f, 1.0f })[0]);
        Debug.Log(nn.feed_forward(new List<float> { 0.0f, 1.0f, 0.0f })[0]);
        Debug.Log(nn.feed_forward(new List<float> { 0.0f, 1.0f, 1.0f })[0]);
        Debug.Log(nn.feed_forward(new List<float> { 1.0f, 0.0f, 0.0f })[0]);
        Debug.Log(nn.feed_forward(new List<float> { 1.0f, 0.0f, 1.0f })[0]);
        Debug.Log(nn.feed_forward(new List<float> { 1.0f, 1.0f, 0.0f })[0]);
        Debug.Log(nn.feed_forward(new List<float> { 1.0f, 1.0f, 1.0f })[0]);*/


    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.T))
        {
            training = !training;
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            /*Debug.Log("CHANGED INPUT");
            semiCorrupted = !semiCorrupted;
            Debug.Log(semiCorrupted);*/
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FeedNetwork(1);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FeedNetwork(2);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            FeedNetwork(3);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            FeedNetwork(4);
        }

        if (training)
        {
            List<List<List<float>>> to_error = new List<List<List<float>>>();

            for (int k = 0; k < inputImages.Length; k++)
            {
                List<float> input = new List<float>();

                List<float> output = new List<float> { 0.0f, 0.0f, 0.0f };

                if(k==0)
                {
                    //nothing
                }
                else if(k==1)
                {
                    output[2] = 1.0f;
                }
                else if (k == 2)
                {
                    output[1] = 1.0f;
                }
                else if (k == 3)
                {
                    output[1] = 1.0f;
                    output[2] = 1.0f;
                }
                else if (k == 4)
                {
                    output[0] = 1.0f;
                }
                else if (k == 5)
                {
                    output[0] = 1.0f;
                    output[2] = 1.0f;

                }
                else if (k == 6)
                {
                    output[0] = 1.0f;
                    output[1] = 1.0f;

                }
                else if (k == 7)
                {
                    output[0] = 1.0f;
                    output[1] = 1.0f;
                    output[2] = 1.0f;
                }


                Texture2D texture = inputImages[k];
                Color[] colours = texture.GetPixels();

                for (int i = 0; i < colours.Length; i++)
                {
                    Color c = colours[i];
                    if (c == Color.black)
                        input.Add(1.0f);
                    else if (c == Color.white)
                        input.Add(0.0f);
                }
                nn.train(input, output);

                List<List<float>> to_insert = new List<List<float>>() { input, output };
                to_error.Add(to_insert);
            }
            Debug.Log(nn.calculate_total_error(to_error));
        }
    }

    void FeedNetwork(int key)
    {
        List<float> input = new List<float>();

        Texture2D texture = corruptedImages[key - 1];
        Color[] colours = texture.GetPixels();

        for (int i = 0; i < colours.Length; i++)
        {
            Color c = colours[i];
            if (c == Color.black)
                input.Add(1.0f);
            else if (c == Color.white)
                input.Add(0.0f);
        }
        List<float> salida = nn.feed_forward(input);

        string binary = ((int)Mathf.Round(salida[0])).ToString() + ((int)Mathf.Round(salida[1])).ToString() + ((int)Mathf.Round(salida[2])).ToString();

        int a = Convert.ToInt32(binary,2);
        Debug.Log(a);
        //Debug.Log(nn.feed_forward(input)[0]);
    }
}

