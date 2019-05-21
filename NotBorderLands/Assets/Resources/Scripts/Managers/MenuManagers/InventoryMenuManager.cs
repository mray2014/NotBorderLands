using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mike4ruls.General.Player;
using Mike4ruls.General.UI;

namespace Mike4ruls.General.Managers
{
    public class InventoryMenuManager : MonoBehaviour
    {
        public ItemIconScript weaponHolsterIcon1;
        public ItemIconScript weaponHolsterIcon2;
        public ItemIconScript weaponHolsterIcon3;
        public ItemIconScript weaponHolsterIcon4;
        public ItemIconScript sheildIcon;

        public PoolManager inventoryPoolManager;

        public Vector3 weaponSpacing = new Vector3(0, -25, 0);
        public Vector3 sheildSpacing = new Vector3(0, -25, 0);
        public Vector3 inventorySpacing = new Vector3(0, -50, 0);

        private PlayerBase _playerBase;
        private PlayerInventory _playerInventory;
        bool initFinished = false;
        // Use this for initialization

        private void OnEnable()
        {
            if (initFinished)
            {
                UpdateUI();
                SetSpacing(inventorySpacing);
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (initFinished)
            {
                if (ItemIconScript.rdyToSwap)
                {
                    Item item1 = ItemIconScript.swap1.GetItem();
                    Item item2 = ItemIconScript.swap2.GetItem();

                    bool isAnOpenSlot = item1 == null || item2 == null;
                    bool isSameItemType = false;

                    if (!isAnOpenSlot)
                    {
                        isSameItemType = item1.itemType == item2.itemType;
                    }

                    if (isSameItemType || isAnOpenSlot)
                    {
                        ItemIconScript.SwapItemIcons();
                    }
                    else
                    {
                        ItemIconScript.WipeSwap();
                    }
                }
            }
        }
        void UpdateUI()
        {
            int iconCount = 0;
            weaponHolsterIcon1.StoreItem(_playerInventory.GetWeaponFromHolster(0));
            weaponHolsterIcon2.StoreItem(_playerInventory.GetWeaponFromHolster(1));
            weaponHolsterIcon3.StoreItem(_playerInventory.GetWeaponFromHolster(2));
            weaponHolsterIcon4.StoreItem(_playerInventory.GetWeaponFromHolster(3));
            sheildIcon.StoreItem(_playerInventory.GetCurrentSheild());

            GameObject inventory = _playerInventory.weaponsInventoryObj;

            for (int i = 0; i < inventory.transform.childCount; i++)
            {
                Item item = inventory.transform.GetChild(i).GetComponent<Item>();
                ItemIconScript icon = inventoryPoolManager.transform.GetChild(iconCount).GetComponent<ItemIconScript>();

                icon.StoreItem(item);
                iconCount++;
            }

            inventory = _playerInventory.sheildInventoryObj;

            for (int i = 0; i < inventory.transform.childCount; i++)
            {
                Item item = inventory.transform.GetChild(i).GetComponent<Item>();
                ItemIconScript icon = inventoryPoolManager.transform.GetChild(iconCount).GetComponent<ItemIconScript>();

                icon.StoreItem(item);
                iconCount++;
            }

            inventory = _playerInventory.itemInventoryObj;

            for (int i = 0; i < inventory.transform.childCount; i++)
            {
                Item item = inventory.transform.GetChild(i).GetComponent<Item>();
                ItemIconScript icon = inventoryPoolManager.transform.GetChild(iconCount).GetComponent<ItemIconScript>();

                icon.StoreItem(item);
                iconCount++;
            }
        }
        public void Initialize(PlayerBase player1)
        {
            _playerBase = player1;
            _playerInventory = _playerBase.GetComponent<PlayerInventory>();

            inventoryPoolManager.Initialize(_playerInventory.GetMaxInventorySpace(), false);
            SetSpacing(inventorySpacing);
            initFinished = true;
        }
        public void SetSpacing(Vector3 spacing)
        {
            Vector3 rectPosition = weaponHolsterIcon1.GetComponent<RectTransform>().position;
            weaponHolsterIcon2.SetLastPosition(rectPosition + (weaponSpacing * 1));
            weaponHolsterIcon3.SetLastPosition(rectPosition + (weaponSpacing * 2));
            weaponHolsterIcon4.SetLastPosition(rectPosition + (weaponSpacing * 3));
            sheildIcon.SetLastPosition(rectPosition + (weaponSpacing * 3) + sheildSpacing);

            for (int i = 0; i < inventoryPoolManager.transform.childCount; i++)
            {
                ItemIconScript obj = inventoryPoolManager.transform.GetChild(i).gameObject.GetComponent<ItemIconScript>();
                obj.SetLastPosition(inventoryPoolManager.GetComponent<RectTransform>().position + (spacing * i));
            }
        }
    }
}
