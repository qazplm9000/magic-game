using SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{

    public static WorldManager world;
    public SkillObjectDatabase skillObjects;

    // Start is called before the first frame update
    void Start()
    {
        if(world == null)
        {
            world = this;
            DontDestroyOnLoad(this);
            skillObjects.SetupDictionary();
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
}
