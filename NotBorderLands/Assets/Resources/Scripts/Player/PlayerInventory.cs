using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mike4ruls.General.Items;

namespace Mike4ruls.General.Player
{
    public class PlayerInventory : MonoBehaviour
    {

        // Public Vars
        public GameObject weaponsInventoryObj;
        public GameObject sheildInventoryObj;
        public GameObject itemInventoryObj;
        public GameObject gunSpawnPoint;
        public float holdDownToEquipTime = 1;
        public float throwItemForce = 4;
        public bool hitInteractable = false;

        // Private Vars 
        private GunBase[] weaponHolster;
        private PlayerBase _playerBase;
        private Camera playerCamera;
        private SheildBase curSheild = null;
        private int curWeapon = 0;
        private int currentInventorySpace = 0;
        private int maxInventorySpace = 10;
        private float pickUpTimer = 0.0f;
        private bool startEquipTimeCheck = false;
        private bool unHoverStillPressingECheck = false;



        // Use this for initialization
        void Start()
        {
            playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
            _playerBase = GetComponent<PlayerBase>();
            weaponHolster = new GunBase[4];
        }

        // Update is called once per frame
        void Update()
        {
            currentInventorySpace =  weaponsInventoryObj.transform.childCount + 
                                     sheildInventoryObj.transform.childCount + 
                                     itemInventoryObj.transform.childCount;


            if (_playerBase.GetPlayerActive())
            {
                if (Input.GetKeyDown(KeyCode.Tab) && weaponHolster[curWeapon] != null)
                {
                    DropCurrentlyEquippedWeapon();
                }
                if (Input.GetKeyDown(KeyCode.P) && curSheild != null)
                {
                    DropCurrentlyEquippedSheild();
                }

                if (startEquipTimeCheck)
                {
                    pickUpTimer += Time.deltaTime;
                }
                else
                {
                    pickUpTimer = 0;
                }
                CheckPlayerInput();
                CheckForInteraction();
            }


        }
        #region Input Check Functionality

        void CheckPlayerInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                LeftToggleWeaponHolster();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                RightToggleWeaponHolster();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetWeaponHolster(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetWeaponHolster(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetWeaponHolster(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetWeaponHolster(3);
            }
        }

        void CheckForInteraction()
        {
            RaycastHit hit;
            Ray newRay = new Ray(transform.position, playerCamera.transform.forward);

            if (Physics.Raycast(newRay, out hit, 500))
            {
                if (hit.transform.gameObject.GetComponent<Item>() != null)
                {
                    hitInteractable = true;
                    Item item = hit.transform.GetComponent<Item>();
                    if (item.IsPullingIn())
                    {
                        return;
                    }
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        unHoverStillPressingECheck = true;
                        if (item.isEquippable)
                        {
                            startEquipTimeCheck = true;
                        }
                    }
                    else if (Input.GetKeyUp(KeyCode.F) && unHoverStillPressingECheck)
                    {
                        PickUpItem(item);
                        startEquipTimeCheck = false;
                        pickUpTimer = 0;
                    }

                    if ((pickUpTimer >= holdDownToEquipTime))
                    {
                        QuickEquipItem(item);
                        startEquipTimeCheck = false;
                    }
                }
                else
                {
                    unHoverStillPressingECheck = false;
                    startEquipTimeCheck = false;
                    hitInteractable = false;
                }
            }
            else
            {
                unHoverStillPressingECheck = false;
                startEquipTimeCheck = false;
                hitInteractable = false;
            }
        }

        #endregion

        #region Weapon Toggle Functionality

        void LeftToggleWeaponHolster()
        {
            int toggle = curWeapon - 1;
            if (toggle < 0)
            {
                toggle = 3;
            }
            SetWeaponHolster(toggle);
        }
        void RightToggleWeaponHolster()
        {
            int toggle = curWeapon + 1;
            if (toggle > 3)
            {
                toggle = 0;
            }
            SetWeaponHolster(toggle);
        }
        void SetWeaponHolster(int weaponSlot)
        {
            if (weaponHolster[curWeapon] != null)
            {
                weaponHolster[curWeapon].gameObject.SetActive(false);
            }

            curWeapon = weaponSlot;

            GunBase gunToEquip = weaponHolster[curWeapon];
            if (gunToEquip != null)
            {
                gunToEquip.transform.parent = gunSpawnPoint.transform;
                gunToEquip.PickUp(gunSpawnPoint.transform.position);
                gunToEquip.transform.forward = playerCamera.transform.forward;
                gunToEquip.gameObject.SetActive(true);
            }
        }

