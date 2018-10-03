using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {

    float LEARNING_RATE = 0.3f;

    //Variables
    int num_inputs;
    //NeuronLayer input_layer; //Añadido
    NeuronLayer hidden_layer;
    NeuronLayer output_layer;

    public NeuralNetwork(int num_inputs, int num_hidden, int num_outputs, List<float> hidden_layer_weights = null, float hidden_layer_bias = float.NaN, List<float> output_layer_weights = null, float output_layer_bias = float.NaN)
    {
        this.num_inputs = num_inputs;

        //this.input_layer = new NeuronLayer(num_inputs, 0.5f); //Añadido
        this.hidden_layer = new NeuronLayer(num_hidden, hidden_layer_bias);
        this.output_layer = new NeuronLayer(num_outputs, output_layer_bias);

        //this.init_weights_from_inputs_to_input_layer_neurons();
        this.init_weights_from_inputs_to_hidden_layer_neurons(hidden_layer_weights);
        this.init_weights_from_hidden_layer_neurons_to_output_layer_neurons(output_layer_weights);
    }

    /*public void init_weights_from_inputs_to_input_layer_neurons()
    {
        for (int i = 0; i < input_layer.neurons.Count; i++)
        {
                input_layer.neurons[i].weights.Add(Random.Range(-1.0f, 1.0f));
        }
    }*/


    public void init_weights_from_inputs_to_hidden_layer_neurons(List<float> hidden_layer_weights)
    {
        int weight_num = 0;

        for(int i=0; i<hidden_layer.neurons.Count;i++)
        {
            for(int j=0;j<num_inputs;j++)
            {
                if(hidden_layer_weights == null)
                {
                    hidden_layer.neurons[i].weights.Add(Random.Range(-1.0f, 1.0f));
                }
                else
                {
                    hidden_layer.neurons[i].weights.Add(hidden_layer_weights[weight_num]);
                }
                weight_num += 1;
            }
        }
    }

    public void  init_weights_from_hidden_layer_neurons_to_output_layer_neurons(List<float> output_layer_weights)
    {
        int weight_num = 0;

        for(int i=0;i<output_layer.neurons.Count;i++)
        {
            for (int j = 0; j < hidden_layer.neurons.Count; j++)
            {
                if(output_layer_weights == null)
                {
                    this.output_layer.neurons[i].weights.Add(Random.Range(-1.0f, 1.0f));
                }
                else
                {
                    this.output_layer.neurons[i].weights.Add(output_layer_weights[weight_num]);
                }
                weight_num += 1;
            }
        }
    }

    public void inspect()
    {
        Debug.Log("------");
        Debug.Log("* Inputs: " + this.num_inputs);
        Debug.Log("------");
        Debug.Log("Hidden Layer");
        this.hidden_layer.inspect();
        Debug.Log("------");
        Debug.Log("* Output Layer");
        this.output_layer.inspect();
        Debug.Log("------");
    }

    public List<float> feed_forward(List<float> inputs)
    {
        //List<float> input_layer_outputs = this.input_layer.feed_forward(inputs); //Añadido
        List<float> hidden_layer_outputs = this.hidden_layer.feed_forward(inputs);
        return this.output_layer.feed_forward(hidden_layer_outputs);
    }


    public void train(List<float> training_inputs, List<float> training_outputs)
    {
        this.feed_forward(training_inputs);

        //1. Output neuron deltas
        List<float> pd_errors_wrt_output_neuron_total_net_input = new List<float>();
        for(int i=0;i< output_layer.neurons.Count;i++)
        {
            float value = output_layer.neurons[i].calculate_pd_error_wrt_total_net_input(training_outputs[i]);
            pd_errors_wrt_output_neuron_total_net_input.Add(value);
        }

        //2. Hidden neuron deltas
        List<float> pd_errors_wrt_hidden_neuron_total_net_input = new List<float>();
        for (int i = 0; i < hidden_layer.neurons.Count; i++)
        {
            pd_errors_wrt_hidden_neuron_total_net_input.Add(0.0f);

            /*We need to calculate the derivative of the error with respect to the output of each hidden layer neuron
                dE/dyⱼ = Σ ∂E/∂zⱼ * ∂z/∂yⱼ = Σ ∂E/∂zⱼ * wᵢ*/

            float d_error_wrt_hidden_neuron_output = 0.0f;

            for(int j=0; j<output_layer.neurons.Count;j++)
            {
                d_error_wrt_hidden_neuron_output += pd_errors_wrt_output_neuron_total_net_input[j] * this.output_layer.neurons[j].weights[i];
            }

            pd_errors_wrt_hidden_neuron_total_net_input[i] = d_error_wrt_hidden_neuron_output * this.hidden_layer.neurons[i].calculate_pd_total_net_input_wrt_input();
        }

        //3. Update output neuron weights

        for(int i=0;i<this.output_layer.neurons.Count;i++)
        {
            for(int j=0;j<this.output_layer.neurons[i].weights.Count;j++)
            {
                //∂Eⱼ/∂wᵢⱼ = ∂E/∂zⱼ * ∂zⱼ/∂wᵢⱼ
                float pd_error_wrt_weight = pd_errors_wrt_output_neuron_total_net_input[i] * this.output_layer.neurons[i].calculate_pd_total_net_input_wrt_weight(j);

                //Δw = α * ∂Eⱼ/∂wᵢ
                this.output_layer.neurons[i].weights[j] -= this.LEARNING_RATE * pd_error_wrt_weight;
            }
        }

        //4. Update hidden neuron weights
        for(int i=0;i<this.hidden_layer.neurons.Count;i++)
        {
            for(int j=0;j<this.hidden_layer.neurons[i].weights.Count;j++)
            {
                //∂Eⱼ/∂wᵢ = ∂E/∂zⱼ * ∂zⱼ/∂wᵢ
                float pd_error_wrt_weight = pd_errors_wrt_hidden_neuron_total_net_input[i] * this.hidden_layer.neurons[i].calculate_pd_total_net_input_wrt_weight(j);

                //# Δw = α * ∂Eⱼ/∂wᵢ
                this.hidden_layer.neurons[i].weights[j] -= this.LEARNING_RATE * pd_error_wrt_weight;
            }
        }
    }

    public float calculate_total_error(List<List<List<float>>> training_sets)
    {
        float total_error = 0.0f;
        List<float> training_inputs_l = new List<float>();
        List<float> training_outputs_l = new List<float>();

        for (int i=0; i< training_sets.Count;i++)
        {
            training_inputs_l = training_sets[i][0];
            training_outputs_l = training_sets[i][1];
            this.feed_forward(training_inputs_l);

            for(int j=0;j< training_outputs_l.Count;j++)
            {
                total_error += this.output_layer.neurons[j].calculate_error(training_outputs_l[j]);
            }
        }
        return total_error;
    }

}
