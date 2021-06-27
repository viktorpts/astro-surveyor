using System.Collections.Generic;
using UnityEngine;


namespace AstroSurveyor
{
    public class Inventory : MonoBehaviour
    {
        public Dictionary<string, GameObject> slots;
        Dictionary<string, int> indexMap;


        void Start()
        {
            slots = new Dictionary<string, GameObject>();
            slots.Add("Inventory 1", null);
            slots.Add("Inventory 2", null);
            slots.Add("Inventory 3", null);
            slots.Add("Inventory 4", null);

            indexMap = new Dictionary<string, int>();
            indexMap.Add("Inventory 1", 0);
            indexMap.Add("Inventory 2", 1);
            indexMap.Add("Inventory 3", 2);
            indexMap.Add("Inventory 4", 3);
        }

        public bool PutAway(GameObject item, string slotIndex)
        {
            if (slots[slotIndex] != null)
            {
                return false;
            }
            else
            {
                slots[slotIndex] = item;
                item.SetActive(false);
                GameManager.Instance.ShowMessage($"Placed item in slot {slotIndex}");
                GameManager.Instance.UpdateInventory(item, indexMap[slotIndex]);
                return true;
            }
        }

        public GameObject TakeOut(string slotIndex)
        {
            if (slots[slotIndex] == null)
            {
                return null;
            }
            else
            {
                var item = slots[slotIndex];
                slots[slotIndex] = null;
                item.transform.position = gameObject.transform.position;
                item.SetActive(true);
                GameManager.Instance.ShowMessage($"Retrieved item from slot {slotIndex}");
                GameManager.Instance.UpdateInventory(null, indexMap[slotIndex]);
                return item;
            }
        }
    }
}