using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SaveSystem
{
    public class VariableDictionary<T>
    {
        private Dictionary<string, T> variables;

        public VariableDictionary() {
            variables = new Dictionary<string, T>();
        }

        public VariableDictionary(Dictionary<string, T> newVariables) {
            variables = new Dictionary<string, T>();

            foreach (string key in newVariables.Keys) {
                variables[key] = newVariables[key];
            }
        }

        /// <summary>
        /// Checks if a value exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ValueExists(string name) {
            return variables.ContainsKey(name);
        }

        /// <summary>
        /// Retrieves the value with the given name
        /// Raises an exception if the variable does not exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetValue(string name) {
            T result;

            if (ValueExists(name))
            {
                result = variables[name];
            }
            else {
                throw new System.Exception("The variable " + name + " does not exist in the dictionary.\n Variable Type: " + typeof(T).ToString());
            }

            return result;
        }

        /// <summary>
        /// Gets the value with the given name
        /// If the variable does not exist, set the variable to the default value and return it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetValueOrDefault(string name) {
            T result;

            if (ValueExists(name))
            {
                result = variables[name];
            }
            else
            {
                result = default(T);
                SetValue(name, result);
            }

            return result;
        }

        public void SetValue(string name, T newValue) {
            variables[name] = newValue;
        }

    }
}