using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StatSystem;
using TMPro;

public class HealthBar : MonoBehaviour {

    private Slider healthBar;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI healthText;
    public CharacterManager target;

	// Use this for initialization
	void Awake() {
        healthBar = transform.GetComponentInChildren<Slider>();
        /*
        if (target != null) {
            target.stats.health.valueUpdate += new SlidingStat.ValueUpdate(UpdateHealth);
            UpdateHealth();
        }*/
	}

    private void OnEnable()
    {
        /*if (target != null)
        {
            //Debug.Log(target.stats.health.currentValue);
            target.stats.health.valueUpdate += new SlidingStat.ValueUpdate(UpdateHealth);
            UpdateHealth();
            Debug.Log("Added health event");
        }*/
    }

    // Update is called once per frame
    void Update () {
        UpdateHealth();
	}

    public void UpdateHealth() {
        int currentHealth = target.stats.currentHealth;
        int maxHealth = target.stats.maxHealth;

        healthBar.value = Mathf.InverseLerp(0, maxHealth, currentHealth);

        healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }


    public void SetNewTarget(CharacterManager newTarget) {
        target = newTarget;
        UpdateHealth();
    }

    private void OnDisable()
    {
        /*if (target != null)
        {
            target.stats.health.valueUpdate -= UpdateHealth;
            Debug.Log("Removed health event");
        }*/
    }
}
