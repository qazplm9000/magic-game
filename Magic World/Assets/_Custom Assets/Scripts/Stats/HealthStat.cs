using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthStat : Stat {

    public int currentValue;


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
        ValidateCurrentHealth();
    }

    public void RestoreHealth(int healing) {
        currentValue += healing;
        ValidateCurrentHealth();
    }

    private void ValidateCurrentHealth() {
        currentValue = currentValue > totalValue ? totalValue : currentValue;
        currentValue = currentValue < 0 ? 0 : currentValue;
    }
}
