using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool pool;
    public Dictionary<GameObject, List<GameObject>> objectPool = new Dictionary<GameObject, List<GameObject>>();

	// Use this for initialization
	void Start () {
        if (pool == null)
        {
            pool = this;
        }
        else {
            Destroy(this);
        }
	}

    /// <summary>
    /// Adds the object to the pool and returns the last result
    /// </summary>
    /// <param name="newObject"></param>
    /// <param name="number"></param>
    public GameObject AddObject(GameObject newObject, int number = 1) {

        GameObject result = null;

        if (!objectPool.ContainsKey(newObject)) {
            objectPool[newObject] = new List<GameObject>();
        }

        for (int i = 0; i < number; i++) {
            GameObject instantiatedObject = Instantiate(newObject);
            instantiatedObject.SetActive(false);
            objectPool[newObject].Add(instantiatedObject);
            result = instantiatedObject;
        }

        return result;
    }


    /// <summary>
    /// Grabs the object from the pool
    /// </summary>
    /// <param name="thisObject"></param>
    public GameObject PullObject(GameObject thisObject) {

        GameObject result = null;

        //creates the object if it does not exist in the pool
        if (!objectPool.ContainsKey(thisObject)) {
            result = AddObject(thisObject);
        }

        List<GameObject> objectList = objectPool[thisObject];

        //try to find object
        if (result == null)
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                GameObject currentObject = objectList[i];

                if (!currentObject.active)
                {
                    result = currentObject;
                    break;
                }
            }
        }

        //If no inactive objects, create a new object
        if (result == null) {
            result = AddObject(thisObject);
        }

        result.SetActive(true);

        return result;
    }



    public GameObject PullObject(GameObject thisObject, Transform location) {
        GameObject result = PullObject(thisObject);
        result.transform.position = location.position;
        result.transform.rotation = location.rotation;

        return result;
    }

    public void RemoveObject(GameObject thisObject) {
        thisObject.SetActive(false);
        thisObject.transform.position = transform.position;
    }

    public static Behaviour GetComponentFromObject<Behaviour>(GameObject gameObject, Behaviour behaviour) where Behaviour : MonoBehaviour{
        Behaviour component = null;

        Component[] behaviours = gameObject.GetComponents(behaviour.GetType());

        for (int i = 0; i < behaviours.Length; i++) {
            if (behaviours[i] == behaviour) {
                component = (Behaviour)behaviours[i];
                break;
            }
        }

        if (component == null) {
            component = (Behaviour)gameObject.AddComponent(behaviour.GetType());
        }

        return component;
    }

}
