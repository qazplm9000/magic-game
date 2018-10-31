using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour {

    public CharacterManager manager;

    private void Awake()
    {
        manager = transform.GetComponent<CharacterManager>();
    }

}
