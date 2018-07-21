using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;

[CreateAssetMenu(menuName = "Skill Effects/Play Animation Effect")]
public class PlayAnimationEffect : SkillEffect
{
    public override bool Execute(CharacterManager user, CharacterManager target, SkillEffectData data)
    {
        PlayAnimationEffectData thisData = (PlayAnimationEffectData)data;
        string animationName = thisData.animationName;
        int animationLayer = thisData.animationLayer;
        float crossFadeTime = thisData.crossFadeTime;

        user.anim.CrossFade(animationName, crossFadeTime, animationLayer);

        return true;
    }
}
