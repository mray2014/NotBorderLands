using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General.Items
{
    public enum ModType
    {
        WeaponMod = 0,
        SheildMod,
    }
    public class ModBase : Item
    {
        public ModType modType = ModType.WeaponMod;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
