using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectPool<T> : ISerializationCallbackReceiver where T : MonoBehaviour {

    public Dictionary<GameObject, List<T>> objectPool;
    public Vector3 defaultPosition; //where objects are hidden


    //GUI Layout functions
    private bool foldout = false;






    public ObjectPool(){
        objectPool = new Dictionary<GameObject, List<T>>();
        defaultPosition = Vector3.zero;
    }

    public ObjectPool(Vector3 newPosition)
    {
        objectPool = new Dictionary<GameObject, List<T>>();
        defaultPosition = newPosition;
    }



    /// <summary>
    /// Adds the object to the pool and returns the last result
    /// </summary>
    /// <param name="newObject"></param>
    /// <param name="number"></param>
    public GameObject AddObject(GameObject newObject, int number = 1) {

        GameObject result = null;

        if (!objectPool.ContainsKey(newObject)) {
            objectPool[newObject] = new List<T>();
        }

        for (int i = 0; i < number; i++) {
            GameObject instantiatedObject = Object.Instantiate(newObject, defaultPosition, Quaternion.Euler(0,0,0));
            instantiatedObject.SetActive(false);
            T scriptReference = instantiatedObject.AddComponent<T>();

            objectPool[newObject].Add(scriptReference);
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

        List<T> objectList = objectPool[thisObject];

        //try to find object
        if (result == null)
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                T currentObject = objectList[i];

                if (!currentObject.gameObject.activeInHierarchy)
                {
                    result = currentObject.gameObject;
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
        thisObject.transform.position = defaultPosition;
    }

    /// <summary>
    /// Finds the game object and returns the attached monobehaviour
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public T PullObjectBehaviour(GameObject go) {

        if (objectPool.ContainsKey(go))
        {
            List<T> objectList = objectPool[go];

            for (int i = 0; i < objectList.Count; i++) {
                if (!objectList[i].gameObject.activeInHierarchy) {
                    objectList[i].gameObject.SetActive(true);
                    return objectList[i];
                }
            }
        }

        GameObject newGo = AddObject(go);
        newGo.SetActive(true);
        return newGo.GetComponent<T>();

    }

    public void OnBeforeSerialize()
    {
        EditorGUILayout.BeginVertical();
        foldout = EditorGUILayout.Foldout(foldout, "Pooled Objects");
        
        

        EditorGUILayout.EndVertical();
    }

    public void OnAfterDeserialize()
    {
        
    }
}
