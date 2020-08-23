using CombatSystem;
using StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StateSystem
{
    public enum StateActionType
    {
        ChangeFlag,
        SetAnimationBool,
        SetAnimationFloat,
        SetAnimationInt,
        EnableGravity,
        DisableGravity
    }

    [Serializable]
    public class StateActionData
    {
        public string description;
        public Flag flag;
        public bool flagValue;
        public string animationVariableName;
        public bool animationBoolValue;
        public float animationFloatValue;
        public int animationIntValue;

        public void UpdateDescription(StateActionType type)
        {
            switch (type)
            {
                case StateActionType.ChangeFlag:
                    description = $"Flag {flag.ToString()} -> {flagValue.ToString()}";
                    break;
                case StateActionType.SetAnimationBool:
                    description = $"Anim Bool \"{animationVariableName}\" -> {animationBoolValue.ToString()}";
                    break;
                case StateActionType.SetAnimationFloat:
                    description = $"Anim Bool \"{animationVariableName}\" -> {animationFloatValue.ToString()}";
                    break;
                case StateActionType.SetAnimationInt:
                    description = $"Anim Bool \"{animationVariableName}\" -> {animationIntValue.ToString()}";
                    break;
                case StateActionType.EnableGravity:
                    description = $"Gravity -> Enabled";
                    break;
                case StateActionType.DisableGravity:
                    description = $"Gravity -> Disabled";
                    break;
            }
        }
    }

    [Serializable]
    public class StateAction : ISerializationCallbackReceiver
    {
        public string description;
        public StateActionType type;
        public StateActionData data;

        public void CallAction(Combatant character)
        {
            switch (type)
            {
                case StateActionType.ChangeFlag:
                    character.ChangeFlag(data.flag, data.flagValue);
                    break;
                case StateActionType.SetAnimationBool:
                    character.SetAnimationBool(data.animationVariableName, data.animationBoolValue);
                    break;
                case StateActionType.SetAnimationFloat:
                    character.SetAnimationFloat(data.animationVariableName, data.animationFloatValue);
                    break;
                case StateActionType.SetAnimationInt:
                    character.SetAnimationInt(data.animationVariableName, data.animationIntValue);
                    break;
                case StateActionType.EnableGravity:
                    character.EnableGravity();
                    break;
                case StateActionType.DisableGravity:
                    character.DisableGravity();
                    break;
            }
        }

        public void OnAfterDeserialize()
        {
            data.UpdateDescription(type);
            description = data.description;
        }

        public void OnBeforeSerialize()
        {
            
        }
    }
}
