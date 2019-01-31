using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationSystem
{
    [System.Serializable]
    public class AnimationManager : MonoBehaviour
    {
        private enum AnimationType
        {
            Attack,
            Cast,
            None
        }

        public List<string> attackAnimations;
        public List<string> castAnimations;

        private long lastCall = 0;
        private int lastIndex = -1;
        private AnimationType lastType = AnimationType.None;
        

        private CharacterManager character;

        private void Start()
        {
            character = transform.GetComponent<CharacterManager>();
        }

        /// <summary>
        /// Gets the proper casting animation
        /// </summary>
        /// <returns></returns>
        public string GetCastAnimation() {
            string result = "";

            switch (castAnimations.Count) {
                case 0:
                    break;
                case 1:
                    result = castAnimations[0];
                    break;
                default:
                    break;
            }

            return result;
        }

    }
}