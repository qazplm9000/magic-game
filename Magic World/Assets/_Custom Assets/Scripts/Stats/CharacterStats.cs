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
        healthBar = healthBarObject.GetComponent<Slider>();
        UpdateHealthBar();
    }


    /// <summary>
    /// Character takes damage
    /// </summary>
    /// <param name="spell"></param>
    /// <param name="enemyStats"></param>
    public void TakeDamage(int damage) {
        health.TakeDamage(damage);
        UpdateHealthBar();
    }


    public void UpdateHealthBar()
    {
        healthBar.value = health.value / health.totalValue;
    }
    
}
