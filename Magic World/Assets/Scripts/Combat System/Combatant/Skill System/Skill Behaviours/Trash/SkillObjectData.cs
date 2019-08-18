using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.SkillSystem
{
    public class SkillObjectData
    {
        public Dictionary<string, int> integers;
        public Dictionary<string, float> floats;
        public Dictionary<string, string> strings;
        public Dictionary<string, bool> bools;
        public Dictionary<string, GameObject> gameObjects;
        public Dictionary<string, object> objects;

        public SkillObjectData() {

        }

        public void AddInt(string name, int value) {
            integers[name] = value;
        }

        public void AddFloat(string name, float value) {
            floats[name] = value;
        }

        public void AddString(string name, string value) {
            strings[name] = value;
        }

        public void AddBool(string name, bool value) {
            bools[name] = value;
        }

        public void AddGameObject(string name, GameObject value) {
            gameObjects[name] = value;
        }

        public void AddObject(string name, object value) {
            objects[name] = value;
        }



        public int GetInt(string name) {
            return integers[name];
        }

        public float GetFloat(string name) {
            return floats[name];
        }

        public string GetString(string name) {
            return strings[name];
        }

        public bool GetBool(string name) {
            return bools[name];
        }

        public GameObject GetGameObject(string name) {
            return gameObjects[name];
        }

        public object GetObject(string name) {
            return objects[name];
        }




        public void ResetData() {
            integers = new Dictionary<string, int>();
            floats = new Dictionary<string, float>();
            strings = new Dictionary<string, string>();
            bools = new Dictionary<string, bool>();
            gameObjects = new Dictionary<string, GameObject>();
            objects = new Dictionary<string, object>();
        }
    }
}