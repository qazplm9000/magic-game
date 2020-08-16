using SkillSystem;
using System.Collections;
using System.Collections.Generic;
using TargettingSystem;
using UnityEngine;
using TMPro;
using CombatSystem;

public class WorldManager : MonoBehaviour
{

    public static WorldManager world;
    public SkillObjectDatabase skillObjects;
    public TargetTracker trackerPrefab;
    public DamageUIManager damageUI;
    public Camera cam;
    public Canvas canvas;
    public ObjectPool pool;

    public int groundLayer = 10;

    // Start is called before the first frame update
    void Awake()
    {
        if(world == null)
        {
            world = this;
            DontDestroyOnLoad(this);
            skillObjects.SetupDictionary();
            cam = Camera.main;
            pool = transform.GetComponent<ObjectPool>();
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static SkillObject GetSkillObjectByID(int id)
    {
        return world.skillObjects.GetObjectByID(id);
    }


    public void ShowDamageValue(Combatant target, int damage)
    {
        damageUI.ShowDamageValue(target, damage);
    }

    public static GameObject PullObject(GameObject obj)
    {
        return world.pool.PullObject(obj);
    }

    public static SkillObject PullSkillObject(int id)
    {
        SkillObject so = world.skillObjects.GetObjectByID(id);
        return world.pool.PullObject(so.gameObject).GetComponent<SkillObject>();
    }

    public static void RemoveObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = world.transform.position;
    }

    public static void AddObject(GameObject obj, int copies = 1)
    {
        world.pool.AddObject(obj, copies);
    }

    public static void AddSkillObject(int id, int copies = 1)
    {
        SkillObject so = world.skillObjects.GetObjectByID(id);
        world.pool.AddObject(so.gameObject, copies);
    }
}
