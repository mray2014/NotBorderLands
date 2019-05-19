using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

    //[HideInInspector]
    public GameObject gunSpawnPoint;
    public float holdDownToEquipTime = 1;
    public float throwItemForce = 4; 
    public bool hitInteractable = false;
    Camera playerCamera;


    GunBase[] weaponHolster;

    List<Item> weaponsInventory;
    List<Item> sheildsInventory;
    List<Item> itemsInventory;

    int curWeapon = 0;

    int currentInventorySpace = 0;
    int maxInventorySpace = 10;
    bool startEquipTimeCheck = false;

    [HideInInspector]
    public bool playerActive = true;

    PlayerMovement _playerMovement;

    float pickUpTimer = 0.0f;
    // Use this for initialization
    void Start () {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        _playerMovement = GetComponent<PlayerMovement>();
        weaponHolster = new GunBase[4];
        weaponsInventory = new List<Item>();
        sheildsInventory = new List<Item>();
        itemsInventory = new List<Item>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (playerActive)
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
    public void SetPlayerActive(bool active)
    {
        playerActive = active;

        _playerMovement.enabled = active;
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
                if (item.pullIn)
                {
                    return;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (item.isEquippable)
                    {
                        startEquipTimeCheck = true;
                    }
                }
                else if (Input.GetKeyUp(KeyCode.E))
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
                startEquipTimeCheck = false;
                hitInteractable = false;
            }
        }
        else
        {
            startEquipTimeCheck = false;
            hitInteractable = false;
        }
    }

    public void PickUpItem(Item itemToPickUp)
    {
        switch (itemToPickUp.itemType)
        {
            case Item.ItemType.Weapon:
                {
                    weaponsInventory.Add(itemToPickUp);
                    break;
                }
            case Item.ItemType.Sheild:
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
            case Item.ItemType.Weapon:
                {
                    EquipWeapon((GunBase)equipItem);
                    break;
                }
            case Item.ItemType.Sheild:
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

}
