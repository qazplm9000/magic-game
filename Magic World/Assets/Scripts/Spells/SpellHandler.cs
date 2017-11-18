using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetCharacter))]
public class SpellHandler : MonoBehaviour {

    public BaseSpell spell;

    private TargetCharacter targetter;
    private Transform target;

    public GameObject castLocation;

    private BaseSpell currentSpell = null;
    private float castTimer = 0f;

    private GameObject currentSpellEffect = null;

	// Use this for initialization
	void Start () {
        targetter = transform.GetComponent<TargetCharacter>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T) && currentSpell == null) {
            if (target == null) {
                targetter.TargetNearestEnemy();
            }

            if (targetter.target != null)
            {
                StartCast(spell);
            }
        }

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
        currentSpellEffect = Instantiate<GameObject>(spell.spellEffect, transform);
    }

    public void CastUpdate() {
        castTimer += Time.deltaTime;
    }

    public void FinishCast() {
        GameObject spellObject = Instantiate<GameObject>(currentSpell.spellObject, castLocation.transform.position, transform.rotation);
        spellObject.GetComponent<SpellTarget>().SetTarget(target);
        Debug.Log("Spell Y Rotation: " + spellObject.transform.rotation.y);
        Debug.Log("Transform Y Rotation: " + transform.rotation.y);
        Destroy(currentSpellEffect);

        currentSpell = null;
        target = null;
        castTimer = 0f;
    }

}
