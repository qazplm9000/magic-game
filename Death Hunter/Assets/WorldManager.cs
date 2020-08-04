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
    public DamageValueUI damageUI;
    public Camera cam;
    public Canvas canvas;

    // Start is called before the first frame update
    void Awake()
    {
        if(world == null)
        {
            world = this;
            DontDestroyOnLoad(this);
            skillObjects.SetupDictionary();
            cam = Camera.main;
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
        DamageValueUI temp = Instantiate<DamageValueUI>(damageUI);
        temp.transform.parent = canvas.transform;
        temp.SetupDamage(target, damage);
    }
}
