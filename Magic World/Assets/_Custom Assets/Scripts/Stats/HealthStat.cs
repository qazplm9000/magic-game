using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthStat : Stat {

    int currentValue;


    public HealthStat() {
        value = 100;
        CalculateTotalValue();
        currentValue = 100;
        Init();
        OnTotalValueChanged += ValidateCurrentHealth;
    }

    public HealthStat(int newValue) {
        value = newValue;
        CalculateTotalValue();
        currentValue = totalValue;
        Init();
        OnTotalValueChanged += ValidateCurrentHealth;
    }

    public void TakeDamage(int damage) {
        currentValue -= damage;

        if (currentValue < 0) {
            currentValue = 0;
        }
    }

    public void RestoreHealth(int healing) {
        currentValue += healing;

        if (currentValue > totalValue) {
            currentValue = totalValue;
        }
    }

    private void ValidateCurrentHealth() {
        if (currentValue > totalValue)
        {
            currentValue = totalValue;
        }
        else if (currentValue < 0) {
            currentValue = 0;
        }
    }
}
