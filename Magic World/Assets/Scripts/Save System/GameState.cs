using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace SaveSystem
{
    public class GameState
    {
        private VariableDictionary<int> intVars;
        private VariableDictionary<float> floatVars;
        private VariableDictionary<string> stringVars;
        private VariableDictionary<bool> boolVars;

        public GameState()
        {
            intVars = new VariableDictionary<int>();
            floatVars = new VariableDictionary<float>();
            stringVars = new VariableDictionary<string>();
            boolVars = new VariableDictionary<bool>();
        }




        public void Save(string filename) {
            

            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(filename, FileMode.CreateNew);

                SaveObject save = new SaveObject
                {
                    intVars = intVars,
                    floatVars = floatVars,
                    stringVars = stringVars,
                    boolVars = boolVars
                };

                bf.Serialize(stream, save);
                stream.Close();
            }
            catch (Exception e) {
                Debug.Log(e);
            }
        }

        public void Load(string filename) {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(filename, FileMode.Open);

                SaveObject save = (SaveObject)bf.Deserialize(stream);
                stream.Close();

                intVars = save.intVars;
                floatVars = save.floatVars;
                stringVars = save.stringVars;
                boolVars = save.boolVars;
            }
            catch (Exception e) {
                Debug.Log(e);
            }
        }




        private class SaveObject {
            string version;
            public VariableDictionary<int> intVars;
            public VariableDictionary<float> floatVars;
            public VariableDictionary<string> stringVars;
            public VariableDictionary<bool> boolVars;
        }





        #region Getters and Setters


        /// <summary>
        /// Gets the value
        /// Sets the value to the default if it does not exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetInt(string name)
        {
            return intVars.GetValueOrDefault(name);
        }

        /// <summary>
        /// Gets the value
        /// Sets the value to the default if it does not exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public float GetFloat(string name)
        {
            return floatVars.GetValueOrDefault(name);
        }

        /// <summary>
        /// Gets the value
        /// Sets the value to the default if it does not exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetString(string name)
        {
            return stringVars.GetValueOrDefault(name);
        }

        /// <summary>
        /// Gets the value
        /// Sets the value to the default if it does not exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetBool(string name)
        {
            return boolVars.GetValueOrDefault(name);
        }

        
        public void SetValue(string name, int value) {
            intVars.SetValue(name, value);
        }

        public void SetFloat(string name, float value) {
            floatVars.SetValue(name, value);
        }

        public void SetString(string name, string value) {
            stringVars.SetValue(name, value);
        }

        public void SetBool(string name, bool value) {
            boolVars.SetValue(name, value);
        }

        #endregion

    }
}