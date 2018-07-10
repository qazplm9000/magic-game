using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler pooler;

    private Dictionary<GameObject, Pool> poolsDict = new Dictionary<GameObject, Pool>();


    private void Awake()
    {
        if (pooler == null)
        {
            pooler = this;
        }
        else {
            Destroy(this);
        }
    }


    public void AddObject(GameObject newObject) {
        if (poolsDict.ContainsKey(newObject))
        {
            poolsDict[newObject].AddObjects(1);
        }
        else {
            poolsDict[newObject] = new Pool(newObject);
        }
    }

    public void AddObject(GameObject newObject, int number) {
        if (poolsDict.ContainsKey(newObject))
        {
            poolsDict[newObject].AddObjects(number);
        }
        else
        {
            poolsDict[newObject] = new Pool(newObject, number);
        }
    }

    public GameObject GetObject(GameObject gameObject) {
        GameObject foundObject = null;

        if (poolsDict.ContainsKey(gameObject)) {
            foundObject = poolsDict[gameObject].GetObject();
            foundObject.SetActive(true);
        }

        return foundObject;
    }

    public GameObject GetObject(GameObject gameObject, Transform spawnLocation, Transform parent = null) {
        GameObject foundObject = GetObject(gameObject);

        foundObject.transform.position = spawnLocation.transform.position;
        foundObject.transform.rotation = spawnLocation.transform.rotation;
        foundObject.transform.SetParent(parent);

        return foundObject;
    }

    public void AddScript(GameObject gameObject, MonoBehaviour script) {
        if (poolsDict.ContainsKey(gameObject)) {
            poolsDict[gameObject].AddScript(script);
        }
    }

}
