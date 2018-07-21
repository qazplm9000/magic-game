using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;

[CreateAssetMenu(menuName = "Skill Effects/Instantiate Effect")]
public class InstantiateEffect : SkillEffect
{
    public override bool Execute(CharacterManager user, CharacterManager target, SkillEffectData data)
    {
        //bool result = true;
        InstantiateEffectData thisData = (InstantiateEffectData)data;
        GameObject gameObject = thisData.gameObject;
        Vector3 offset = thisData.offset;
        Quaternion offsetRotation = Quaternion.Euler(thisData.rotation);
        Transform userLocation = user.transform;

        GameObject thisObject = ObjectPool.pool.PullObject(gameObject, userLocation);
        thisObject.transform.parent = user.transform;
        thisObject.transform.localPosition += offset;
        thisObject.transform.localRotation *= offsetRotation;
        thisObject.transform.parent = null;
        Debug.Log("Instantiated object: " + thisObject.transform.position);
        return true;
    }
}
