using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {

    public static PauseGame pause;

    public delegate void PauseDelegate();
    public static event PauseDelegate OnPause;
    public static event PauseDelegate OnUnpause;

    public GameObject PauseBackground;

    public KeyCode pauseButton = KeyCode.Escape;

    private float savedTimescale = 1f;

	// Use this for initialization
	void Start () {
        if (pause == null)
        {
            pause = this;
        }
        else {
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(pauseButton)) {
            if (Time.timeScale != 0f)
            {
                Pause();
            }
            else {
                Unpause();
            }
        }
	}

    //Saves the current time scale, sets the time scale to 0, then calls OnPause
    public void Pause()
    {
        savedTimescale = Time.timeScale;
        Time.timeScale = 0f;
        PauseBackground.SetActive(false);

        if (OnPause != null) {
            OnPause();
        }
    }

    //Sets the timescale to the saved timescale then calls OnUnpause
    public void Unpause() {
        Time.timeScale = savedTimescale;
        PauseBackground.SetActive(true);

        if (OnUnpause != null) {
            OnUnpause();
        }
    }

}
