using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mike4ruls.Items;

namespace Mike4ruls.Player
{
    public class PlayerInventory : MonoBehaviour
    {

        // Public Vars 
        public GameObject gunSpawnPoint;
        public float holdDownToEquipTime = 1;
        public float throwItemForce = 4;
        public bool hitInteractable = false;

        public List<Item> weaponsInventory;
        public List<Item> sheildsInventory;
        public List<Item> itemsInventory;

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
            weaponsInventory = new List<Item>();
            sheildsInventory = new List<Item>();
            itemsInventory = new List<Item>();
        }

        // Update is called once per frame
        void Update()
        {

            if (_playerBase.GetPlayerActive())
            {
                CheckForInteraction();
                if (Input.GetKeyDown(KeyCode.Tab) && weaponHolster[curWeapon] != null)
                {
                    DropCurrentlyEquippedWeapon();
                }

                if (startEquipTimeCheck)
                {
                    pickUpTimer += Time.deltaTime;
                }
                else
                {
                    pickUpTimer = 0;
                }
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
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        unHoverStillPressingECheck = true;
                        if (item.isEquippable)
                        {
                            startEquipTimeCheck = true;
                        }
                    }
                    else if (Input.GetKeyUp(KeyCode.E) && unHoverStillPressingECheck)
                    {
                        PickUpItem(item);
                        startEquipTimeCheck = false;
                        pickUpTimer = 0;
                    }

                    if ((pickUpTimer >= holdDownToEquipTime))
                    {
                        EquipItem(item);
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
                        weaponsInventory.Add(itemToPickUp);
                        break;
                    }
                case ItemType.Sheild:
                    {
                        sheildsInventory.Add(itemToPickUp);
                        break;
                    }
                default:
                    {
                        itemsInventory.Add(itemToPickUp);
                        break;
                    }
            }
            currentInventorySpace++;
            itemToPickUp.PickUp(this.gameObject);
        }
        void EquipItem(Item equipItem)
        {
            switch (equipItem.itemType)
            {
                case ItemType.Weapon:
                    {
                        EquipWeapon((GunBase)equipItem);
                        break;
                    }
                case ItemType.Sheild:
                    {

                        break;
                    }
            }
        }

        void EquipWeapon(GunBase gunToEquip)
        {
            if (weaponHolster[curWeapon] != null)
            {

                DropCurrentlyEquippedWeapon();

                //Stowing Weapon in inventory
                //weaponHolster[curWeapon].transform.parent = null;
                //PickUpItem(weaponHolster[curWeapon]);
            }
            weaponHolster[curWeapon] = gunToEquip;
            gunToEquip.transform.parent = gunSpawnPoint.transform;
            gunToEquip.PickUp(gunSpawnPoint.transform.position);
            gunToEquip.transform.forward = playerCamera.transform.forward;
        }
        public void DropItem(Item itemToPickUp)
        {
            Vector3 throwDir = new Vector3(playerCamera.transform.forward.x, 1, playerCamera.transform.forward.z) * throwItemForce;
            itemToPickUp.ThrowAway(gunSpawnPoint.transform.position, throwDir);
        }
        void DropCurrentlyEquippedWeapon()
        {
            Item weapon = weaponHolster[curWeapon];
            weaponHolster[curWeapon] = null;
            DropItem(weapon);
        }
        public SheildBase GetCurrentSheild()
        {
            return curSheild;
        }
        public bool IsWearingASheild()
        {
            if (curSheild == null)
            {
                return false;
            }
            return true;
        }
    }
}
