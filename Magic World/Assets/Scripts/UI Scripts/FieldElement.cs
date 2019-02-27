using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AbilitySystem;

public class FieldElement : MonoBehaviour
{

    public AbilityElement fieldElement;
    public Image foreground;
    public Image background;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        fieldElement = World.battle.fieldElement;
        foreground.sprite = fieldElement.elementImage;
    }
}
