﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviour : MonoBehaviour {

    public TargetPoint target;
    public float speed = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        TargetPoint colliderTarget = other.GetComponent<TargetPoint>();

        if (colliderTarget == target) {
            target.manager.stats.TakeDamage(5);
            //function for spell disappearing
            ObjectPool.pool.RemoveObject(transform.gameObject);
        }
    }

}
