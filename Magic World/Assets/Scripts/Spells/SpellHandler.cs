using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetCharacter))]
public class SpellHandler : MonoBehaviour {

    public BaseSpell spell;

    private TargetCharacter targetter;
    private Transform target;

    private Targetable userHealth;

    public GameObject castLocation;

    private Animator anim;

    private BaseSpell currentSpell = null;
    private float castTimer = 0f;

    private GameObject currentSpellEffect = null;

	// Use this for initialization
	void Start () {
        anim = transform.GetComponentInChildren<Animator>();
        targetter = transform.GetComponent<TargetCharacter>();
        userHealth = transform.GetComponent<Targetable>();
        ObjectPooler.pooler.AddObject(spell.spellEffect);
        ObjectPooler.pooler.AddObject(spell.spellObject);
	}
	
	// Update is called once per frame
	void Update () {
        //cast spell
        if (Input.GetKeyDown(KeyCode.T) && currentSpell == null) {
            if (target == null) {
                targetter.TargetNearestEnemy();
            }

            if (targetter.target != null && userHealth.currentMana >= spell.mpCost)
            {
                StartCast(spell);
            }
        }

        //update spell
        if (currentSpell != null) {
            if (currentSpell.castTime > castTimer)
            {
                CastUpdate();
            }
            else {
                FinishCast();
            }
        }
	}


    public void StartCast(BaseSpell spell) {
        currentSpell = spell;
        castTimer = 0f;
        target = targetter.target;
        currentSpellEffect = ObjectPooler.pooler.GetObject(spell.spellEffect, transform, transform);
        currentSpellEffect.SetActive(true);

        anim.SetBool("Casting", true);
    }

    public void CastUpdate() {
        castTimer += Time.deltaTime;
    }

    public void FinishCast() {
        GameObject spellObject = ObjectPooler.pooler.GetObject(spell.spellObject, castLocation.transform);
        spellObject.SetActive(true);
        spellObject.GetComponent<SpellTarget>().SetTarget(target);

        currentSpellEffect.SetActive(false);
        currentSpellEffect.transform.SetParent(null);
        userHealth.UseMana(spell.mpCost);

        currentSpell = null;
        target = null;
        castTimer = 0f;

        anim.SetBool("Casting", false);
    }

}
