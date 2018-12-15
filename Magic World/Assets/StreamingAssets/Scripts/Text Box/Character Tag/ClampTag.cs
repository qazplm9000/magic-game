using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampTag : MonoBehaviour {

	public Text nameLabel;
	public Slider healthbarLabel;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		FollowObject();
	}

	void FollowObject(){
		//makes text follow the object
		Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
		nameLabel.transform.position = namePos;
		healthbarLabel.transform.position = namePos;
	}

}
