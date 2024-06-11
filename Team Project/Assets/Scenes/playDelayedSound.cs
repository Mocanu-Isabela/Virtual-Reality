using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class playDelayedSound : MonoBehaviour
{

    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
                Debug.Log("started");
                StartCoroutine(Wait()); 
        audioData.Play(0);

    }


     IEnumerator Wait() {
     yield return new WaitForSeconds (7);
 }
    // public AudioSource myAudio;
    // // Use this for initialization
    // void Start () {

    //     StartCoroutine(PlaySoundAfterDelay(myAudio, 1000.0f));
    // }

    // // Update is called once per frame
    // void Update () {

    // }

    // IEnumerator PlaySoundAfterDelay(AudioSource audioSource, float delay)
    // {
    //     if (audioSource == null)
    //         yield break;
    //     yield return new WaitForSeconds(delay);
    //     audioSource.Play();
    // }
}
