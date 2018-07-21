using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;

[CreateAssetMenu(menuName = "Skill Effects/Play Animation Data")]
public class PlayAnimationEffectData : SkillEffectData {
    public string animationName;
    public int animationLayer;
    public float crossFadeTime;
}
