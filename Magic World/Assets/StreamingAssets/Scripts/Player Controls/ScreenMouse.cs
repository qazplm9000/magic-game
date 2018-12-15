using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMouse : MonoBehaviour {

    public Texture mouseSprite;

    public float mouseSize = 50;

    [System.NonSerialized] public static float positionX;
    [System.NonSerialized] public static float positionY;

    private float yawY = 0;
    private float yawX = 0;

    [Range(0, 30)] public float mouseSpeed = 25;

    private bool hideCursor = false;

	// Use this for initialization
	void Start () {
        positionX = Screen.width/2;
        positionY = Screen.height/2;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

        HideCursor();

        if (!hideCursor)
        {
            yawY = Input.GetAxis("Mouse Y") * mouseSpeed;
            yawX = Input.GetAxis("Mouse X") * mouseSpeed;

            positionX += yawX;
            positionY -= yawY;

            positionX = Mathf.Clamp(positionX, 0, Screen.width);
            positionY = Mathf.Clamp(positionY, 0, Screen.height);
        }
        

	}

    void OnGUI()
    {
        Rect position = new Rect(positionX, positionY, mouseSize, mouseSize);
        

        if (!hideCursor)
        {
            //GUI.Box(position, content);
            GUI.DrawTexture(position, mouseSprite);
        }
    }

    private void HideCursor() {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
                hideCursor = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            hideCursor = false;
        }
    }

    private void CursorState() {
        
    }
}
