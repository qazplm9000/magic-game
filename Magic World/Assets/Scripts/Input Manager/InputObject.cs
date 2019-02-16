using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Inputs/Input Object")]
    public class InputObject : ScriptableObject
    {

        public List<InputAxis> axes;
        public List<InputKey> keys;



    }
}