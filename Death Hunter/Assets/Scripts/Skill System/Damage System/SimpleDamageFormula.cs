using CombatSystem.StatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(menuName = "Skill/Simple Damage Formula")]
public class SimpleDamageFormula : DamageFormula, ISerializationCallbackReceiver
{
    public StatType casterAttackStat;
    public int atkCoeff = 2;
    public StatType targetDefenseStat;
    public int defCoeff = 4;
    public int potencyCoeff = 100;

    [Header("Test Values")]
    public int testAtk = 1;
    public int testDef = 1;
    public int testPotency = 1;

    [Space(5)]
    public int testDamage;

    public override int CalculateDamage(AttackData data)
    {
        int damage = (data.GetCasterStat(casterAttackStat) / atkCoeff - data.GetTargetStat(targetDefenseStat) / defCoeff) * data.potency / potencyCoeff;
        damage = Mathf.Max(1, damage);

        return damage;
    }

    public void OnAfterDeserialize()
    {
        description = $"({casterAttackStat} / {atkCoeff} - {targetDefenseStat} / {defCoeff}) * Potency / {potencyCoeff}";
        testDamage = (testAtk / atkCoeff - testDef / defCoeff) * testPotency / potencyCoeff;
        testDamage = Mathf.Max(1, testDamage);
    }

    public void OnBeforeSerialize()
    {
    }
}

