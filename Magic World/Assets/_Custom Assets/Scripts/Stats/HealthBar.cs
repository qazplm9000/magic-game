using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StatSystem;

public class HealthBar : MonoBehaviour {

    private Slider healthBar;
    public CharacterManager target;

	// Use this for initialization
	void Start() {
        healthBar = transform.GetComponent<Slider>();
        if (target != null) {
            target.stats.health.valueUpdate += new SlidingStat.ValueUpdate(UpdateHealth);
        }
	}

    private void OnEnable()
    {
        if (target != null)
        {
            //Debug.Log(target.stats.health.currentValue);
            target.stats.health.valueUpdate += new SlidingStat.ValueUpdate(UpdateHealth);
            UpdateHealth();
        }
    }

    // Update is called once per frame
    void Update () {

	}

    public void UpdateHealth() {
        healthBar.value = Mathf.InverseLerp(0, target.stats.health.totalValue, target.stats.health.currentValue);
    }

    private void OnDisable()
    {
        if (target != null)
        {
            target.stats.health.valueUpdate -= UpdateHealth;
        }
    }
}
