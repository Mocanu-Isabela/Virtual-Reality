using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAfterMouse : MonoBehaviour
{

 float speed = 75.0f;
 Vector3 v3 = new Vector3(0f,0f,0f);

 float maxRange = 90f;
 float currentVertical = 0f;
 float currentHorizontal = 0f;
 
 void Update() {

    currentVertical +=-Input.GetAxis("Vertical");
    float vertical = 0f;
    if(currentVertical < maxRange && currentVertical > -maxRange){
        vertical = -Input.GetAxis("Vertical");
    }

    float hor = 0f;
    currentHorizontal += Input.GetAxis("Horizontal");
    if(currentHorizontal < maxRange && currentHorizontal > -maxRange){
        hor =  Input.GetAxis("Horizontal");
    }

    v3 = new Vector3(vertical, hor, 0.0f);
    //Debug.Log("vertical: " + -Input.GetAxis("Vertical"));
    transform.Rotate(v3 * speed * Time.deltaTime); 
 }
}
