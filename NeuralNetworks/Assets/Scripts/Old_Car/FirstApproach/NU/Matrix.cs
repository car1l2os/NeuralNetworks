using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix {

    public int rows, colums;
    float[,] matrix;

	public Matrix(int rows, int colums)
    {
        this.rows = rows;
        this.colums = colums;
        matrix = new float[rows, colums];
        for(int i=0;i< rows;i++)
        {
            for (int j=0;j<colums;j++)
            {
                matrix[i,j] = 0.0f;
            }
        }
    }

    public void Add(float n)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < colums; j++)
            {
                matrix[i,j] += n;
            }
        }
    } //Number
    public void Add(Matrix n)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < colums; j++)
            {
                matrix[i, j] += n.matrix[i,j];
            }
        }
    } //Matrix

    public void Multiply(float n)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < colums; j++)
            {
                matrix[i,j] *= n;
            }
        }
    }

    public void Randomize(float n)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < colums; j++)
            {
                matrix[i,j] = Random.Range(0.0f, 10.0f);
            }
        }
    }
}
