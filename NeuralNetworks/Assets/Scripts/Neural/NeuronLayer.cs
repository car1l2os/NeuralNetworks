using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronLayer {

    public float bias;
    public List<Neuron> neurons;

    public NeuronLayer(int num_neurons,float bias)
    {
        this.bias = bias;
        neurons = new List<Neuron>();

        for(int i=0;i<num_neurons;i++)
        {
            neurons.Add(new Neuron(bias));
        }
    }

    public void inspect()
    {
        Debug.Log("Neurons: " + neurons.Count);

        foreach(Neuron n in neurons)
        {
            Debug.Log(" Neuron: " + n);
            foreach(float w in n.weights)
            {
                Debug.Log("     Weight: " + w);
            }

            Debug.Log("     Bias: " + this.bias);
        }
    }

    public List<float> feed_forward(List<float> inputs)
    {
        List<float> outputs = new List<float>();

        foreach(Neuron n in neurons)
        {
            outputs.Add(n.calculate_output(inputs));
        }

        return outputs;
    }

    public List<float> get_outputs()
    {
        List<float> outputs = new List<float>();
        foreach (Neuron n in neurons)
        {
            outputs.Add(n.output);
        }

        return outputs;
    }
}
