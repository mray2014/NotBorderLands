using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mike4ruls.General.Items;

using UnityEditor;
namespace Mike4ruls.General
{
    [RequireComponent(typeof(EditorUpdater))]
    public class SpawnerComponent : MonoBehaviour, IUniqueEditorElement
    {
        [Range(0, 100)]
        public float itemdropChance;

        [System.Serializable]
        public struct SpawnItemSettings
        {
            [Range(0, 100)]
            public float item;
            [Range(0, 100)]
            public float weapon;
            [Range(0, 100)]
            public float sheild;
            [Range(0, 100)]
            public float weaponMod;
            [Range(0, 100)]
            public float sheildMod;

            public string itemDropChance;
            public string weaponDropChance;
            public string sheildDropChance;
            public string weaponModDropChance;
            public string sheildModDropChance;

            public void UpdatePossibleSpawnSettings()
            {
                float itm = item;
                float weap = weapon;
                float shld = sheild;
                float weapMod = weaponMod;
                float shldMod = sheildMod;


                if (item > weapon)
                {
                    weap = item;
                }
                if (weapon > sheild)
                {
                    shld = weapon;
                }
                if (sheild > weaponMod)
                {
                    weapMod = sheild;
                }
                if (weaponMod > sheildMod)
                {
                    shldMod = weaponMod;
                }

                if (sheildMod < weaponMod)
                {
                    weapMod = sheildMod;
                }
                if (weaponMod < sheild)
                {
                    shld = weaponMod;
                }
                if (sheild < weapon)
                {
                    weap = sheild;
                }
                if (weapon < item)
                {
                    itm = weapon;
                }

                item = itm;
                weapon = weap;
                sheild = shld;
                weaponMod = weapMod;
                sheildMod = shldMod;


                itemDropChance = "0%";
                weaponDropChance = "0%";
                sheildDropChance = "0%";
                weaponModDropChance = "0%";
                sheildModDropChance = "0%";

                itemDropChance = item + "%";
                weaponDropChance = (weapon - item) + "%";
                sheildDropChance = (sheild - weapon) + "%";
                weaponModDropChance = (weaponMod - sheild) + "%";
                sheildModDropChance = (sheildMod - weaponMod) + "%";
            }
        }
        [System.Serializable]
        public struct RarityChanceSettings
        {
            [Range(0, 100)]
            public float common;
            [Range(0, 100)]
            public float unCommon;
            [Range(0, 100)]
            public float rare;
            [Range(0, 100)]
            public float epic;
            [Range(0, 100)]
            public float legendary;

            public string commonDropChance;
            public string unCommonDropChance;
            public string rareDropChance;
            public string epicDropChance;
            public string legendaryDropChance;

            public void UpdateRaritySettings()
            {

                float com = common;
                float unCom = unCommon;
                float ra = rare;
                float ep = epic;
                float leg = legendary;

                if (common > unCommon)
                {
                    unCom = common;
                }
                if (unCommon > rare)
                {
                    ra = unCommon;
                }
                if (rare > epic)
                {
                    ep = rare;
                }

                if (epic > legendary)
                {
                    leg = epic;
                }

                if (legendary < epic)
                {
                    ep = legendary;
                }
                if (epic < rare)
                {
                    ra = epic;
                }
                if (rare < unCommon)
                {
                    unCom = rare;
                }
                if (unCommon < common)
                {
                    com = unCommon;
                }

                common = com;
                unCommon = unCom;
                rare = ra;
                epic = ep;
                legendary = leg;

                commonDropChance = common + "%";
                unCommonDropChance = (unCommon - common) + "%";
                rareDropChance = (rare - unCommon) + "%";
                epicDropChance = (epic - rare) + "%";
                legendaryDropChance = (legendary - epic) + "%";

            }

        }
        [System.Serializable]
        public struct ManufacturerSettings
        {
            public bool noxelCorpON;
            public bool dysfunctionalHqON;
            public bool midnightIntelligenceON;
            public bool ironPawON;
            public bool jerrysCakenBakeryON;
        }

