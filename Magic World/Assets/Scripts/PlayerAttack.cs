using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Target")){
			findTarget();
		}
	}


	void findTarget(){
		//tries to find a target with the following tag
		target = GameObject.FindGameObjectWithTag("enemy");
	}

	void castSpell(){
		
	}

}
