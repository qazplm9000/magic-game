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

    }

    // Update is called once per frame
    void Update()
    {
    }





    public void TestWithTypeCast() {
        TestClass test = new TestClass2();
        TestClass2 test2 = (TestClass2)test;
    }

    public void TestWithoutTypeCast() {
        TestClass2 test = new TestClass2();
    }





    public interface TestClass{
        void Test();
    }


    public class TestClass1 : TestClass{

        public virtual void Test() {
            int i = 0;
        }
    }

    public class TestClass2 : TestClass{
        public void Test() {
            int i = 0;
        }
    }









}
