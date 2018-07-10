using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCharacter : MonoBehaviour {

    public Transform target;
    private Targetable targetHealth;

    public GUIStyle portraitStyle;

    public Rect playerBarPosition;
    public Rect enemyBarPosition;

    public Texture2D portraitForeground;
    public Texture2D portraitBackground;
    public Texture2D healthBar;
    public Texture2D manaBar;

    public float clickTimeSensitivity = 0.1f;
    private float clickHoldTime = 0f;

    private Vector3 clickLocation;

    public Camera camera;

	// Use this for initialization
	void Start () {
        if (camera == null) {
            camera = Camera.main;
        }
	}
	
	// Update is called once per frame
	void Update () {
        TargetObject();
	}

    void OnGUI()
    {
        if (target != null)
        {
            RenderTargetHealth(enemyBarPosition);
        }

        RenderTargetHealth(playerBarPosition);
    }


    public void TargetObject() {

        //check where the mouse is located upon clicking
        if (Input.GetMouseButtonDown(0)) {
            clickLocation = Input.mousePosition;
        }

        //while holding the mouse down, timer increases
        if (Input.GetMouseButton(0)) {
            clickHoldTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0)) {
            //only change target if either mouse was held for a short time or player did not move the mouse
            if (clickHoldTime < clickTimeSensitivity ||
                clickLocation == Input.mousePosition) {
                DrawLineToTarget();
            }
            clickHoldTime = 0;
        }

        //gets the nearest enemy
        if (Input.GetKeyDown(KeyCode.Tab)) {
            TargetNearestEnemy();
        }

    }


    public void TargetNearestEnemy() {
        Targetable[] targets = GameObject.FindObjectsOfType<Targetable>();

        foreach (Targetable t in targets)
        {
            if (t.relation == Relation.Enemy && !t.dead)
            {
                target = t.transform;
                targetHealth = t;
                break;
            }
        }
    }


    //Draw line and choose target
    public void DrawLineToTarget() {
        RaycastHit hit;

        Ray ray = camera.ScreenPointToRay(clickLocation);

        if (Physics.Raycast(ray, out hit)) {
            Targetable healthComponent = hit.transform.GetComponent<Targetable>();
            if (healthComponent != null)
            {
                target = hit.transform;
                targetHealth = healthComponent;
            }
            else {
                target = null;
                targetHealth = null;
            }
        }
    }


    public void RenderTargetHealth(Rect barPosition) {

        GUI.Box(barPosition, portraitBackground, portraitStyle);
        GUI.Box(barPosition, manaBar, portraitStyle);
        GUI.Box(barPosition, healthBar, portraitStyle);
        GUI.Box(barPosition, portraitForeground, portraitStyle);
    }

}
