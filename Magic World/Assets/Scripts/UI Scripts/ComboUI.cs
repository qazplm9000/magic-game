using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour
{
    [Tooltip("Index starts at 0")]
    public int comboIndex = 0;
    public Image comboImage;
    public Image comboBackground;
    public Sprite sprite;
    public Color activeColor;
    public Color inactiveColor;
    public CharacterManager target;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null) {
            sprite = target.abilityCaster.listOfCombos[comboIndex].image;
            comboImage.sprite = sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target.abilityCaster.currentComboIndex == comboIndex)
        {
            comboBackground.color = activeColor;
        }
        else {
            comboBackground.color = inactiveColor;
        }
    }
}
