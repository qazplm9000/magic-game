using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;
using UnityEngine.UI;


public class CharacterStats : MonoBehaviour{


    public HealthStat health;

    public Stat strength;
    public Stat magic;
    public Stat defense;

    public Transform healthBarObject;
    private Slider healthBar;

    private void Start()
    {
        try
        {
            healthBar = healthBarObject.GetComponent<Slider>();
        }
        catch {
            Debug.Log("Health bar not found");
        }
    }


    private void Update()
    {
        UpdateHealthBar();
    }

    /// <summary>
    /// Character takes damage
    /// </summary>
    /// <param name="spell"></param>
    /// <param name="enemyStats"></param>
    public void TakeDamage(int damage) {
        health.TakeDamage(damage);
    }


    public void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)health.currentValue / health.totalValue;
        }
    }
    
}
