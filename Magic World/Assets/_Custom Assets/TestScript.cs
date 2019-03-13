using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestScript : MonoBehaviour
{

    public TestClass t;

    private List<TestClass> ts = new List<TestClass>();
    
    private Vector3 testValue = new Vector3(1, 1, 1);



    // Start is called before the first frame update
    void Update()
    {
        /*Vector3 test = Vector3.zero;

        DateTime newTime = DateTime.Now;
        DateTime time = DateTime.Now;

        for (int i = 0; i < 1000000; i++) {
            TestWithTypeCast();
        }

        newTime = DateTime.Now;

        Debug.Log("With Typecast: " + (newTime - time).TotalMilliseconds);

        time = DateTime.Now;

        for (int i = 0; i < 1000000; i++) {
            TestWithoutTypeCast();
        }

        newTime = DateTime.Now;

        Debug.Log("Without Typecast: " + (newTime - time).TotalMilliseconds);
        */
        //InputSystem.AllowedActions actions = new InputSystem.AllowedActions();
    }
    




    public void TestWithTypeCast() {
        
    }

    public void TestWithoutTypeCast(object ob = null) {
    }

    









}
