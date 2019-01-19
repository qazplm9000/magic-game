using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestScript : MonoBehaviour
{

    public TestClass t;

    private List<TestClass> ts = new List<TestClass>();

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 1000000; i++) {
            ts.Add(new TestClass());
        }

        DateTime newTime = DateTime.Now;
        DateTime time = DateTime.Now;

        for (int i = 0; i < 1000000; i++) {
            TestWithTypeCast(ts[i]);
        }

        newTime = DateTime.Now;

        Debug.Log("With Typecast: " + (newTime - time).TotalMilliseconds);

        time = DateTime.Now;

        for (int i = 0; i < 1000000; i++) {
            TestWithoutTypeCast(ts[i]);
        }

        newTime = DateTime.Now;

        Debug.Log("Without Typecast: " + (newTime - time).TotalMilliseconds);

    }

    // Update is called once per frame
    void Update()
    {
    }





    public void TestWithTypeCast(object obj) {
        TestClass character = (TestClass)obj;
        character.Test();
    }

    public void TestWithoutTypeCast(TestClass character) {
        character.Test();
    }

}
