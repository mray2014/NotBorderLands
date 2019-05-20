using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.Items
{
    public enum WeaponType
    {
        Pistol,
        AssualtRifle,
        Shotgun,
        LightMachineGun,
        RocketLauncher
    }
    public enum FireMode
    {
        SemiAuto,
        Burst,
        Automatic
    }
    public class GunBase : Item
    {
        // Public Vars 
        public WeaponType weaponType = WeaponType.Pistol;
        public FireMode fireMode = FireMode.Automatic;
        public Vector2 rateOfFireRange, burstRateOfFireRange, reloadSpeedRange, recoilRange, accuracyRange, bulletRangeRange;

        // Protected Vars 
        protected int weaponLevel = 1;
        protected float damage;
        protected float rateOfFire;
        protected float burstRateOfFire;
        protected float reloadSpeed;
        protected float recoil;
        protected float accuracy;
        protected float bulletRange;
        protected int burstNum = 3;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GenerateWeapon()
        {
            DecideFireMode();
            damage = weaponLevel * 3;
            rateOfFire = Random.Range(rateOfFireRange.x, rateOfFireRange.y);
            burstRateOfFire = Random.Range(burstRateOfFireRange.x, burstRateOfFireRange.y);
            reloadSpeed = Random.Range(reloadSpeedRange.x, reloadSpeedRange.y);
            recoil = Random.Range(recoilRange.x, recoilRange.y);
            accuracy = Random.Range(accuracyRange.x, accuracyRange.y);
            bulletRange = Random.Range(bulletRangeRange.x, bulletRangeRange.y);
        }
        void DecideFireMode()
        {

            int ranNum = Random.Range(0, 101);

            switch (ranNum)
            {
                case 33:
                    {
                        fireMode = FireMode.SemiAuto;
                        break;
                    }
                case 66:
                    {
                        fireMode = FireMode.Burst;
                        break;
                    }
                default:
                    {
                        fireMode = FireMode.Automatic;
                        break;
                    }
            }
        }
    }
}

