using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour {

	public float objectLife = 5f;

	void onEnable(){
		Invoke("Destroy", objectLife);
	}

	void Destroy(){
		gameObject.SetActive(false);
	}

	void onDisable(){
		CancelInvoke();
	}
}
