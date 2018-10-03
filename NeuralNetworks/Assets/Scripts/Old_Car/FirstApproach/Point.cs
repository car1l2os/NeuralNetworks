using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public float x;
    public float y;
    public float bias = 1.0f;
    public int label;

    public Point()
    {
        x = Random.Range(-100, 100);
        y = Random.Range(-100, 100);

        if (x > y)
        {
            label = 1;
        }
        else
        {
            label = -1;
        }

    }

    Point(float x, float y)
    {
        this.x = x;
        this.y = y;
    
        if (x > y)
        {
            label = 1;
        }
        else
        {
            label = -1;
        }
    }
}





