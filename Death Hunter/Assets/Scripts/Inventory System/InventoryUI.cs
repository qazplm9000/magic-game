using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ItemSystem
{
    public class InventoryUI : MonoBehaviour
    {
        public List<Image> inventorySlots = new List<Image>();

        public void Awake()
        {
            Image[] tempSlots = transform.GetComponentsInChildren<Image>();
            inventorySlots.AddRange(tempSlots);
        }

        public void UpdateSlot(int index, Sprite image)
        {
            inventorySlots[index + 1].sprite = image;
        }

        public RectTransform GetSlotPosition(int index)
        {
            return inventorySlots[index + 1].rectTransform;
        }
    }
}
