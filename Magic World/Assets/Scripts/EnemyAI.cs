using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public Transform target;
	public int moveSpeed = 5;
	public int rotationSpeed = 10;
	public int range = 5;

	private Transform myTransform;

	void Awake(){
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		findTarget();
	}
	
	// Update is called once per frame
	void Update () {
		drawDebugLine();
		lookAtTarget();
		moveForward();
	}


	void findTarget(){
		//finds a target with tag "player"
		GameObject player = GameObject.FindGameObjectWithTag("player");
		target = player.transform;
	}

	void lookAtTarget(){
		//turns object towards target

		//sets the direction to look in
		Vector3 lookVector = target.position - myTransform.position;
		lookVector.y = 0; // sets y vector to 0 so that object doesn't rotate up or down

		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, 
												Quaternion.LookRotation(lookVector), 
												rotationSpeed * Time.deltaTime);
	}


	void moveForward(){
		//move object forward
		if((myTransform.position - target.position).magnitude > range){
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
	}

	void drawDebugLine(){
		//draws a line from this object to its target
		if(target != null){
			Debug.DrawLine(myTransform.position, target.position, Color.yellow);
		}
	}
}
