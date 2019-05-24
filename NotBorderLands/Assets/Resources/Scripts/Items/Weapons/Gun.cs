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
    public enum ElementType
    {
        Normal,
        Fire,
        Ice,
        Explosion,
        Acid,
        Electric
    }

    [RequireComponent(typeof(WeaponStatManager))]
    public class Gun : Item
    {
        [Header("~Weapons Settings~")]
        // Public Vars 
        public WeaponType weaponType = WeaponType.Pistol;
        public ElementType elementType = ElementType.Normal;
        public FireMode fireMode = FireMode.Automatic;

        public bool hasFallOffDamage = true;

        // Protected Vars 
        public int weaponLevel = 1;
        public int damageMultiplyer = 1;
        public float damage;
        public float bulletVelocity;
        public float rateOfFire;
        public float burstRateOfFire;
        public float reloadSpeed;
        public float recoil;
        public float accuracy;
        public Vector3 bulletFallOffRange;
        public float maxBulletRange;
        public int burstNum = 3;

        public int magazine;
        public int magazineSize;
        public int ammoReserves;
        public int maxAmmo;

        private void Start()
        {

        }

        public void GenerateWeapon()
        {
            GetComponent<WeaponStatManager>().RollStats(this);
        }
        
        

        virtual public void Shoot() { }
        virtual public void Reload() { }
    }
}

