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
    //public static OverworldState overworld;
    public static InputManager inputs;
    public InputObject inputObject;

    //object pools for various objects
    public static ObjectPool<SpellBehaviour> spellPool;
    public static ObjectPool<Hitbox> hitboxPool;

    public WorldState state;

	// Use this for initialization
	void Awake () {
        if (world == null)
        {
            world = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this);
        }
        if (inputs == null) {
            inputs = new InputManager();
            inputs.inputKeys = inputObject;
        }

        spellPool = new ObjectPool<SpellBehaviour>();
        hitboxPool = new ObjectPool<Hitbox>();
	}
    

    // Update is called once per frame
    void Update () {

        //overworld update
        if (state == WorldState.Overworld)
        {
            
        }//battle update
        else if (state == WorldState.Battle) {
            battle.Update();
        }

	}
}
