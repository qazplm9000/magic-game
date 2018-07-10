using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ManaStat : Stat {

    int currentValue;


    public ManaStat()
    {
        value = 100;
        CalculateTotalValue();
        currentValue = 100;
        Init();
        OnTotalValueChanged += ValidateCurrentMana;
    }

    public ManaStat(int newValue)
    {
        value = newValue;
        CalculateTotalValue();
        currentValue = totalValue;
        Init();
        OnTotalValueChanged += ValidateCurrentMana;
    }

    public bool UseMana(int cost)
    {
        bool hasEnough = false;

        if (currentValue >= cost) {
            currentValue -= cost;
            hasEnough = true;
        }

        return hasEnough;
    }

    public void RestoreMana(int amount)
    {
        currentValue += amount;

        if (currentValue > totalValue)
        {
            currentValue = totalValue;
        }
    }

    private void ValidateCurrentMana()
    {
        if (currentValue > totalValue)
        {
            currentValue = totalValue;
        }
        else if (currentValue < 0)
        {
            currentValue = 0;
        }
    }
}
