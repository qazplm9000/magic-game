using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass
{
    public int testInt = 0;
    public float testFloat = 0;
    public string testString = "";
    public long testLong;
    public long testLong1;
    public long testLong2;
    public long testLong3;
    public long testLong4;
    public long testLong5;

    public TestClass() {
        testInt = 0;
        testFloat = 0;
        testString = "";
        testLong = 0;
        testLong1 = 0;
        testLong2 = 0;
        testLong3 = 0;
        testLong4 = 0;
        testLong5 = 0;
    }

    public void Test() {
        testInt++;
    }
}
