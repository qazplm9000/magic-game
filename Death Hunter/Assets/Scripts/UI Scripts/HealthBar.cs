using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CombatSystem.StatSystem;

public class HealthBar : MonoBehaviour
{

    public Combatant character;
    public Slider slider;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI characterText;


    // Start is called before the first frame update
    void Start()
    {
        characterText.text = character.characterName;
        UpdateHealth(character.GetStat(Stat.CurrentHealth), character.GetStat(Stat.MaxHealth));
    }

    // Update is called once per frame
    void Update()
    {

        UpdateHealth(character.GetStat(Stat.CurrentHealth), character.GetStat(Stat.MaxHealth));
    }

    private void UpdateHealth(int currentHealth, int maxHealth)
    {
        slider.value = currentHealth / (float)maxHealth;
        healthText.text = $"{currentHealth} / {maxHealth}";
    }
}
