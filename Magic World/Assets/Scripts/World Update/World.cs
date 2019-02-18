using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;
using InputSystem;
using AbilitySystem;
using EventSystem;

//Keeps track of all globals needed
//also runs all required scripts every frame
public class World : MonoBehaviour {

    public static World world;
    public BattleState battle;
    public WorldUpdate worldUpdate;
    //public static OverworldState overworld;
    public static InputManager inputs;
    public InputObject inputObject;
    public static CharacterEventManager eventManager;

    //object pools for various objects
    public ObjectPool<SpellBehaviour> spellPool;
    public ObjectPool<Hitbox> hitboxPool;


    [Header("Combat")]
    public List<CharacterManager> allCharacters = new List<CharacterManager>();
    public List<CharacterManager> turnOrder = new List<CharacterManager>();

    public CharacterManager currentTurn;
    public int turnIndex = 0;
    public float turnTime;
    public float turnTimer;


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
        SetAllCharacters();
        worldUpdate.StartWorld(this);
    }


    // Update is called once per frame
    void Update () {
        worldUpdate.UpdateWorld(this);
	}



    public void SetAllCharacters() {
        CharacterManager[] characters = FindObjectsOfType<CharacterManager>();
        allCharacters = new List<CharacterManager>();

        for (int i = 0; i < characters.Length; i++) {
            allCharacters.Add(characters[i]);
        }
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
