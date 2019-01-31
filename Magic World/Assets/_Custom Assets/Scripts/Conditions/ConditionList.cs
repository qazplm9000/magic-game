using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionList
{

    public List<Condition> conditions;

    public ConditionList() {
        conditions = new List<Condition>();
    }

    public bool ConditionsPass(CharacterManager character) {
        bool result = true;

        if (conditions.Count == 0)
        {
            result = true;
        }
        else {
            for (int i = 0; i < conditions.Count; i++) {
                if (!conditions[i]._Execute(character)) {
                    result = false;
                    break;
                }
            }
        }

        return result;
    }

}
