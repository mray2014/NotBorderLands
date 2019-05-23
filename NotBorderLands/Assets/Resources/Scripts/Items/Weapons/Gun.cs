using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General.Items
{
    public enum WeaponType
    {
        Pistol,
        Uzi,
        AssualtRifle,
        Shotgun,
        LightMachineGun,
        Sniper,
        RocketLauncher
    }
    public enum FireMode
    {
        SemiAuto,
        Burst,
        FullAutoBurst,
        Automatic
    }
    public class Gun : Item
    {
        // Public Vars 
        public WeaponType weaponType = WeaponType.Pistol;
        public ElementType elementType = ElementType.Normal;
        public FireMode fireMode = FireMode.Automatic;
        public Vector2 damageMultiplierRange = new Vector2(1,1);
        public Vector2 damageRange, bulletVelocityRange, rateOfFireRange, burstRateOfFireRange,
            burstNumRange = new Vector2(3, 5), reloadSpeedRange, recoilRange, accuracyRange, magazineSizeRange;
        public Vector3 minBulletFallOffRange, maxBulletFallOffRange;
        public bool hasFallOffDamage = true;

        // Protected Vars 
        protected int weaponLevel = 1;
        protected int damageMultiplyer = 1;
        protected float damage;
        protected float bulletVelocity;
        protected float rateOfFire;
        protected float burstRateOfFire;
        protected float reloadSpeed;
        protected float recoil;
        protected float accuracy;
        protected Vector3 bulletFallOffRange;
        protected float maxBulletRange;
        protected int burstNum = 3;

        protected int magazine;
        protected int magazineSize;
        protected int ammoReserves;
        protected int maxAmmo;

        public void GenerateWeapon()
        {
            DecideFireMode();
            DecideGunStats();

        }
        void DecideGunStats()
        {
            damage = Random.Range((damageRange.x * weaponLevel), (damageRange.y * weaponLevel));
            damageMultiplyer = (int)Random.Range(damageMultiplierRange.x, damageMultiplierRange.y);
            bulletVelocity = Random.Range(bulletVelocityRange.x, bulletVelocityRange.y);
            rateOfFire = Random.Range(rateOfFireRange.x, rateOfFireRange.y);
            burstRateOfFire = Random.Range(burstRateOfFireRange.x, burstRateOfFireRange.y);
            burstNum = (int)Random.Range(burstNumRange.x, burstNumRange.y);
            reloadSpeed = Random.Range(reloadSpeedRange.x, reloadSpeedRange.y);
            recoil = Random.Range(recoilRange.x, recoilRange.y);
            accuracy = Random.Range(accuracyRange.x, accuracyRange.y);
            magazineSize = (int)Random.Range(magazineSizeRange.x, magazineSizeRange.y);
            bulletFallOffRange.x = Random.Range(minBulletFallOffRange.x, maxBulletFallOffRange.x);

            if (bulletFallOffRange.x > minBulletFallOffRange.y)
            {
                bulletFallOffRange.y = Random.Range(bulletFallOffRange.x, maxBulletFallOffRange.y);
            }
            else
            {
                bulletFallOffRange.y = Random.Range(minBulletFallOffRange.y, maxBulletFallOffRange.y);
            }

            if (bulletFallOffRange.y > minBulletFallOffRange.z)
            {
                bulletFallOffRange.z = Random.Range(bulletFallOffRange.y, maxBulletFallOffRange.z);
            }
            else
            {
                bulletFallOffRange.z = Random.Range(minBulletFallOffRange.z, maxBulletFallOffRange.z);
            }
            maxBulletRange = Random.Range(bulletFallOffRange.z + 50, bulletFallOffRange.z + 150);
        }
        
        void DecideFireMode()
        {

            int ranNum = Random.Range(0, 101);

            if (ranNum < 25)
            {
                fireMode = FireMode.SemiAuto;
            }
            else if (ranNum < 50)
            {
                fireMode = FireMode.Burst;
            }

            else if (ranNum < 75)
            {
                fireMode = FireMode.FullAutoBurst;
            }

            else
            {
                fireMode = FireMode.Automatic;
            }
        }
        virtual public void Shoot() { }
        virtual public void Reload() { }
    }
}

