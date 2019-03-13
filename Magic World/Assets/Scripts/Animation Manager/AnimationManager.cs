using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationSystem
{
    public class AnimationManager : MonoBehaviour
    {
        private CharacterManager character;
        private Animator anim;

        public List<CharacterAnimation> animations;
        private Dictionary<string, string> _animations;

        public float crossFadeTime = 0.2f;


        public void Awake()
        {
            character = transform.GetComponent<CharacterManager>();
            anim = transform.GetComponentInChildren<Animator>();
        }


        public void Update()
        {
            anim.SetFloat("Speed", character.GetCurrentSpeed());
        }


        /// <summary>
        /// Plays the animation mapped to the name 
        /// </summary>
        /// <param name="animation"></param>
        public void PlayAnimation(string animation, int layer = 0) {
            string mappedAnim = _animations[animation];
            anim.CrossFade(mappedAnim, crossFadeTime);
        }


        /// <summary>
        /// Plays the animation in the Animator of the given name
        /// </summary>
        /// <param name="animation"></param>
        public void PlayRawAnimation(string animation) {
            anim.CrossFade(animation, crossFadeTime);
        }


        public void SetAnimator(Animator newAnim) {
            anim = newAnim;
        }

    }
}