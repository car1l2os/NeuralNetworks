using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour {

    [Header("Angle debug")]
    public float rotation_angle;
    public bool debug_angle = false;

    [Range(0,2)]
    public int angle_to_check;

    private int ostacleLayer = 1 << 8;

    public float[] angles = new float[3] { 45f, 0f, -45f }; //Three rays to make the input 
    public List<float> hits_distances = new List<float>();


    [Header("Obstacle variables")]
    public float obstacleDetectionDistance = 500.0f;

    private void Start()
    {
        hits_distances.Add(0.0f);
        hits_distances.Add(0.0f);
        hits_distances.Add(0.0f);
    }

    public void InputReceiverConstructor(float[] ang)
    {
        angles = ang;
    }

    private void Update()
    {
        CheckObstacle();
    }

    private void CheckObstacle()
    {
        for(int i=0;i<angles.Length;i++)
        {
            float angle = angles[i];
            Vector3 direction = Quaternion.AngleAxis(angle, transform.up) * transform.forward; 

            RaycastHit hitInfo; 

            bool hit = Physics.Raycast(transform.position,direction,out hitInfo,500f,ostacleLayer);
            if (hit)
            {
                Debug.Log("Ha dado a un obstaculo");
                hits_distances[i] = hitInfo.distance;

                if(debug_angle)
                {
                    if (angle == angles[angle_to_check])
                    {
                        Debug.Log(Mathf.RoundToInt(hitInfo.distance));
                        Debug.DrawRay(transform.position, direction * 500, Color.yellow, 0.1f, false); //Modificar punto final para dependiente de la posicion en la que estás 
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, direction * 500, Color.magenta, 0.1f, false); //Modificar punto final para dependiente de la posicion en la que estás 
                    }
                }
            }
        }
    }

    public void setAngles()
    {
        for(int i=0;i<angles.Length;i++)
        {
            angles[i] = Random.Range(-180f, 180f);
        }
    }
    public void setAngles(float [] agl)
    {
        angles = agl;
    }

}
