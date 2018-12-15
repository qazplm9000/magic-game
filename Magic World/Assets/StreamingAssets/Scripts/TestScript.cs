using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

    

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
                Debug.Log("Time set to 1");
            }
            else
            {
                Time.timeScale = 0f;
                Debug.Log("Time set to 0");
            }
        }
    }



}
