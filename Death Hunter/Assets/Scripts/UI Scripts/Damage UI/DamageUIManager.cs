using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUIManager : MonoBehaviour
{

    public DamageValueUI damageUIPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDamageValue(Combatant target, int damage)
    {
        DamageValueUI temp = WorldManager.PullObject(damageUIPrefab.gameObject).GetComponent<DamageValueUI>();
        temp.transform.parent = transform;
        temp.SetupDamage(target, damage);
        temp.transform.SetAsLastSibling();
    }
}
