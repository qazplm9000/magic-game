using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)){
			gameObject.GetComponent<Renderer>().material.color += Color.red;
		}
		if(Input.GetKeyDown(KeyCode.T)){
			gameObject.GetComponent<Renderer>().material.color -= Color.red;
		}
	}
}
