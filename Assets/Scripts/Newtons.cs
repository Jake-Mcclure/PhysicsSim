using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newtons : MonoBehaviour
{
    
    private float gravity = 9.8f;
    private float timer = 0.0f;
    private float FPS = 1 / 60;
    private float fpscount = 0.0f;
    //private Vector3 acceleration = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 velocity = new Vector3(40.0f, 15.0f, 0.0f);
    private Vector3 startpos;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > FPS)
        {
            if (transform.position.y > 0)
            {
                //transform.position = new Vector3(startpos.x + (velocity.x * time), startpos.y + (velocity.y * time) + ((-gravity * (time * time) / 2)), startpos.y);
                transform.position = new Vector3(startpos.x + (velocity.x * (fpscount / 60)), startpos.y + (velocity.y * (fpscount / 60)) + ((-gravity * ((fpscount / 60) * (fpscount / 60)) / 2)), startpos.z);
                timer -= FPS;
            }
            fpscount++;

        }

    }

    
}
