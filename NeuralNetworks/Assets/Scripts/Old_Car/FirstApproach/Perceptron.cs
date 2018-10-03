using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perceptron {

    public float[] weights;
    public float learningRate = 0.1f;

    public Perceptron(int numberWeights)
    {
        weights = new float[numberWeights];
        for(int i=0; i<weights.Length;i++)
        {
            weights[i] = Random.Range(-1.0f,1.0f);
        }
    }

    public int Approach(float[] inputs)
    {
        float sum = 0.0f;

        for (int i = 0; i < weights.Length; i++)
        {
            sum += inputs[i] * weights[i];
        }

    
        int result = Sigmoid(sum);
        return result;
    }

    int Sigmoid(float epsilon) // Activation function 
    {
        float value = 1.0f / (1.0f + (Mathf.Pow(Mathf.Exp(1), -5.0f * (epsilon - 0.75f)))); //Values?

        if (value > 0.5) return 1;
        else return -1;
    }

    public float Train(float[] inputs, float target)
    {
        int approach = Approach(inputs);
        float error = target - approach;

        //Adjustment of weights
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] += error * inputs[i] * learningRate;
        }

        return error; //To paint 
    }

}






























/*
 * 
public int Approach(float[] inputs)
{
    float sum = 0;

    for (int i = 0; i < weights.Length; i++)
    {
        sum += inputs[i] * weights[i];
    }

    int sign = Sign(sum);
    return sign;
}
int Sign(float value) //Activation Function
{
    if (value >= 0) return 1;
    else return -1;
}


    public int Train(float[] inputs, int target)
    {
        int approach = Approach(inputs);
        int error = target - approach;

        //Adjustment of weights
        for(int i=0;i<weights.Length;i++)
        {
            weights[i] += error * inputs[i] * learningRate;
        }

        return error; //To paint 
    }
    public float Train(float[] inputs, float target)
    {
        int approach = Approach(inputs);
        float error = target - approach;

        //Adjustment of weights
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] += error * inputs[i] * learningRate;
        }

        return error; //To paint 
    }

    */


