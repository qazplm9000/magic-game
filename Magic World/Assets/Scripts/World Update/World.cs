using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;
using InputSystem;
using AbilitySystem;
using EventSystem;
using BattleSystem;

//Keeps track of all globals needed
//also runs all required scripts every frame
public class World : MonoBehaviour {

    public static World world;
    public static BattleManager battle;
    //public static OverworldState overworld;
    public static InputManager inputs;
    public InputObject inputObject;
    public static CharacterEventManager eventManager;
    public GlobalInputManager input1;
    public GlobalInputManager input2;

    //object pools for various objects
    public ObjectPool<SpellBehaviour> spellPool;
    public ObjectPool<Hitbox> hitboxPool;
    

    //main camera
    public Camera mainCamera;

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

        eventManager = transform.GetComponent<CharacterEventManager>();
        battle = transform.GetComponent<BattleManager>();

        GlobalInputManager[] allInputs = transform.GetComponents<GlobalInputManager>();
        input1 = allInputs[0];
        input2 = allInputs[1];

        spellPool = new ObjectPool<SpellBehaviour>();
        hitboxPool = new ObjectPool<Hitbox>();

        mainCamera = Camera.main;
	}
    

    public void OnLevelWasLoaded(int level)
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        //worldUpdate.StartWorld(this);
    }


    // Update is called once per frame
    void Update () {
        //worldUpdate.UpdateWorld(this);
	}

    


    // Pooling functions

    public static SpellBehaviour PullSpellObject(GameObject go) {
        return world.spellPool.PullObjectBehaviour(go);
    }

    public static Hitbox PullHitboxObject(GameObject go) {
        return world.hitboxPool.PullObjectBehaviour(go);
    }

    public static void RemoveObject(GameObject go) {
        world.spellPool.RemoveObject(go);
        go.transform.position = World.world.transform.position;
    }

}
