using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject plane;

    public GameObject sphere1;
    public GameObject sphere2;
    GameObject[] spheres = new GameObject[2];
    Collisions[] spheresscript = new Collisions[2];
    GameObject[] planes = new GameObject[2];

    private float m_timer = 0.0f;
    private float m_FPS = 1.0f / 60.0f;
    private float m_stepsperframe = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        spheres[0] = sphere1;
        spheres[1] = sphere2;
        spheresscript[0] = sphere1.GetComponent<Collisions>();
        spheresscript[1] = sphere2.GetComponent<Collisions>();

        planes[0] = plane;

    }

    // Update is called once per frame
    void Update()
    {

        m_timer += Time.deltaTime;

        if (m_timer > m_FPS)
        {
            for (int x = 0; x < spheres.Length; x++)
            {
                for (int y = 0; y < spheres.Length; y++)
                {
                    if (checkCollisionSphere(spheres[x], spheres[y]) && x != y)
                    {

                        if (spheresscript[x].m_velocity.magnitude != 0)
                        {
                            if (spheresscript[y].m_velocity.magnitude == 0)
                            {
                                sphereCollisionStill(spheres[x], spheres[y]);
                            }
                            if (spheresscript[y].m_velocity.magnitude != 0)
                            {
                                sphereCollisionMoving(spheres[x], spheres[y]);
                            }
                        }

                    }
                    if (checkCollisionPlane(spheres[x]))
                      {
                         //spheresscript[x].m_velocity.y = -spheresscript[x].m_velocity.y;
                        Vector3 pointa = new Vector3(2,0,2);
                        Vector3 pointb = new Vector3(5, 0, -2);
                        Vector3 pointc = new Vector3(-2, 0, 3);
                        Vector3 side1 = pointb - pointa;
                        Vector3 side2 = pointc - pointa;

                        Vector3 normal = Vector3.Cross(side1, side2);
                        normal = normal / normal.magnitude;
                        print(normal);
                        Vector3 Va = spheresscript[x].m_velocity / spheresscript[x].m_velocity.magnitude;
                        Vector3 vb = ((normal.x * -Va.x) + (normal.y * -Va.y) + (normal.z * -Va.z)) * 2 * normal + Va;
                        spheresscript[x].m_velocity = vb * spheresscript[x].m_velocity.magnitude;


                    }
                    for (int i = 0; i < m_stepsperframe; i++)
                    {
                        eulerForward(spheres[x]);

                        spheresscript[x].m_velocity = spheresscript[x].m_newvelocity;
                        spheres[x].transform.position = spheresscript[x].m_newposition;
                    }
                }
                
            }
            m_timer -= m_FPS;
        }


    }

    bool checkCollisionPlane(GameObject a)
    {
        
            if (a.transform.position.y - (a.transform.localScale.y / 2) <= plane.transform.position.y)
            {
                return true;
            }
            
        
        return false;
    }

    bool checkCollisionSphere(GameObject a, GameObject b)
    {
        Collisions asphere = a.GetComponent<Collisions>();
        Collisions bsphere = b.GetComponent<Collisions>();
 
        Vector3 pvector = new Vector3(a.transform.position.x - b.transform.position.x, a.transform.position.y - b.transform.position.y, a.transform.position.z - b.transform.position.z);
        Vector3 vvector = new Vector3(asphere.m_velocity.x - bsphere.m_velocity.x, asphere.m_velocity.y - bsphere.m_velocity.y, asphere.m_velocity.z - bsphere.m_velocity.z);
        float timeDifference = 1.0f;
        
        float A = timeDifference * (vvector.x * vvector.x) + timeDifference * (vvector.y * vvector.y) + timeDifference * (vvector.z * vvector.z);
        float B = (2 * ((timeDifference * pvector.x) * (timeDifference * vvector.x))) + (2 * ((timeDifference * pvector.y) * (timeDifference * vvector.y))) + (2 * ((timeDifference * pvector.z) * (timeDifference * vvector.z)));
        float C = (timeDifference * (pvector.x * pvector.x)) + (timeDifference * (pvector.y * pvector.y)) + (timeDifference * (pvector.z * pvector.z))
            - (((transform.localScale.y / 2) + (sphere1.transform.localScale.y / 2)) * ((transform.localScale.y / 2) + (sphere1.transform.localScale.y / 2)));

        float t = (-B + (Mathf.Sqrt((B * B) - (4 * A * C)))) / (2 * A);
        float t2 = (-B - (Mathf.Sqrt((B * B) - (4 * A * C)))) / (2 * A);

        if (t > 0 && t2 > 0)
        {

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

    void eulerForward(GameObject a)
    {
        Collisions asphere = a.GetComponent<Collisions>();
        asphere.m_newvelocity = asphere.m_velocity + (asphere.m_acceleration * (m_FPS / m_stepsperframe));
        asphere.m_newposition = a.transform.position + (asphere.m_velocity * (m_FPS / m_stepsperframe));
    }

    void sphereCollisionStill(GameObject a,GameObject b)
    {
        Collisions asphere = sphere1.GetComponent<Collisions>();
        Collisions bsphere = sphere2.GetComponent<Collisions>();
        Vector3 middle = b.transform.position - a.transform.position;
        Vector3 collivect;

        float temp = (middle.x * asphere.m_velocity.x) + (middle.y * asphere.m_velocity.y) + (middle.z * asphere.m_velocity.z);

        float costh = temp / (middle.magnitude * asphere.m_velocity.magnitude);

        if (costh > 0)
        {
            float sinth = Mathf.Sqrt(1 - (costh * costh));
            float dist = sinth * middle.magnitude;
            if (dist < 1)
            {
                float VD = costh * middle.magnitude;
                float E = Mathf.Sqrt(1 - (dist * dist));

                float VC = VD - E;
                if (VC < asphere.m_velocity.magnitude)
                {
                    Vector3 vcol = (asphere.m_velocity / asphere.m_velocity.magnitude) * VC;
                    collivect = vcol + a.transform.position;
                    float cosphi = E / 1;

                    Vector3 direction = (b.transform.position - collivect) / (1);

                    bsphere.m_velocity = Vector3.Scale((asphere.m_velocity * cosphi), direction);
                    asphere.m_velocity = asphere.m_velocity - bsphere.m_velocity;
                }
            }

        }


    }

    void sphereCollisionMoving(GameObject a, GameObject b)
    {
        Collisions asphere = sphere1.GetComponent<Collisions>();
        Collisions bsphere = sphere2.GetComponent<Collisions>();

        Vector3 middle = a.transform.position - b.transform.position;
        float A1 = asphere.m_velocity.magnitude;
        //(middle.x * asphere.m_velocity.x) + (middle.y * asphere.m_velocity.y) + (middle.z * asphere.m_velocity.z);
        float A2 = bsphere.m_velocity.magnitude;
        //(middle.x * bsphere.m_velocity.x) + (middle.y * bsphere.m_velocity.y) + (middle.z * bsphere.m_velocity.z);

        float optimizedp = (2.0f * (A1 - A2)) / (1.0f);
        asphere.m_velocity = asphere.m_velocity - optimizedp * middle;
        asphere.m_newvelocity = asphere.m_velocity - optimizedp * middle;

        bsphere.m_velocity = bsphere.m_velocity + optimizedp * 1 * middle;
        bsphere.m_newvelocity = bsphere.m_velocity + optimizedp * 1 * middle;
    }

}
