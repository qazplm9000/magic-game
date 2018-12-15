using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;
using InputSystem;

//Keeps track of all globals needed
//also runs all required scripts every frame
public class World : MonoBehaviour {

    public static World world;
    public static BattleState battle;
    public static InputManager inputs;
    public InputObject inputObject;


	// Use this for initialization
	void Awake () {
        if (world == null)
        {
            world = this;
        }
        else {
            Destroy(this);
        }
        if (inputs == null) {
            inputs = new InputManager();
            inputs.inputKeys = inputObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (battle != null) {
            battle.Update();
        }
	}
}
