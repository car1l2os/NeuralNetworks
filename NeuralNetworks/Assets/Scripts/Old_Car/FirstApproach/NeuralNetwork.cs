using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork__antigua {

    Perceptron[] inputLayer, hiddenLayer, outputLayer;

    public NeuralNetwork__antigua(int numberInput, int numberHidden, int numberOutput)
    {
        inputLayer = new Perceptron[numberInput];
        hiddenLayer = new Perceptron[numberHidden];
        outputLayer = new Perceptron[numberOutput];

        for (int i=0;i<numberInput;i++)
        {
            inputLayer[i] = new Perceptron(3);
        }

        for (int i = 0; i < numberHidden; i++)
        {
            hiddenLayer[i] = new Perceptron(3);
        }

        for (int i = 0; i < numberOutput; i++)
        {
            outputLayer[i] = new Perceptron(3);
        }
    }
}
