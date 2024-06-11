using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTurbulence : MonoBehaviour
{
    // Start is called before the first frame update
    int pos = 0;
    public System.DateTime startTime;
    Vector3 p = new Vector3 (0,0,0);


    void Start()
    {
        startTime = System.DateTime.UtcNow;
        
    }

    // Update is called once per frame
    void Update()
    {

        System.TimeSpan ts = System.DateTime.UtcNow - startTime;
        if (ts.Seconds > 18 && ts.Seconds < 23)
        {
            float intensity =0.02f;
            turbulence(intensity);
        }

        if (ts.Seconds > 28 && ts.Seconds < 35)
        {
            float intensity =0.05f;
            turbulence(intensity);
        }
        
    }

    void turbulence(float intensity){
        if (pos == 0){
            pos = 1;
            p = transform.position;
            p.y += intensity;
            transform.position = p;
        }
        else {
            pos = 0;
            p = transform.position;
            p.y -= intensity;
            transform.position = p;
        }
        
    }

}
