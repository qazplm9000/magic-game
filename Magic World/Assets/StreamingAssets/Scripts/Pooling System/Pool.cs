using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool {

    public GameObject pooledObject;

    public List<GameObject> pool = new List<GameObject>();

    public List<MonoBehaviour> scripts = new List<MonoBehaviour>();

    public int poolSize = 10;

    public Pool(GameObject newObject) {
        pooledObject = newObject;
        AddObjects(poolSize);
    }

    public Pool(GameObject newObject, int newPoolSize) {
        pooledObject = newObject;
        poolSize = newPoolSize;
        AddObjects(poolSize);
    }


    public void AddObjects(int number) {
        for (int i = 0; i < number; i++) {
            pool.Add(CreateObject());
        }
    }


    public GameObject GetObject() {
        GameObject gameObject = null;

        foreach (GameObject go in pool) {
            if (!go.activeInHierarchy) {
                gameObject = go;
                break;
            }
        }

        if (gameObject == null) {
            AddObjects(1);
            gameObject = pool[pool.Count - 1];
        }

        return gameObject;
    }

    /*public void AttachScripts() {
        foreach (MonoBehaviour script in scripts) {
            foreach (GameObject go in pool) {
                MonoBehaviour newScript = (MonoBehaviour)go.AddComponent(script.GetType());
                newScript.enabled = false;
            }
        }
    }*/

        /// <summary>
        /// Adds a script to all objects
        /// </summary>
        /// <param name="script"></param>
    public void AddScript(MonoBehaviour script) {
        scripts.Add(script);

        foreach (GameObject go in pool) {
            MonoBehaviour newScript = (MonoBehaviour)go.AddComponent(script.GetType());
            newScript.enabled = false;
        }
    }

    public void AddScript<T>() where T : MonoBehaviour{
        MonoBehaviour newScript = null;

        foreach (GameObject go in pool) {
            newScript = (MonoBehaviour)go.AddComponent(typeof(T));
            newScript.enabled = false;
        }

        if (newScript != null)
        {
            scripts.Add(newScript);
        }
    }

    /// <summary>
    /// Creates a new object with all scripts attached
    /// </summary>
    private GameObject CreateObject() {
        GameObject newObject = GameObject.Instantiate(pooledObject);
        newObject.SetActive(false);

        AddScriptsToObject(newObject);

        return newObject;
    }

    /// <summary>
    /// Adds all scripts to one object
    /// </summary>
    /// <param name="newObject"></param>
    private void AddScriptsToObject(GameObject newObject) {
        foreach (MonoBehaviour script in scripts) {
            MonoBehaviour newScript = (MonoBehaviour)newObject.AddComponent(script.GetType());
            newScript.enabled = false;
        }
    }

}
