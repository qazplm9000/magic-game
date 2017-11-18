using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	public static ObjectPool current;
	public GameObject pooledObject;
	public int poolSize = 20;
	public bool allowGrow = true;

	public List<GameObject> pool;

	void Awake(){
		current = this;
	}

	// Use this for initialization
	void Start () {
		fillPool();
	}
		
	//Get a pooled object
	public GameObject GetPooledObject(){

		for(int i = 0; i < poolSize; i++){
			if(pool[i].activeInHierarchy){
				return pool[i];
			}
		}

		if(allowGrow){
			GameObject obj = CreateNewObject(pooledObject);
			pool.Add(obj);
			return obj;
		}

		return null;
	}



	/*
		Private Functions
	*/

	//fills the pool
	private void fillPool(){
		for(int i = 0; i < poolSize; i++){
			GameObject obj = CreateNewObject(pooledObject);
			pool.Add(obj);
		}
	}

	//creates a new object ready to be pooled
	private GameObject CreateNewObject(GameObject newObject){
		GameObject obj = Instantiate(newObject);
		obj.SetActive(false);
		return obj;
	}
}