        #endregion

        #region Item PickUp/Drop Functionality

        public void PickUpItem(Item itemToPickUp)
        {
            if (currentInventorySpace >= maxInventorySpace)
            {
                return;
            }
            switch (itemToPickUp.itemType)
            {
                case ItemType.Weapon:
                    {
                        itemToPickUp.transform.parent = weaponsInventoryObj.transform;
                        break;
                    }
                case ItemType.Sheild:
                    {
                        itemToPickUp.transform.parent = sheildInventoryObj.transform;
                        break;
                    }
                default:
                    {
                        itemToPickUp.transform.parent = itemInventoryObj.transform;
                        break;
                    }
            }
            itemToPickUp.PickUp(this.gameObject);
        }

        public void DropItem(Item itemToDrop)
        {
            Vector3 throwDir = new Vector3(playerCamera.transform.forward.x, 1, playerCamera.transform.forward.z) * throwItemForce;
            itemToDrop.ThrowAway(gunSpawnPoint.transform.position, throwDir);
        }
        #endregion

        #region Equipping Item Functionality
        void QuickEquipItem(Item equipItem)
        {
            switch (equipItem.itemType)
            {
                case ItemType.Weapon:
                    {
                        QuickEquipWeapon((GunBase)equipItem);
                        break;
                    }
                case ItemType.Sheild:
                    {
                        QuickEquipSheild((SheildBase)equipItem);
                        break;
                    }
            }
        }

        #region Sheild Equipping Functionality
        void EquipSheild(SheildBase sheildToEquip)
        {
            if (curSheild != null)
            {
                StowAwayEquippedSheild();
            }
            curSheild = sheildToEquip;
            curSheild.transform.parent = sheildInventoryObj.transform;
        }
        void QuickEquipSheild(SheildBase sheildToEquip)
        {
            if (curSheild != null)
            {
                DropCurrentlyEquippedSheild();
            }
            curSheild = sheildToEquip;
            curSheild.transform.parent = sheildInventoryObj.transform;
            StowAwayEquippedSheild();
            curSheild.gameObject.SetActive(true);
        }
        void StowAwayEquippedSheild()
        {
            if (curSheild != null)
            {
                curSheild.PickUp(new Vector3(0, 9999, 0));
            }
        }
        void DropCurrentlyEquippedSheild()
        {
            Item sheild = curSheild;
            curSheild = null;
            DropItem(sheild);
        }
        #endregion

        #region WeaponFunctionality
        void EquipWeapon(GunBase gunToEquip, int slot)
        {
            if (weaponHolster[slot] != null)
            {
                StowAwayEquippedWeapon(slot);
            }
            gunToEquip.transform.parent = gunSpawnPoint.transform;
            weaponHolster[slot] = gunToEquip;
            SetWeaponHolster(curWeapon);
        }
        void QuickEquipWeapon(GunBase gunToEquip)
        {
            if (weaponHolster[curWeapon] != null)
            {
                DropCurrentlyEquippedWeapon();
            }
            gunToEquip.transform.parent = gunSpawnPoint.transform;
            weaponHolster[curWeapon] = gunToEquip;
            SetWeaponHolster(curWeapon);
        }
        void StowAwayEquippedWeapon()
        {
            if (weaponHolster[curWeapon] != null)
            {
                weaponHolster[curWeapon].transform.parent = weaponsInventoryObj.transform;
                weaponHolster[curWeapon].PickUp(new Vector3(0,9999,0));
            }
        }
        void StowAwayEquippedWeapon(int slot)
        {
            if (weaponHolster[slot] != null)
            {
                weaponHolster[slot].transform.parent = weaponsInventoryObj.transform;
                weaponHolster[curWeapon].PickUp(new Vector3(0, 9999, 0));
            }
        }
        void DropCurrentlyEquippedWeapon()
        {
            Item weapon = weaponHolster[curWeapon];
            weaponHolster[curWeapon] = null;
            DropItem(weapon);
        }
        #endregion


        #endregion


        #region Getters and Setters
        public SheildBase GetCurrentSheild()
        {
            return curSheild;
        }
        public int GetMaxInventorySpace()
        {
            return maxInventorySpace;
        }
        public bool IsWearingASheild()
        {
            if (curSheild == null)
            {
                return false;
            }
            return true;
        }
        public Item GetWeaponFromHolster(int pos)
        {
            if (weaponHolster[pos] != null)
            {
                return weaponHolster[pos];
            }
            return null;
        }
        #endregion
    }
}
