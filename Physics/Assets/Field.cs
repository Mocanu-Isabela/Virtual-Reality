using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    // Start is called before the first frame update
    float resistance = 5;
    float hits = 0;
    void Start()
    {

    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        // if(collision.collider.CompareTag("canonBall")){
        hits +=1;
        // }

        if(hits > resistance){
            Destroy(gameObject);
        }
        
    }
}