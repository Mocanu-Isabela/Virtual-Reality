using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitApp : MonoBehaviour
{
    // Start is called before the first frame update
    public System.DateTime startTime;
    void Start()
    {
        startTime = System.DateTime.UtcNow;
        
    }

    // Update is called once per frame
    void Update()
    {
        System.TimeSpan ts = System.DateTime.UtcNow - startTime;

        Debug.Log("seconds: " + ts);
        // if (ts.Seconds > 60)
        // {
        //     //Application.Quit();
        //     QuitGame();
        // }
        if (ts.Seconds > 55)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        
    }

        public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
