using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour
{
        // Start is called before the first frame update
        int straightended = 0;
        int landingStarted=0;
        int landed=0;
        Vector3 p = new Vector3 (0,0,0);
        
        //float speed = 1.0f; //how fast it shakes
        //float amount = 1.0f; //how much it shakes
        int pos = 0;
        public System.DateTime startTime;
    void Start()
    {
        transform.Rotate(-20f, 0.0f, 0F);
        startTime = System.DateTime.UtcNow;
    }


    void Update()
    {
        System.TimeSpan ts = System.DateTime.UtcNow - startTime;

        if (ts.Seconds > 10 && straightended==0)
        {
            straightended=1;
            transform.Rotate(20f, 0.0f, 0F);
        }

        if (ts.Seconds > 18 && ts.Seconds < 23)
        {
            turbulence();
        }

        if (ts.Seconds > 28 && ts.Seconds < 35)
        {
            turbulence();
        }

        if (ts.Seconds > 40 && landingStarted==0)
        {
            landingStarted=1;
            transform.Rotate(20f, 0.0f, 0F);
        }


        if (ts.Seconds > 50 )
        {
            if(landed==0)
            {
                landed = 1;
                transform.Rotate(-20f, 0.0f, 0F);

            }

        }

        else
        {
            transform.position += transform.forward * Time.deltaTime * 10.0f;
        }

    }

    void turbulence(){
        if (pos == 0){
            pos = 1;
            p = transform.position;
            p.x += 5f;
            transform.position = p;
        }
        else {
            pos = 0;
            p = transform.position;
            p.x -= 5f;
            transform.position = p;
        }
        
    }
}
