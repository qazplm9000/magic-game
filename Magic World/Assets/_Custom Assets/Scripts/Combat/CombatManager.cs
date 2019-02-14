using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Tooltip("Used for setting rotation")]
    public Vector3 direction;

    [Tooltip("Used for proper run animation")]
    public float currentSpeed = 5f;
    [Tooltip("Highest speed character can move at")]
    public float maxSpeed = 5f;

    [Tooltip("Used for ending turn (can't end turn if true)")]
    public bool isAttacking = false;


    //public StateManager states


    public void Update()
    {
        
    }






}
