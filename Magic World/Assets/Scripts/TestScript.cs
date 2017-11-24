using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

    public GameObject testObject;
    public SpellTarget script;

    public void Start()
    {
        ObjectPooler.pooler.AddObject(testObject);
        ObjectPooler.pooler.AddScript(testObject, script);
    }



}
