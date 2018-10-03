using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour {

    NeuralNetwork nn;


    //2, 2, 2, hidden_layer_weights=[0.15, 0.2, 0.25, 0.3], hidden_layer_bias=0.35, output_layer_weights=[0.4, 0.45, 0.5, 0.55], output_layer_bias=0.6

    List<float> hlw = new List<float> { 0.15f, 0.2f, 0.25f, 0.3f };
    List<float> olw = new List<float> { 0.4f, 0.45f, 0.5f, 0.55f };

    // Use this for initialization
    void Start () {
        nn = new NeuralNetwork(2, 2, 2, hlw, 0.35f, olw, 0.6f);

        List<float> training_inputs = new List<float> { 0.05f, 0.1f };
        List<float> training_outputs = new List<float> { 0.01f, 0.99f };
        List<List<float>> set = new List<List<float>> { training_inputs, training_outputs };
        List<List<List<float>>> training_sets = new List<List<List<float>>> { set };

        for (int i=0;i< 10000;i++)
        {
            nn.train(training_inputs, training_outputs);
                Debug.Log(nn.calculate_total_error(training_sets));
        }
}

}
