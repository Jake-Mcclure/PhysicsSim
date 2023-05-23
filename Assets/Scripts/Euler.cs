using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Euler : MonoBehaviour
{
    private float timer = 0.0f;
    private float FPS = 1.0f / 60.0f;
    private float stepsperframe = 3.0f;
    private Vector3 acceleration = new Vector3(0.0f, -9.8f, 0.0f);
    private Vector3 velocity = new Vector3(40.0f, 15.0f, 0.0f);
    private Vector3 newvelocity;
    private Vector3 newposition;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    { 

        timer += Time.deltaTime;

        //if (timer > FPS)
        //{
            if (transform.position.y > 0)
            {
                for (int i = 0; i < stepsperframe; i++)
                {
                    newvelocity = velocity + (acceleration * (FPS / stepsperframe));
                    newposition = transform.position + (velocity * (FPS / stepsperframe));
                    velocity = newvelocity;
                    transform.position = newposition;
                    //print(velocity);
                }
            }
            timer -= FPS;
            
        //}
    }
}
