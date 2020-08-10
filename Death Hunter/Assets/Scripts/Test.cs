using CombatSystem;
using EffectSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public static int calls = 0;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = calls.ToString();
    }

    public static void AddCall()
    {
        calls++;
    }
}
