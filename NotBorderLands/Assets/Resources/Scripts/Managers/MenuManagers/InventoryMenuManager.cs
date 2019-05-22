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

        public PoolManager inspectionPoolManager;

        public Vector3 weaponSpacing = new Vector3(0, -25, 0);
        public Vector3 sheildSpacing = new Vector3(0, -25, 0);
        public Vector3 inventorySpacing = new Vector3(0, -50, 0);

        private ItemIconScript referenceToOriginalIcon = null;
        private ItemIconScript itemBeingInspectedIcon = null;
        private int iconCount = 0;
        private bool inspectionON = false;
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
                TurnOffInspection();
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
                        ItemIconScript itemIconToDrop = ItemIconScript.swap1;
                        ItemIconScript modIcon = ItemIconScript.swap1;
                        if (inspectionON)
                        {
                            itemIconToDrop = referenceToOriginalIcon;                           
                        }
                        bool isAMod = false;
                        bool notUnderInventoryObj = itemIconToDrop.GetParent() != inventoryPoolManager.gameObject;
                        if (!notUnderInventoryObj && inspectionON)
                        {
                            isAMod = ItemIconScript.swap1.GetItem().itemType == ItemType.Mod; 
                        }
                        if (notUnderInventoryObj || isAMod)
                        {
                            switch (itemToDrop.itemType)
                            {
                                case ItemType.Weapon:
                                    {
                                        _playerInventory.DropEquippedWeapon(itemIconToDrop.transform.GetSiblingIndex());
                                        TurnOffInspection();
                                        break;
                                    }
                                case ItemType.Sheild:
                                    {
                                        _playerInventory.DropCurrentlyEquippedSheild();
                                        TurnOffInspection();
                                        break;
                                    }
                                case ItemType.Mod:
                                    {
                                        _playerInventory.DropSpecificModOnItem(itemIconToDrop.GetItem(), modIcon.transform.GetSiblingIndex()-2);
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            if (inspectionON && (ItemIconScript.swap1 == referenceToOriginalIcon || ItemIconScript.swap1 == itemBeingInspectedIcon))
                            {
                                TurnOffInspection();
                            }
                            _playerInventory.DropItem(itemToDrop);
                        }
                        UpdateUI();
                        if (inspectionON)
                        {
                            UpdateInspectionUI();
                        }
                        
                        dropItem = false;
                    }
                    ItemIconScript.dragEnded = false;
                }
                if (ItemIconScript.clicked)
                {
                    ToggleInspectEquipment(ItemIconScript.swap1);
                    ItemIconScript.clicked = false;
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

                        if (inspectionON && itemBeingInspectedIcon == ItemIconScript.swap2)
                        {
                            if (itemBeingInspectedIcon.GetItem() != item1)
                            {
                                ToggleInspectEquipment(ItemIconScript.swap1);
                            }
                            slotCheckOk = false;

                        }

                        if (isSameItemType)
                        {
                            if (item1.itemType == ItemType.Mod)
                            {
                                ModBase mod1  = item1.GetComponent<ModBase>();
                                ModBase mod2 = item2.GetComponent<ModBase>();
                                isSameItemType = mod1.modType == mod2.modType;
                            }
                        }
                        else if (item1.itemType == ItemType.Mod || item2.itemType == ItemType.Mod)
                        {
                            Item isAMod = item1.itemType == ItemType.Mod ? item1 : item2;
                            Item notAMod = item1.itemType == ItemType.Mod ? item2 : item1;

                            int hasModIndex = notAMod.HasMod((ModBase)isAMod);
                            if (hasModIndex == -1)
                            {
                                if ((int)notAMod.itemType == (int)isAMod.GetComponent<ModBase>().modType)
                                {
                                    _playerInventory.QuickEquipSpecificMod(notAMod, (ModBase)isAMod);
                                }
                            }
                           
                        }
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
                            if (inspectionON)
                            {
                                ItemIconScript onInspectionSideIcon = ItemIconScript.swap1.GetParent() == inspectionPoolManager.gameObject ? ItemIconScript.swap1 : ItemIconScript.swap2;
                                ItemIconScript hasItemIcon1 = item1 == null ? ItemIconScript.swap2 : ItemIconScript.swap1;
                                if (onInspectionSideIcon != hasItemIcon1)
                                {
                                   if (hasItemIcon1.GetItem().itemType != ItemType.Mod)
                                    {
                                        slotCheckOk = false;
                                    }
                                   else if ((int)itemBeingInspectedIcon.GetItem().itemType != (int)hasItemIcon1.GetItem().GetComponent<ModBase>().modType)
                                    {
                                        slotCheckOk = false;
                                    }
                                    
                                }
                            }
                            else
                            {
                                ItemIconScript onEquipmentSideIcon = ItemIconScript.swap1.GetParent() == equipmentPoolManager.gameObject ? ItemIconScript.swap1 : ItemIconScript.swap2;
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
                    }
                    else
                    {
                        ItemIconScript.WipeSwap();
                    }
                    UpdateUI();
                    if (inspectionON)
                    {
                        UpdateInspectionUI();
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

            iconCount = 0;

            SetItemsIntoIcons(_playerInventory.weaponsInventoryObj);

            SetItemsIntoIcons(_playerInventory.sheildInventoryObj);

            SetItemsIntoIcons(_playerInventory.weaponModInventoryObj);

            SetItemsIntoIcons(_playerInventory.sheildModInventoryObj);

            SetItemsIntoIcons(_playerInventory.itemInventoryObj);

            
        }
        void SetItemsIntoIcons(GameObject inventory)
        {
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
            inspectionPoolManager.Initialize(8, false);
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
            if (inspectionON)
            {
                for (int i = 0; i < referenceToOriginalIcon.GetItem().numOfModSlots; i++)
                {
                    Item mod = inspectionPoolManager.transform.GetChild(i+2).GetComponent<ItemIconScript>().GetItem();
                    _playerInventory.EquipMod(referenceToOriginalIcon.GetItem(), (ModBase)mod, i);
                }
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
        public void ToggleInspectEquipment(ItemIconScript itemIcon)
        {
            referenceToOriginalIcon = itemIcon;
            Item item = itemIcon.GetItem();
            inspectionON = inspectionPoolManager.gameObject.activeInHierarchy ? false : true;
            if (item == null)
            {
                return;
            }
            Vector3 centerPosition = inspectionPoolManager.transform.position;
            itemBeingInspectedIcon = inspectionPoolManager.transform.GetChild(1).GetComponent<ItemIconScript>();
            Item itemBeingInspected = itemBeingInspectedIcon.GetItem();

            if (itemBeingInspected != null && inspectionPoolManager.gameObject.activeInHierarchy)
            {
                inspectionON = itemBeingInspected != item;
            }


            UpdateInspectionUI();

            inspectionPoolManager.gameObject.SetActive(inspectionON);
            equipmentPoolManager.gameObject.SetActive(!inspectionON);
        }
        void UpdateInspectionUI()
        {
            Item item = referenceToOriginalIcon.GetItem();
            if (item == null)
            {
                return;
            }
            Vector3 centerPosition = inspectionPoolManager.transform.position;

            itemBeingInspectedIcon.SetLastPosition(centerPosition);
            itemBeingInspectedIcon.StoreItem(item);

            for (int i = 2; i < inspectionPoolManager.transform.childCount; i++)
            {
                ItemIconScript modIcon = inspectionPoolManager.transform.GetChild(i).GetComponent<ItemIconScript>();
                modIcon.StoreItem(null);
                int slotNum = i - 2;
                if (slotNum < item.numOfModSlots)
                {
                    ModBase mod = item.itemMods[slotNum];
                    if (mod != null)
                    {
                        modIcon.StoreItem(mod);
                    }
                    else
                    {
                        modIcon.emptyText = "Mod Component " + (slotNum + 1);
                    }

                    Vector3 dirToNewPos = Quaternion.Euler(0, 0, 360.0f * ((float)slotNum / (float)item.numOfModSlots)) * (Vector3.up * 75);
                    Vector3 positionOffset = new Vector3(0, -15, 0);
                    modIcon.SetLastPosition(centerPosition + positionOffset + dirToNewPos);
                }
                else
                {
                    modIcon.SetLastPosition(new Vector3(0, 9999, 0));
                }
            }
        }
        public void TurnOffInspection()
        {
            inspectionON = false;
            inspectionPoolManager.gameObject.SetActive(inspectionON);
            equipmentPoolManager.gameObject.SetActive(!inspectionON);
        }
    }
}
