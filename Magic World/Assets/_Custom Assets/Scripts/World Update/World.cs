using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;
using InputSystem;

//Keeps track of all globals needed
//also runs all required scripts every frame
public class World : MonoBehaviour {

    public static World world;
    public BattleState battle;
    public WorldUpdate worldUpdate;
    //public static OverworldState overworld;
    public static InputManager inputs;
    public InputObject inputObject;

    //object pools for various objects
    public static ObjectPool<SpellBehaviour> spellPool;
    public static ObjectPool<Hitbox> hitboxPool;


    public List<CharacterManager> allCharacters = new List<CharacterManager>();
    public List<CharacterManager> players = new List<CharacterManager>();
    public List<CharacterManager> enemies = new List<CharacterManager>();
    public List<CharacterManager> npcs = new List<CharacterManager>();
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
    
}
