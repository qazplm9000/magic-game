using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MagicLevel {

    public int level = 1;
    public int totalExperience = 0;
    public int currentExperience = 0;
    public int experienceForNextLevel = 100;

    public Element element;

    public void GainExperience(int experience) {
        totalExperience += experience;
        currentExperience += experience;
    }

    public int CalculateExperienceForNextLevel() {
        int result = 100;

        return result;
    }

    public bool CanLevelUp()
    {
        return currentExperience >= experienceForNextLevel;
    }

    public void LevelUp()
    {
        currentExperience -= experienceForNextLevel;
        level++;
    }

}
