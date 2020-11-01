using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CombatSystem.StatSystem;
using TargettingSystem;

public class HealthBar : MonoBehaviour
{

    public Combatant character;
    public Slider slider;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI characterText;
    public Image background;

    public Color activeColor;
    public Color inactiveColor;

    public bool isEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        HideUI();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfEnabled();

        if (isEnabled)
        {
            characterText.text = GetCharacter().GetName();
            UpdateHealth();
        }
    }

    protected void UpdateHealth()
    {
        ITargettable character = GetCharacter();

        int currentHealth = character.GetStat(StatType.CurrentHealth);
        int maxHealth = character.GetStat(StatType.MaxHealth);
        slider.value = currentHealth / (float)maxHealth;
        healthText.text = $"{currentHealth} / {maxHealth}";
    }

    protected virtual ITargettable GetCharacter()
    {
        return character;
    }

    public void HideUI()
    {
        Debug.Log($"{transform.name} UI hidden!");
        slider.gameObject.SetActive(false);
        healthText.gameObject.SetActive(false);
        characterText.gameObject.SetActive(false);
        background.color = inactiveColor;
    }

    public void ShowUI()
    {
        Debug.Log($"{transform.name} UI shown!");
        slider.gameObject.SetActive(true);
        healthText.gameObject.SetActive(true);
        characterText.gameObject.SetActive(true);
        background.color = activeColor;

        
    }


    protected void CheckIfEnabled()
    {
        if(isEnabled != TargetExists())
        {
            isEnabled = TargetExists();

            if (isEnabled)
            {
                ShowUI();
            }
            else
            {
                HideUI();
            }
        }
    }

    protected bool TargetExists()
    {
        return GetCharacter() != null;
    }
}
