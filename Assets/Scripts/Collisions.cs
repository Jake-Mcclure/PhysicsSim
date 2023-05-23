using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    public GameObject plane;

    public GameObject sphere1;

    private float m_gravity = -9.8f;
    private float m_timer = 0.0f;
    private float m_FPS = 1.0f / 60.0f;
    //private float m_time = 0.0f;
    private float m_stepsperframe = 3.0f;
    public Vector3 m_acceleration = new Vector3(0.0f, -9.8f, 0.0f);
    //private Vector3 m_newacceleration = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 m_velocity = new Vector3(10.0f, 5.0f, 0.0f);
    public Vector3 m_newvelocity;
    public Vector3 m_newposition;

    // Update is called once per frame
    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_FPS)
        {

            for (int i = 0; i < m_stepsperframe; i++)
            {
                //if(checkCollisionSphere())
                //{
                //   print("Collision : sphere 1 " + transform.position + " sphere 2 " + sphere1.transform.position);
                //}

                //eulerForward();
                //if(checkCollisionPlane())
                //{
                //    m_velocity.y = -m_velocity.y;
                //    eulerForward();
                //}
                
                //m_velocity = m_newvelocity;
                //transform.position = m_newposition;
                
            }

            m_timer -= m_FPS;
        }


    }

    bool checkCollisionPlane()
    {
        if (transform.position.y - (transform.localScale.y / 2) <= plane.transform.position.y)
        {
            return true;
        }
        return false;
    }

    bool checkCollisionSphere()
    {
        Collisions othersphere = sphere1.GetComponent<Collisions>();
        Vector3 pvector = new Vector3(transform.position.x - sphere1.transform.position.x, transform.position.y - sphere1.transform.position.y, transform.position.z - sphere1.transform.position.z);
        Vector3 vvector = new Vector3(m_newvelocity.x - othersphere.m_velocity.x, m_newvelocity.y - othersphere.m_velocity.y, m_velocity.z - othersphere.m_velocity.z);
        float timeDifference = 1.0f;

        float A = timeDifference*(vvector.x * vvector.x) + timeDifference*(vvector.y * vvector.y) + timeDifference*(vvector.z * vvector.z);
        float B = (2 * ((timeDifference * pvector.x) * (timeDifference * vvector.x))) + (2 * ((timeDifference * pvector.y) * (timeDifference * vvector.y))) + (2 * ((timeDifference * pvector.z) * (timeDifference * vvector.z)));
        float C = (timeDifference * (pvector.x * pvector.x)) + (timeDifference * (pvector.y * pvector.y)) + (timeDifference * (pvector.z * pvector.z))
            - (((transform.localScale.y/2) + (sphere1.transform.localScale.y/2)) * ((transform.localScale.y / 2) + (sphere1.transform.localScale.y / 2)));

        float t = (-B + (Mathf.Sqrt((B * B) - (4 * A * C)))) / (2 * A);
        float t2 = (-B - (Mathf.Sqrt((B * B) - (4 * A * C)))) / (2 * A);

        if (t > 0 && t2 > 0)
        {
            //print("t1 = " + t + " t2 = " + t2);
            //print("Collision : sphere 1 " + transform.position + " sphere 2 " + sphere1.transform.position);
            //print("i am " + transform.position);
            if (t < t2)
            {
                if (t < m_FPS)
                    return true;
            }
            if (t2 < t)
            {
                if (t2 < m_FPS)
                    return true;
            }
            
        }
        

        return false;
    }

    void eulerForward()
    {
        m_newvelocity = m_velocity + (m_acceleration * (m_FPS / m_stepsperframe));
        m_newposition = transform.position + (m_velocity *(m_FPS/m_stepsperframe));
    }

    void sphereCollision()
    {
        Collisions othersphere = sphere1.GetComponent<Collisions>();
    }
}
