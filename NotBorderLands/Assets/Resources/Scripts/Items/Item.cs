using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public enum ItemType
    {
        Weapon,
        Sheild,
        HealthPickUp,
        SheildBattery
    }
    public enum RarityType
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public RarityType rewardType = RarityType.Common;
    public ItemType itemType = ItemType.Weapon;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
