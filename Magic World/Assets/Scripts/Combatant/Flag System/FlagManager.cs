using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Flag
{
    character_is_moving,
    character_can_move,
    character_is_casting,
    character_can_cast
}

public class FlagManager : MonoBehaviour, ISerializationCallbackReceiver
{
    public bool foldout;
    public List<bool> flagValues = new List<bool>();


    private void Awake()
    {
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
        int numOfFlags = Enum.GetNames(typeof(Flag)).Length;
        if (numOfFlags != flagValues.Count)
        {
            flagValues = new List<bool>(numOfFlags);
            bool[] tempFlags = new bool[numOfFlags];
            flagValues.AddRange(tempFlags);
        }
    }
}