        public SpawnItemSettings spawnItemSettings;
        public RarityChanceSettings rarityChanceSettings;
        public ManufacturerSettings manufacturerSettings;

        private List<Manufacturer> possibleManufacturers;
        private GameObject enviornment;
        private GameObject itemList;
        private float randomMaxNum = 10000;

        private ItemType itemTypeToSpawn = ItemType.Item;
        private RarityType rarityTypeToSpawn = RarityType.Common;
        private ModType modTypeToSpawn = ModType.WeaponMod;
        private Manufacturer manufacturerToSpawn = Manufacturer.MotherNature;
        private GameObject manufacturer = null;
        private GameObject itemToSpawn = null;

        private void Awake()
        {
            enviornment = GameObject.FindGameObjectWithTag("Enviornment");
            itemList = GameObject.FindGameObjectWithTag("ItemList");
            InitpossibleManufacturers();

        }


        private void Update()
        {
            
        }

        private void InitpossibleManufacturers()
        {
            if (possibleManufacturers == null)
            {
                possibleManufacturers = new List<Manufacturer>();
            }
            else
            {
                possibleManufacturers.Clear();
            }


            if (manufacturerSettings.noxelCorpON)
            {
                possibleManufacturers.Add(Manufacturer.NoxelCorp);
            }
            if (manufacturerSettings.dysfunctionalHqON)
            {
                possibleManufacturers.Add(Manufacturer.DysfunctionalHQ);
            }
            if (manufacturerSettings.midnightIntelligenceON)
            {
                possibleManufacturers.Add(Manufacturer.MidnightIntelligence);
            }
            if (manufacturerSettings.ironPawON)
            {
                possibleManufacturers.Add(Manufacturer.IronPaw);
            }
            if (manufacturerSettings.jerrysCakenBakeryON)
            {
                possibleManufacturers.Add(Manufacturer.JerrysCakenBakery);
            }
        }
        public Item SpawnItem()
        {
            Item newItem = null;
            float itemDropRanNum = (Random.Range(0, randomMaxNum + 1) / randomMaxNum) * 100;

            if (itemDropRanNum < itemdropChance)
            {

                DecideItemType();
                DecideManufacturer();
                DecideItemRarity();
                DecideItemToSpawn();

                newItem = Instantiate(itemToSpawn).GetComponent<Item>();
                newItem.transform.SetParent(enviornment.transform);
                newItem.transform.position = transform.position;
                newItem.gameObject.SetActive(true);
                newItem.manufacturer = manufacturerToSpawn;
                newItem.rarityType = rarityTypeToSpawn;
                newItem.Initialize();
            }

            return newItem;
        }

