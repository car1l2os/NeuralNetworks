using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron
{
    public float bias;
    public List<float> weights;
    public List<float> inputs;
    public float output;

    public Neuron(float bias)
    {
        this.bias = bias;
        this.weights = new List<float>();
    }

    public float calculate_output(List<float> inputs)
    {
        this.inputs = inputs;
        this.output = this.squash(this.calculate_total_net_input());
        return output;
    }

    public float calculate_total_net_input()
    {
        float total = 0;
        for (int i = 0; i < inputs.Count; i++)
        {
            total += this.inputs[i] * this.weights[i];
        }

        return total + this.bias;
    }

    public float squash(float total_net_input)
    {
        return 1.0f / (1.0f + Mathf.Exp(-total_net_input));
    }

    public float calculate_pd_error_wrt_total_net_input(float target_output)
    {
        return calculate_pd_error_wrt_output(target_output) * calculate_pd_total_net_input_wrt_input();
    }

    public float calculate_error(float target_output)
    {
        return 0.5f * Mathf.Pow((target_output - output), 2);
    }

    public float calculate_pd_error_wrt_output(float target_output)
    {
        return -(target_output - this.output);
    }

    public float calculate_pd_total_net_input_wrt_input()
    {
        return this.output * (1 - this.output);
    }

    public float calculate_pd_total_net_input_wrt_weight(int index)
    {
        return this.inputs[index];
    }

}
