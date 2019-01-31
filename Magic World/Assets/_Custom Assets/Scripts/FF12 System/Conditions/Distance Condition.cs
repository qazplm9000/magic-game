using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Conditions/Distance From Target")]
public class DistanceCondition : Condition
{
    public float maxDistance;

    public override bool _Execute(CharacterManager character)
    {
        return (character.transform.position - character.target.transform.position).magnitude <= maxDistance;
    }
    
}
