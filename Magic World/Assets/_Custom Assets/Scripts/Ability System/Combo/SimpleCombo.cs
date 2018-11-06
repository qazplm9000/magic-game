using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

public class SimpleCombo : MonoBehaviour {

    private CharacterManager manager;
    public List<Combo> combos;

    private int comboIndex = 0;
    private int spellIndex = 0;

    private float comboTimer = 0f;
    public float comboTime = 1f;

	// Use this for initialization
	void Start () {
        manager = transform.GetComponent<CharacterManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (comboTimer > comboTime) {
            ResetCombo();
        }
	}

    public void UseCombo() {
        if (comboIndex < combos.Count) {
            Combo combo = combos[comboIndex];
            manager.anim.Play(combo.animation);
            comboIndex++;
            comboTimer = 0;
        }
    }

    public void UseSpell() {
        Combo combo = combos[comboIndex - 1];
        if (spellIndex < combo.spells.Count) {
            manager.caster.CastSpell(combo.spells[spellIndex]);
            spellIndex++;
        }
    }

    public void ResetCombo() {
        comboTimer = 0;
        comboIndex = 0;
        spellIndex = 0;
    }
}