        void DecideItemType()
        {
            float itemtypeRanNum = (Random.Range(0, randomMaxNum + 1) / randomMaxNum) * 100;
            if (itemtypeRanNum < spawnItemSettings.item)
            {
                itemTypeToSpawn = ItemType.Item;
            }
            else if (itemtypeRanNum < spawnItemSettings.weapon)
            {
                itemTypeToSpawn = ItemType.Weapon;
            }
            else if (itemtypeRanNum < spawnItemSettings.sheild)
            {
                itemTypeToSpawn = ItemType.Sheild;
            }
            else if (itemtypeRanNum < spawnItemSettings.weaponMod)
            {
                itemTypeToSpawn = ItemType.Mod;
                modTypeToSpawn = ModType.WeaponMod;
            }
            else if (itemtypeRanNum < spawnItemSettings.sheildMod)
            {
                itemTypeToSpawn = ItemType.Mod;
                modTypeToSpawn = ModType.SheildMod;
            }
        }
        void DecideItemRarity()
        {
            if (itemTypeToSpawn != ItemType.Item)
            {
                float itemRarityRanNum = (Random.Range(0, randomMaxNum + 1) / randomMaxNum) * 100;
                if(itemRarityRanNum < rarityChanceSettings.common)
                {
                    rarityTypeToSpawn = RarityType.Common;
                }
                else if (itemRarityRanNum < rarityChanceSettings.unCommon)
                {
                    rarityTypeToSpawn = RarityType.Uncommon;
                }
                else if (itemRarityRanNum < rarityChanceSettings.rare)
                {
                    rarityTypeToSpawn = RarityType.Rare;
                }
                else if (itemRarityRanNum < rarityChanceSettings.epic)
                {
                    rarityTypeToSpawn = RarityType.Epic;
                }
                else if (itemRarityRanNum < rarityChanceSettings.legendary)
                {
                    rarityTypeToSpawn = RarityType.Legendary;
                }
            }
            else
            {
                rarityTypeToSpawn = RarityType.Uncommon;
            }
        }
        void DecideManufacturer()
        {
            if (itemTypeToSpawn != ItemType.Item)
            {
                if (possibleManufacturers.Count > 0)
                {
                    int manufacturerRanNum = (int)(Random.Range(0, randomMaxNum + 1) / randomMaxNum) * possibleManufacturers.Count;
                    manufacturerToSpawn = possibleManufacturers[manufacturerRanNum];
                }
                else
                {
                    itemTypeToSpawn = ItemType.Item;
                    manufacturerToSpawn = Manufacturer.MotherNature;
                }
            }
            else
            {
                manufacturerToSpawn = Manufacturer.MotherNature;
            }
            GrabManufacturer();
        }
        void GrabManufacturer()
        {
            switch (manufacturerToSpawn)
            {
                case Manufacturer.MotherNature:
                    {
                        manufacturer = itemList.transform.GetChild(0).gameObject;
                        break;
                    }
                case Manufacturer.NoxelCorp:
                    {
                        manufacturer = itemList.transform.GetChild(1).gameObject;
                        break;
                    }
                case Manufacturer.DysfunctionalHQ:
                    {
                        manufacturer = itemList.transform.GetChild(2).gameObject;
                        break;
                    }
                case Manufacturer.MidnightIntelligence:
                    {
                        manufacturer = itemList.transform.GetChild(3).gameObject;
                        break;
                    }
                case Manufacturer.IronPaw:
                    {
                        manufacturer = itemList.transform.GetChild(4).gameObject;
                        break;
                    }
                case Manufacturer.JerrysCakenBakery:
                    {
                        manufacturer = itemList.transform.GetChild(5).gameObject;
                        break;
                    }
            }
        }
        void DecideItemToSpawn()
        {
            float itemRanNum = Random.Range(0, randomMaxNum + 1) / randomMaxNum;
            switch (itemTypeToSpawn)
            {
                case ItemType.Item:
                    {
                        itemRanNum *= (manufacturer.transform.childCount - 1);
                        itemToSpawn = manufacturer.transform.GetChild((int)itemRanNum).gameObject;
                        break;
                    }
                case ItemType.Weapon:
                    {
                        itemRanNum *= (manufacturer.transform.GetChild(0).childCount - 1);
                        itemToSpawn = manufacturer.transform.GetChild(0).GetChild((int)itemRanNum).gameObject;
                        break;
                    }
                case ItemType.Sheild:
                    {
                        itemRanNum *= (manufacturer.transform.GetChild(1).childCount - 1);
                        itemToSpawn = manufacturer.transform.GetChild(1).GetChild((int)itemRanNum).gameObject;
                        break;
                    }
                case ItemType.Mod:
                    {
                        if (modTypeToSpawn == ModType.WeaponMod)
                        {
                            itemRanNum *= (manufacturer.transform.GetChild(2).childCount - 1);
                            itemToSpawn = manufacturer.transform.GetChild(2).GetChild((int)itemRanNum).gameObject;
                        }
                        else if (modTypeToSpawn == ModType.WeaponMod)
                        {
                            itemRanNum *= (manufacturer.transform.GetChild(3).childCount - 1);
                            itemToSpawn = manufacturer.transform.GetChild(3).GetChild((int)itemRanNum).gameObject;
                        }
                        break;
                    }
            }
        }

        public void ExecuteInEditor()
        {
            spawnItemSettings.UpdatePossibleSpawnSettings();
            rarityChanceSettings.UpdateRaritySettings();
        }
    }
}
