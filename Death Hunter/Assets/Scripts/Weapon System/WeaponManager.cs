using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponManager : MonoBehaviour
    {

        public Combatant character;
        public GameObject weapon;
        public GameObject weaponLocation;

        private void Awake()
        {
            character = transform.GetComponent<Combatant>();
        }

        // Start is called before the first frame update
        void Start()
        {
            EquipWeapon();
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void EquipWeapon()
        {
            GameObject obj = WorldManager.PullObject(weapon);
            obj.transform.SetParent(weaponLocation.transform);
            obj.transform.position = weaponLocation.transform.position;
            obj.transform.rotation = weaponLocation.transform.rotation;
        }
        
    }
}