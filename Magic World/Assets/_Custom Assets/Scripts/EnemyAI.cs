using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CombatController))]
public class EnemyAI : MonoBehaviour {


    private CombatController controller;
    public CombatController target;

    public float period = 0.5f;
    private float timer = 0f;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CombatController>();
	}
	
	// Update is called once per frame
	void Update () {

        if (timer > period)
        {

            if (target != null)
            {
                if (!controller.lockedMovement)
                {
                    StartCoroutine(controller.Dodge((transform.position - target.transform.position)));
                    Debug.Log("The enemy dodged an attack");
                }
            }

            timer -= period;
        }

        timer += Time.deltaTime;

	}

    private void OnTriggerEnter(Collider other)
    {

        if (target == null)
        {
            CombatController tempTarget = other.transform.GetComponent<CombatController>();

            if (tempTarget != null && tempTarget != controller)
            {
                target = tempTarget;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (target != null) {
            CombatController tempTarget = other.transform.GetComponent<CombatController>();
            if (tempTarget == target) {
                target = null;
            }
        }
    }

    public void FindTarget() {

    }


}
