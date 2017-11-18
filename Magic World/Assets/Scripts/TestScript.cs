using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	public bool casting = false;
	public Transform castLocation;
	public GameObject particleEffect;
	public GameObject throwEffect;

	public Transform target;

	public GameObject particleClone;


	public float castTime = 5f;
	public float spellTimer = 0f;

	// Use this for initialization
	void Start () {
		GameObject test = Instantiate(throwEffect) as GameObject;
		Destroy(test);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.T) && !casting){
			Cast();
			casting = true;
		}
	}


	public void Cast(){
		Debug.Log("Started casting spell");
		StartCoroutine("StartCast");
	}

	public IEnumerator StartCast(){
		particleClone = Instantiate(particleEffect,castLocation) as GameObject;
		while(spellTimer < castTime){
			spellTimer += Time.deltaTime;
			yield return null;
		}

		Debug.Log("Spell done casting");
		Destroy(particleClone);
		casting = false;
		spellTimer = 0;

		StartCoroutine("ThrowSpell");
	}

	public IEnumerator ThrowSpell(){
		GameObject throwClone = Instantiate(throwEffect, castLocation.position, castLocation.rotation) as GameObject;

		Transform throwTransform = throwClone.transform;
		throwTransform.rotation = transform.rotation;

		Vector3 lookVector = target.transform.position - throwTransform.position;

		while(lookVector.magnitude > 0.5f){
			throwTransform.rotation = Quaternion.LookRotation(lookVector);
			throwTransform.position += throwTransform.forward * 5 * Time.deltaTime;
			Debug.Log((throwTransform.position - target.transform.position).magnitude);
			Debug.DrawLine(throwTransform.position, target.position);
			yield return null;
			lookVector = target.transform.position - throwTransform.position;
		}

		Destroy(throwClone);
	}
}
