using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mike4ruls.General.Items;
using Mike4ruls.General.Player;
using Mike4ruls.General.UI;

namespace Mike4ruls.General.Managers
{
    public class InventoryMenuManager : MonoBehaviour
    {
        public PoolManager equipmentPoolManager;

        public PoolManager inventoryPoolManager;

        public Vector3 weaponSpacing = new Vector3(0, -25, 0);
        public Vector3 sheildSpacing = new Vector3(0, -25, 0);
        public Vector3 inventorySpacing = new Vector3(0, -50, 0);

        private bool dropItem = false;
        private PlayerBase _playerBase;
        private PlayerInventory _playerInventory;
        bool initFinished = false;
        // Use this for initialization

        private void OnEnable()
        {
            if (initFinished)
            {
                UpdateUI();
                SetSpacing();
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (initFinished)
            {
                if (ItemIconScript.dragEnded)
                {
                    Item itemToDrop = ItemIconScript.swap1.GetItem();
                    if (dropItem && itemToDrop != null)
                    {
                        if (ItemIconScript.swap1.GetParent() == equipmentPoolManager.gameObject)
                        {
                            switch (itemToDrop.itemType)
                            {
                                case ItemType.Weapon:
                                    {
                                        _playerInventory.DropEquippedWeapon(ItemIconScript.swap1.transform.GetSiblingIndex());
                                        break;
                                    }
                                case ItemType.Sheild:
                                    {
                                        _playerInventory.DropCurrentlyEquippedSheild();
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            _playerInventory.DropItem(itemToDrop);
                        }
                        UpdateUI();
                        dropItem = false;
                    }
                    ItemIconScript.dragEnded = false;
                }
                if (ItemIconScript.rdyToSwap)
                {
                    Item item1 = ItemIconScript.swap1.GetItem();
                    Item item2 = ItemIconScript.swap2.GetItem();

                    bool slotCheckOk = true;

                    if (ItemIconScript.swap1.GetParent() == inventoryPoolManager.gameObject && ItemIconScript.swap2.GetParent() == inventoryPoolManager.gameObject)
                    {
                        slotCheckOk = false;
                    }

                    bool isAnOpenSlot = item1 == null || item2 == null;
                    bool isSameItemType = false;

                    if (!isAnOpenSlot)
                    {
                        isSameItemType = item1.itemType == item2.itemType;
                    }
                    else if(item1 != null || item2 != null)
                    {
                        if (ItemIconScript.swap1.GetParent() == equipmentPoolManager.gameObject && ItemIconScript.swap2.GetParent() == equipmentPoolManager.gameObject)
                        {
                            if (ItemIconScript.swap1.transform.GetSiblingIndex() > 3 || ItemIconScript.swap2.transform.GetSiblingIndex() > 3)
                            {
                                slotCheckOk = false;
                            }
                        }
                        else if(slotCheckOk)
                        {
                            ItemIconScript onEquipmentSideIcon = ItemIconScript.swap1.GetParent() == equipmentPoolManager.gameObject ? ItemIconScript.swap1: ItemIconScript.swap2;
                            ItemIconScript hasItemIcon = item1 == null ? ItemIconScript.swap2 : ItemIconScript.swap1;
                            //ItemIconScript doestHaveItemIcon = item1 == null ? ItemIconScript.swap1 : ItemIconScript.swap2;

                            if (onEquipmentSideIcon != hasItemIcon)
                            {
                                switch (hasItemIcon.GetItem().itemType)
                                {
                                    case ItemType.Weapon:
                                        {
                                            if (onEquipmentSideIcon.transform.GetSiblingIndex() > 3)
                                            {
                                                slotCheckOk = false;
                                            }
                                            break;
                                        }
                                    case ItemType.Sheild:
                                        {
                                            if (onEquipmentSideIcon.transform.GetSiblingIndex() < 4)
                                            {
                                                slotCheckOk = false;
                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            slotCheckOk = false;
                                            break;
                                        }
                                }
                            }
                        }
                    }

                    if (slotCheckOk && (isSameItemType || isAnOpenSlot))
                    {
                        int icon1Index = ItemIconScript.swap1.transform.GetSiblingIndex();
                        int icon2Index = ItemIconScript.swap2.transform.GetSiblingIndex();

                        Transform parent = ItemIconScript.swap1.transform.parent;
                        ItemIconScript.swap1.transform.SetParent(ItemIconScript.swap2.transform.parent);
                        ItemIconScript.swap2.transform.SetParent(parent);

                        ItemIconScript.swap1.transform.SetSiblingIndex(icon2Index);
                        ItemIconScript.swap2.transform.SetSiblingIndex(icon1Index);
                        UpdatePlayerInventory();
                        ItemIconScript.SwapItemIcons();
                        UpdateUI();
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
            equipmentPoolManager.transform.GetChild(0).GetComponent<ItemIconScript>().StoreItem(_playerInventory.GetWeaponFromHolster(0));
            equipmentPoolManager.transform.GetChild(1).GetComponent<ItemIconScript>().StoreItem(_playerInventory.GetWeaponFromHolster(1));
            equipmentPoolManager.transform.GetChild(2).GetComponent<ItemIconScript>().StoreItem(_playerInventory.GetWeaponFromHolster(2));
            equipmentPoolManager.transform.GetChild(3).GetComponent<ItemIconScript>().StoreItem(_playerInventory.GetWeaponFromHolster(3));
            equipmentPoolManager.transform.GetChild(4).GetComponent<ItemIconScript>().StoreItem(_playerInventory.GetCurrentSheild());

            equipmentPoolManager.transform.GetChild(0).GetComponent<ItemIconScript>().emptyText = "WEAPON SLOT 1";
            equipmentPoolManager.transform.GetChild(1).GetComponent<ItemIconScript>().emptyText = "WEAPON SLOT 2";
            equipmentPoolManager.transform.GetChild(2).GetComponent<ItemIconScript>().emptyText = "WEAPON SLOT 3";
            equipmentPoolManager.transform.GetChild(3).GetComponent<ItemIconScript>().emptyText = "WEAPON SLOT 4";
            equipmentPoolManager.transform.GetChild(4).GetComponent<ItemIconScript>().emptyText = "SHEILD SLOT";


            for (int i = 0; i < inventoryPoolManager.transform.childCount; i++)
            {
                ItemIconScript icon = inventoryPoolManager.transform.GetChild(i).GetComponent<ItemIconScript>();
                icon.StoreItem(null);
                icon.emptyText = "NONE";
            }

            int iconCount = 0;

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

            equipmentPoolManager.Initialize(5, false);
            inventoryPoolManager.Initialize(_playerInventory.GetMaxInventorySpace(), false);
            SetSpacing();
            initFinished = true;
        }
        public void SetSpacing()
        {
            Vector3 rectPosition = equipmentPoolManager.transform.position;
            equipmentPoolManager.transform.GetChild(0).GetComponent<ItemIconScript>().SetLastPosition(rectPosition);
            equipmentPoolManager.transform.GetChild(1).GetComponent<ItemIconScript>().SetLastPosition(rectPosition + (weaponSpacing * 1));
            equipmentPoolManager.transform.GetChild(2).GetComponent<ItemIconScript>().SetLastPosition(rectPosition + (weaponSpacing * 2));
            equipmentPoolManager.transform.GetChild(3).GetComponent<ItemIconScript>().SetLastPosition(rectPosition + (weaponSpacing * 3));
            equipmentPoolManager.transform.GetChild(4).GetComponent<ItemIconScript>().SetLastPosition(rectPosition + (weaponSpacing * 3) + sheildSpacing);

            for (int i = 0; i < inventoryPoolManager.transform.childCount; i++)
            {
                ItemIconScript obj = inventoryPoolManager.transform.GetChild(i).gameObject.GetComponent<ItemIconScript>();
                obj.SetLastPosition(inventoryPoolManager.GetComponent<RectTransform>().position + (inventorySpacing * i));
            }
        }
        void UpdatePlayerInventory()
        {
            for (int i = 0; i < 4; i++)
            {
                Item newItem = equipmentPoolManager.transform.GetChild(i).GetComponent<ItemIconScript>().GetItem();
                _playerInventory.EquipWeapon((GunBase)newItem, i);
            }
            _playerInventory.EquipSheild((SheildBase)equipmentPoolManager.transform.GetChild(4).GetComponent<ItemIconScript>().GetItem());
        }
        public void StartDroppingItem()
        {
            dropItem = true;
        }
        public void StopDroppingItem()
        {
            dropItem =false;
        }
    }
}
