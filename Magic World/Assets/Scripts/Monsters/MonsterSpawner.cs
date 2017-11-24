using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public Monster monster;

    public float despawnTime = 20f;
    public float respawnTime = 30f;

    private float respawnTimer = 0f;

    private GameObject currentMonster;

	// Use this for initialization
	void Start () {
        ObjectPooler.pooler.AddObject(monster.monsterModel);
        SpawnMonster();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (currentMonster != null)
        {
            //Check if monster is dead
            if (currentMonster.GetComponent<Targetable>().dead)
            {
                currentMonster = null;
            }
        }//Repawns monster after certain amount of time
        else
        {
            respawnTimer += Time.deltaTime;
        }

        if (respawnTimer > respawnTime) {
            respawnTimer = 0f;
            SpawnMonster();
        }
	}

    private void SpawnMonster() {
        currentMonster = ObjectPooler.pooler.GetObject(monster.monsterModel, transform);
    }
}
