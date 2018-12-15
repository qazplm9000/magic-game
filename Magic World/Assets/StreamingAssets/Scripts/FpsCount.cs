using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCount : MonoBehaviour {
	
	public float sampleTime = 0.5f;

	private float timer = 0;
	private int framecount = 0;
	private float fps = 0;

	void Start(){
		
	}

	// Update is called once per frame
	void Update () {
		framecount++;
		timer += Time.deltaTime;

		if(timer >= sampleTime){
			fps = framecount/sampleTime;
			timer = 0;
			framecount = 0;
		}

	}

	void OnGUI(){
		Rect labelPosition = new Rect(10,10,100,100);
		GUI.Label(labelPosition, fps.ToString());
	}

	/* Private Functions */

}
