using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ItemSystem
{
    [System.Serializable]
    public class ItemSlot : ISerializationCallbackReceiver
    {
        public string description;
        public Item item;
        public bool requiresSpecificItemType = false;
        public ItemType type;
        public int numberOfItems = 0;



        public void PutItemInSlot(Item item, int num = 1)
        {
            this.item = item;
            numberOfItems = num;
        }

        public void IncreaseStack(int num)
        {
            numberOfItems += num;
        }


        public Item GetItem()
        {
            return item;
        }

        public bool CanPutInSlot(Item item)
        {
            bool result = false;

            if (requiresSpecificItemType)
            {
                result = type == item.GetType();
            }
            else
            {
                result = true;
            }

            return result;
        }

        public bool ContainsTheSameItem(Item item)
        {
            return item == this.item;
        }


        /*
            Description
             */

        public void OnAfterDeserialize()
        {
            if (Application.isEditor)
            {
                if (!requiresSpecificItemType)
                {
                    description = "Generic Item Slot";
                }
                else
                {
                    description = $"{type.ToString()} Slot";
                }
            }
        }

        public void OnBeforeSerialize()
        {
            
        }
    }


    public class InventoryManager : MonoBehaviour
    {

        public List<ItemSlot> items = new List<ItemSlot>();
        public InventoryUI ui;
        public Image pointer;
        public float inventoryIndex;
        public float pointerSpeed = 1;
        public int pointerOffset = 50;
        public Sprite nullSprite;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            for(int i = 0; i < items.Count; i++)
            {
                if (items[i].item != null)
                {
                    ui.UpdateSlot(i, items[i].item.icon);
                }
                else
                {
                    ui.UpdateSlot(i, nullSprite);
                }
            }

            SetPointerPosition((int)inventoryIndex);

            inventoryIndex += Input.GetAxis("Controller DPad X") * Time.deltaTime * pointerSpeed;
            inventoryIndex = Mathf.Max(0, inventoryIndex);

            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                UseItem((int)inventoryIndex);
            }

            if (Input.GetKeyDown(KeyCode.Joystick1Button13))
            {
                ui.gameObject.SetActive(!ui.gameObject.activeInHierarchy);
                pointer.gameObject.SetActive(!pointer.gameObject.activeInHierarchy);
            }
        }

        public void SetPointerPosition(int index)
        {
            RectTransform trans = ui.GetSlotPosition(index);
            pointer.rectTransform.position = trans.position + new Vector3(0, pointerOffset, 0);
        }

        private void UseItem(int index)
        {
            ItemSlot slot = items[index];
            if (slot.item != null && slot.item.type == ItemType.Consumable)
            {
                //slot.item.Use();
                slot.numberOfItems--;
                if(slot.numberOfItems == 0)
                {
                    slot.item = null;
                }
            }
        }
    }
}