using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;

[CreateAssetMenu(menuName = "Skill Effects/Instantiate Effect Data")]
public class InstantiateEffectData : SkillEffectData {
    public GameObject gameObject;
    public Vector3 offset;
    public Vector3 rotation;
}
