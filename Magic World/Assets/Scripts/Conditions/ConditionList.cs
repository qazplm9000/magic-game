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
                Condition condition = conditions[i];

                if(condition == null)
                {
                    Debug.Log("There is no condition set in " + character.name);
                    
                }
                else if (!conditions[i].Execute(character)) {
                    result = false;
                    break;
                }
            }
        }

        return result;
    }

}
