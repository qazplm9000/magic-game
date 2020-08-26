using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public interface ISkillComponent
    {
        void RunComponent(SkillCastData castData);
        bool IsFinished(SkillCastData castData);

        int GetID();
        void SetID(int newId);
        float GetEndTime();


        void ShowGUI();
        void UpdateDescription();
    }
}