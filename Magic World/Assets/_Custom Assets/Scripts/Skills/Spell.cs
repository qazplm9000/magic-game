using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    [CreateAssetMenu(menuName ="Skills/Bolt Spell")]
    public class BoltSpell : Skill
    {
        public GameObject spellObject;

        public override bool StartCast(CharacterManager user)
        {
            bool result = true;
            //instantiate effects and play animation
            user.anim.CrossFade(castAnimationName, 0.1f);

            return result;
        }

        public override bool CastSkill(CharacterManager user, CharacterManager target = null)
        {
            bool result = true;

            //instantiate spell object and play animation
            user.anim.CrossFade(launchAnimationName, 0.1f);
            Transform castingLocation = user.caster.GetCastLocation(castLocation);
            GameObject castObject = ObjectPool.pool.PullObject(spellObject, castingLocation);

            //make sure the spell object has the bolt spell behaviour, or at least some sort of behaviour
            BoltSpellBehaviour behaviour = castObject.GetComponent<BoltSpellBehaviour>();
            behaviour.InitializeEffect(user, target);
            behaviour.enabled = true;

            return result;
        }

    }
}