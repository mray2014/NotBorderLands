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
            weaponLevel = _playerBase.playerLvl;
            GetComponent<WeaponStatManager>().RollStats(this);
        }
        
        

        virtual public void Shoot() { }
        virtual public void Reload() { }

        protected override void GenerateName()
        {
            name = GetManucaturerName();
            name += GetFireModeType_ToString();
            //name += GetWeaponName();
            name += GetWeaponType_ToString();
        }

        public string GetManucaturerName()
        {
            string text = "";
            switch (manufacturer)
            {
                case Manufacturer.NoxelCorp:
                    {
                        text = "NXL ";
                        break;
                    }
                case Manufacturer.DysfunctionalHQ:
                    {
                        text = "DySfuNc ";
                        break;
                    }
                case Manufacturer.MidnightIntelligence:
                    {
                        text = "MN_Intel ";
                        break;
                    }
                case Manufacturer.IronPaw:
                    {
                        text = "Iron ";
                        break;
                    }
                case Manufacturer.JerrysCakenBakery:
                    {
                        text = "Jerry's ";
                        break;
                    }
            }
            return text;
        }
        public string GetFireModeType_ToString()
        {
            string text = "";
            switch (fireMode)
            {
                case FireMode.SemiAuto:
                    {
                        text = "Semi-Auto ";
                        break;
                    }
                case FireMode.Burst:
                    {
                        text = burstNum + "-round burst ";
                        break;
                    }
                case FireMode.FullAutoBurst:
                    {
                        text = "Full Auto " + burstNum + "-round burst ";
                        break;
                    }
                case FireMode.Automatic:
                    {
                        text = "Automatic ";
                        break;
                    }
            }
            return text;
        }
        public string GetWeaponType_ToString()
        {
            string text = "";
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    {
                        text = "Pistol ";
                        break;
                    }
                case WeaponType.Uzi:
                    {
                        text = "Uzi ";
                        break;
                    }
                case WeaponType.AssualtRifle:
                    {
                        text = "AR ";
                        break;
                    }
                case WeaponType.Shotgun:
                    {
                        text = "Shotgun ";
                        break;
                    }
                case WeaponType.LightMachineGun:
                    {
                        text = "LMG ";
                        break;
                    }
                case WeaponType.Sniper:
                    {
                        text = "Sniper ";
                        break;
                    }
                case WeaponType.RocketLauncher:
                    {
                        text = "Rocket ";
                        break;
                    }
            }
            return text;
        }
        public string GetWeaponName()
        {
            string text = "";
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    {
                        text = "Hammer ";
                        break;
                    }
                case WeaponType.Uzi:
                    {
                        text = "Hit'em Good ";
                        break;
                    }
                case WeaponType.AssualtRifle:
                    {
                        text = "Assault Ranger ";
                        break;
                    }
                case WeaponType.Shotgun:
                    {
                        text = "Truck Stopper ";
                        break;
                    }
                case WeaponType.LightMachineGun:
                    {
                        text = "Brick Shooter ";
                        break;
                    }
                case WeaponType.Sniper:
                    {
                        text = "Thunder Clap ";
                        break;
                    }
                case WeaponType.RocketLauncher:
                    {
                        text = "Disintegrate ";
                        break;
                    }
            }
            return text;
        }
    }
}

