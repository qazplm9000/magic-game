using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVariables
{

    private Dictionary<string, int> intValues;
    private Dictionary<string, float> floatValues;
    private Dictionary<string, string> stringValues;
    private Dictionary<string, bool> boolValues;
    private Dictionary<string, object> objectValues;

    public CharacterVariables() {
        intValues = new Dictionary<string, int>();
        floatValues = new Dictionary<string, float>();
        stringValues = new Dictionary<string, string>();
        boolValues = new Dictionary<string, bool>();
        objectValues = new Dictionary<string, object>();
    }


    public int GetInt(string key) {
        return intValues[key];
    }

    public void SetKey(string key, int value) {
        intValues[key] = value;
    }

    public void DeleteIntKey(string key) {
        intValues.Remove(key);
    }



    public float GetFloat(string key)
    {
        return floatValues[key];
    }

    public void SetKey(string key, float value)
    {
        floatValues[key] = value;
    }

    public void DeleteFloatKey(string key)
    {
        floatValues.Remove(key);
    }


    public string GetString(string key)
    {
        return stringValues[key];
    }

    public void SetKey(string key, string value)
    {
        stringValues[key] = value;
    }

    public void DeleteStringKey(string key)
    {
        stringValues.Remove(key);
    }


    public bool GetBool(string key)
    {
        return boolValues[key];
    }

    public void SetKey(string key, bool value)
    {
        boolValues[key] = value;
    }

    public void DeleteBoolKey(string key)
    {
        boolValues.Remove(key);
    }



    public object GetObject(string key)
    {
        return objectValues[key];
    }

    public void SetKey(string key, object value)
    {
        objectValues[key] = value;
    }

    public void DeleteObjectKey(string key)
    {
        objectValues.Remove(key);
    }


    


}
