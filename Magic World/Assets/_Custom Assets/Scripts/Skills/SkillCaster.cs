using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

namespace SkillSystem
{
    public class SkillCaster : MonoBehaviour
    {


        private bool casting = false;
        public Skill currentSkill = null;
        private float castTimer = 0f;
        private bool interrupted = false;
        public CharacterManager target;
        private CharacterManager user;

        public List<Skill> skills = new List<Skill>();
        public int skillIndex = 0;

        private Dictionary<CastLocation, Transform> castLocationDict = new Dictionary<CastLocation, Transform>();


        private Animator animator;

        // Use this for initialization
        void Start()
        {
            animator = transform.GetComponent<Animator>();
            user = transform.GetComponent<CharacterManager>();
            InitCastLocations();
        }

        public void Update()
        {
            if (casting) {
                castTimer += Time.deltaTime;

                if (castTimer > currentSkill.castTime)
                {
                    currentSkill.CastSkill(user, target);
                    ResetCast();
                }
            }

            

            if (InputManager.manager.GetKeyDown("Cast")) {
                CastStart(skills[skillIndex]);
            }



        }

        //called while spell is first cast
        public void CastStart(Skill skill)
        {
            //instantiate spell effect
            skill.StartCast(user);
            castTimer = 0;
            casting = true;
            currentSkill = skill;
        }

        //called every frame while spell is casting
        private IEnumerator CastingSpell()
        {
            Debug.Log("Casting spell");
            while (castTimer < currentSkill.castTime)
            {
                castTimer += Time.deltaTime;
                yield return null;

                //stops the spellcast when interrupted
                if (interrupted)
                {
                    break;
                }
            }

            if (!interrupted)
            {
                StartCoroutine(CastEnd());
            }

            yield return null;
        }

        //called once spell is done being casted
        //when casttimer > casttime
        private IEnumerator CastEnd()
        {
            Debug.Log("Done casting");
            //change this to use an object pooler
            //GameObject spellObject = Instantiate(currentSpell.spellObject, transform.position, transform.rotation);

            ResetCast();

            yield return null;
        }


        public bool CastSpell(Skill skill)
        {
            bool casted = false;

            if (!IsCasting())
            {

                currentSkill = skill;
                casting = true;
                //StartCoroutine(CastStart());
                casted = true;
            }

            return casted;
        }

        public bool CastSpell() {
            return CastSpell(skills[skillIndex]);
        }

        public bool IsCasting()
        {
            return casting;
        }





        private void ResetCast()
        {
            casting = false;
            currentSkill = null;
            castTimer = 0;
            interrupted = false;
        }

        public void Interrupt()
        {
            interrupted = true;
        }

        /// <summary>
        /// Increments the index by amount (defaults to 1)
        /// </summary>
        /// <param name="amount"></param>
        public void IncrementIndex(int amount = 1)
        {

            if (skills.Count > 0)
            {
                skillIndex += amount;
                skillIndex %= skills.Count;
            }
        }


        /// <summary>
        /// Sets the spellIndex to newIndex
        /// Does nothing if there are no spells in the list
        /// Sets to last spell if index out of range
        /// Sets to first spell if index is negative
        /// </summary>
        /// <param name="newIndex"></param>
        public void SetIndex(int newIndex = 0)
        {

            //breaks out if there are no spells
            if (skills.Count == 0)
            {
                return;
            }

            if (newIndex >= skills.Count)
            {
                skillIndex = skills.Count - 1;
            }
            else if (newIndex < 0)
            {
                skillIndex = 0;
            }
            else
            {
                skillIndex = newIndex;
            }
        }

        public void PlayAnimation(string animationName)
        {
            user.combat.PlayAnimation(animationName);
        }



        public void InitCastLocations()
        {
            //left foot
            Transform leftFoot = transform.Find("left_foot_cast");
            if (leftFoot != null)
            {
                castLocationDict[CastLocation.left_foot] = leftFoot;
            }
            //right foot
            Transform rightFoot = transform.Find("right_foot_cast");
            if (rightFoot != null)
            {
                castLocationDict[CastLocation.right_foot] = rightFoot;
            }
            //left hand
            Transform leftHand = transform.Find("left_hand_cast");
            if (leftHand != null)
            {
                castLocationDict[CastLocation.left_hand] = leftHand;
            }
            //right hand
            Transform rightHand = transform.Find("right_hand_cast");
            if (rightHand != null)
            {
                castLocationDict[CastLocation.right_hand] = rightHand;
            }
            //mouth
            Transform mouth = transform.Find("mouth_cast");
            if (mouth != null)
            {
                castLocationDict[CastLocation.mouth] = mouth;
            }
            //body 1
            Transform body1 = transform.Find("body1_cast");
            if (body1 != null)
            {
                castLocationDict[CastLocation.body_1] = body1;
            }
            //body 2
            Transform body2 = transform.Find("body2_cast");
            if (body2 != null)
            {
                castLocationDict[CastLocation.body_2] = body2;
            }
            //body 3
            Transform body3 = transform.Find("body3_cast");
            if (body3 != null)
            {
                castLocationDict[CastLocation.body_3] = body3;
            }
            //body 4
            Transform body4 = transform.Find("body4_cast");
            if (body4 != null)
            {
                castLocationDict[CastLocation.body_4] = body4;
            }
            //body 5
            Transform body5 = transform.Find("body5_cast");
            if (body5 != null)
            {
                castLocationDict[CastLocation.body_5] = body5;
            }
            //body 6
            Transform body6 = transform.Find("body6_cast");
            if (body6 != null)
            {
                castLocationDict[CastLocation.body_6] = body6;
            }
            //default position
            Transform other = transform;
            castLocationDict[CastLocation.other] = other;


        }


        public Transform GetCastLocation(CastLocation location) {
            Transform result = null;
            if (castLocationDict.ContainsKey(location)) {
                result = castLocationDict[location];
            }

            return result;
        }

    }
}