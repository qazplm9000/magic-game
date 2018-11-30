using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StatSystem;
using TMPro;

public class HealthBar : MonoBehaviour {

    private Slider healthBar;
    public TextMeshProUGUI text;
    public CharacterManager target;

	// Use this for initialization
	void Start() {
        healthBar = transform.GetComponent<Slider>();
        text = transform.GetComponentInChildren<TextMeshProUGUI>();
        if (target != null) {
            target.stats.health.valueUpdate += new SlidingStat.ValueUpdate(UpdateHealth);
            UpdateHealth();
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
        int currentHealth = target.stats.health.currentValue;
        int maxHealth = target.stats.health.totalValue;

        healthBar.value = Mathf.InverseLerp(0, maxHealth, currentHealth);

        text.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    private void OnDisable()
    {
        if (target != null)
        {
            target.stats.health.valueUpdate -= UpdateHealth;
        }
    }
}
