using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestScript : MonoBehaviour
{

    public TestClass t;

    private List<TestClass> ts = new List<TestClass>();

    private Dictionary<string, Vector3> testDict = new Dictionary<string, Vector3>();
    private Vector3 testValue = new Vector3(1, 1, 1);



    // Start is called before the first frame update
    void Start()
    {
        testDict["test"] = new Vector3(1, 1, 1);

        DateTime newTime = DateTime.Now;
        DateTime time = DateTime.Now;

        for (int i = 0; i < 1000000; i++) {
            //TestWithTypeCast();
        }

        newTime = DateTime.Now;

        Debug.Log("With Typecast: " + (newTime - time).TotalMilliseconds);

        time = DateTime.Now;

        for (int i = 0; i < 1000000; i++) {
            TestWithTypeCast();
        }

        newTime = DateTime.Now;

        Debug.Log("Without Typecast: " + (newTime - time).TotalMilliseconds);

    }

    // Update is called once per frame
    void Update()
    {
    }





    public void TestWithTypeCast() {
    }

    public void TestWithoutTypeCast() {
    }





    public interface TestClass{
        
    }


    public class TestClass1{

        public virtual void Test() {
            
        }
    }

    public class TestClass2{
        
    }









}
