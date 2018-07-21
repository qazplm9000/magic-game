using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

namespace SkillSystem
{
    public class SkillCaster : MonoBehaviour
    {

        
        public Skill currentSkill = null;
        private float castTimer = 0f;
        private float thisFrame = 0f;
        private bool casting = false;
        private bool interrupted = false;
        public CharacterManager target;
        private CharacterManager characterManager;
        private SpellAction action = SpellAction.Nothing;

        public List<Skill> skills = new List<Skill>();
        public int skillIndex = 0;

        private Dictionary<CastLocation, Transform> castLocationDict = new Dictionary<CastLocation, Transform>();


        private Animator animator;

        // Use this for initialization
        void Start()
        {
            animator = transform.GetComponent<Animator>();
            characterManager = transform.GetComponent<CharacterManager>();
            InitCastLocations();
        }

        public void Update()
        {
            thisFrame = castTimer + characterManager.delta;
            switch (action) {
                case SpellAction.Casting:
                    if (castTimer <= currentSkill.castTime)
                    {
                        Debug.Log("Casting skill");
                        casting = currentSkill.CastingSkill(characterManager, target, castTimer, thisFrame, interrupted);
                        SetCasting(casting);
                        castTimer = thisFrame;
                    }
                    else {
                        Debug.Log("Finished Casting");
                        action = SpellAction.UsingSkill;
                        castTimer = 0;
                    }

                    if (!IsCasting()) {
                        ResetCast();
                    }
                    break;
                case SpellAction.UsingSkill:
                    if (castTimer <= currentSkill.useTime)
                    {
                        Debug.Log("Using skill");
                        casting = currentSkill.UsingSkill(characterManager, target, castTimer, thisFrame);
                        SetCasting(casting);
                        castTimer = thisFrame;
                    }
                    else {
                        Debug.Log("Finished using skill");
                        ResetCast();
                    }
                    break;
                default:
                    break;
            }

        }
        

        public bool CastSpell(Skill skill)
        {
            bool casted = false;

            if (action == SpellAction.Nothing && !characterManager.movementLocked)
            {
                currentSkill = skill;
                SetCasting(true);
                action = SpellAction.Casting;
                skill.StartCast(characterManager, target);
            }

            return casted;
        }

        public bool CastSpell() {
            return CastSpell(skills[skillIndex]);
        }

        private bool IsCasting()
        {
            return characterManager.isCasting;
        }

        private void SetCasting(bool isCasting) {
            characterManager.isCasting = isCasting;
        }



        private void ResetCast()
        {
            SetCasting(false);
            currentSkill = null;
            castTimer = 0;
            interrupted = false;
            action = SpellAction.Nothing;
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
            characterManager.combat.PlayAnimation(animationName);
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
            if (castLocationDict.ContainsKey(location))
            {
                result = castLocationDict[location];
            }
            else {
                result = transform;
            }

            return result;
        }

    }


    public enum SpellAction {
        Casting,
        UsingSkill,
        Nothing
    }
}