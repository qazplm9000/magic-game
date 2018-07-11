using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stat {

    public int value = 10;

    //temporary and permanent stat boosts
    private List<int> statModifiers;
    public int totalModifier;

    //temporary and permanent stat multipliers
    private List<float> statMultipliers;
    public float totalMultiplier;

    public delegate void TotalValueChanged();
    public event TotalValueChanged OnTotalValueChanged;

    public int totalValue;

    public Stat() {
        value = 10;
        Init();
        CalculateTotalValue();
    }

    public Stat(int newValue) {
        value = newValue;
        Init();
    }

    #region stat modifiers
    //adds a stat modifier in the list
    public void AddStatModifier(int modifier) {
        statModifiers.Add(modifier);
    }

    //removes a stat modifier in the list if it exists
    public bool RemoveStatModifier(int modifier) {
        bool result = false;

        if (statModifiers.Contains(modifier))
        {
            statModifiers.Remove(modifier);
            result = true;
        }

        return result;
    }

    //calculates total value of modifier bonuses
    protected void CalculateTotalModifier() {
        int tempModifier = 0;

        for (int i = 0; i < statModifiers.Count; i++) {
            totalModifier += statModifiers[i];
        }

        totalModifier = tempModifier;
    }
    #endregion

    #region multipliers
    //adds a multiplier
    public void AddMultiplier(float multiplier) {
        if (multiplier >= 0)
        {
            statMultipliers.Add(multiplier);
        }
    }

    //removes a multiplier and returns true if it exists
    public bool RemoveMultiplier(float multiplier) {
        bool result = false;

        if (statMultipliers.Contains(multiplier)) {
            statMultipliers.Remove(multiplier);
            result = true;
        }

        return result;
    }

    protected void CalculateTotalMultipliers() {
        float temp = 1;

        for (int i = 0; i < statMultipliers.Count; i++) {
            temp *= statMultipliers[i];
        }

        totalMultiplier = temp;
    }
    #endregion

    //calculates what the total value of the stat is after boosts
    public void CalculateTotalValue() {
        totalValue = (int)((value + totalModifier) * totalMultiplier);
    }



    protected void Init() {
        statModifiers = new List<int>();
        statMultipliers = new List<float>();
        OnTotalValueChanged += CalculateTotalValue;
    }
}
