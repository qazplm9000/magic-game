using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDirection : MonoBehaviour {

    Transform character;
    Vector3 characterDirection;

    public float rotationSpeed = 10f;

	// Use this for initialization
	void Start () {
        character = transform.parent;
        characterDirection = character.forward;
	}
	
	// Update is called once per frame
	void Update () {
        RotateCharacter();
	}

    public void RotateCharacter() {
        characterDirection = Input.GetAxis("Horizontal")*character.right + Input.GetAxis("Vertical")*character.forward;
        if (characterDirection.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(characterDirection, character.up);
        }
        else {
            if (Input.GetMouseButton(0)) {
                if (Input.GetAxis("Mouse X") != 0 ||
                    Input.GetAxis("Mouse Y") != 0) {
                    transform.rotation = Quaternion.LookRotation(character.forward, character.up);
                }
            }
        }
    }
}
