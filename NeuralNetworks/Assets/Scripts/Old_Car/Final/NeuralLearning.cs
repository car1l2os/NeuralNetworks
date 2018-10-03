using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralLearning : MonoBehaviour {


    NeuralNetwork nn;

    //2, 2, 2, hidden_layer_weights=[0.15, 0.2, 0.25, 0.3], hidden_layer_bias=0.35, output_layer_weights=[0.4, 0.45, 0.5, 0.55], output_layer_bias=0.6

    List<float> hlw = new List<float> { 0.15f, 0.2f, 0.25f, 0.3f, 0.3f, 0.3f };
    List<float> olw = new List<float> { 0.4f, 0.45f, 0.5f, 0.55f };

    // Use this for initialization

    public void Create()
    {
        nn = new NeuralNetwork(3, 4, 2, null, 0.35f, null, 0.65f);
    }

    public void Train()
    {
        //Trainings - Giro, motor
        List<float> training_inputs = new List<float> { 45.0f, 250.0f, 45.0f };
        List<float> training_outputs = new List<float> { 0.0f,1.0f };
        List<List<float>> set = new List<List<float>> { training_inputs, training_outputs };

        List<float> training_inputs1 = new List<float> { 25.0f, 37.0f, 173.0f };
        List<float> training_outputs1 = new List<float> { 1.0f , - 1.0f   };
        List<List<float>> set1 = new List<List<float>> { training_inputs1, training_outputs1 };

        List<float> training_inputs2 = new List<float> { 37.0f, 67.0f, 87.0f };
        List<float> training_outputs2 = new List<float> { 0.5f, -0.8f };
        List<List<float>> set2 = new List<List<float>> { training_inputs2, training_outputs2 };

        List<float> training_inputs3 = new List<float> { 29.0f, 97.0f, 50.0f };
        List<float> training_outputs3 = new List<float> { 0.8f, 0.5f };
        List<List<float>> set3 = new List<List<float>> { training_inputs3, training_outputs3 };

        List<float> training_inputs4 = new List<float> { 20.0f, 66.0f, 67.0f };
        List<float> training_outputs4 = new List<float> { 0.5f, -0.6f };
        List<List<float>> set4 = new List<List<float>> { training_inputs4, training_outputs4 };

        List<float> training_inputs5 = new List<float> { 62.0f, 103.0f, 29.0f };
        List<float> training_outputs5 = new List<float> { -0.8f, 0.3f };
        List<List<float>> set5 = new List<List<float>> { training_inputs5, training_outputs5 };

        List<float> training_inputs6 = new List<float> { 97.0f, 70.0f, 32.0f };
        List<float> training_outputs6 = new List<float> { -0.5f, 0.1f };
        List<List<float>> set6 = new List<List<float>> { training_inputs6, training_outputs6 };

        List<float> training_inputs7 = new List<float> { 45.0f, 173.0f, 48.0f };
        List<float> training_outputs7 = new List<float> { 0.0f, 0.8f };
        List<List<float>> set7 = new List<List<float>> { training_inputs7, training_outputs7 };

        List<List<List<float>>> training_sets = new List<List<List<float>>> { set,set1,set2,set3,set4,set5,set6,set7 };

        for (int i = 0; i < 10000; i++)
        {
            nn.train(training_inputs, training_outputs);
            //Debug.Log(nn.calculate_total_error(training_sets));
        }
    }

    public List<float> GuessValue(List<float> input)
    {
        return nn.feed_forward(input);
    }
}
