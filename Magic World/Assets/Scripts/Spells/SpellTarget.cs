using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTarget : MonoBehaviour {

    private Transform target;

    public float speed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            FollowTarget();
        }
	}

    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Entered Trigger");
        if (collision.transform == target) {
            Targetable targetHealth = target.GetComponent<Targetable>();
            targetHealth.TakeDamage(10);
            transform.gameObject.SetActive(false);
        }
    }

    public void SetTarget(Transform newTarget) {
        target = newTarget;
    }

    public void FollowTarget() {
        transform.LookAt(target);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

}
