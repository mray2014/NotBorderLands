using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General.Items
{
    [RequireComponent(typeof(EditorUpdater))]
    public class WeaponStatManager : MonoBehaviour, IUniqueEditorElement
    {
        [System.Serializable]
        public struct ElementalChanceSettings
        {
            [Range(0, 100)]
            public float normal;
            [Range(0, 100)]
            public float fire;
            [Range(0, 100)]
            public float ice;
            [Range(0, 100)]
            public float explosion;
            [Range(0, 100)]
            public float acid;
            [Range(0, 100)]
            public float electric;

            public string normalDropChance;
            public string fireDropChance;
            public string iceBurstDropChance;
            public string explosionDropChance;
            public string acidBurstDropChance;
            public string electricDropChance;

            public void UpdateElementalChanceSettings()
            {

                float nor = normal;
                float fi = fire;
                float ic = ice;
                float ex = explosion;
                float acd = acid;
                float ele = electric;

                if (normal > fire)
                {
                    fi = normal;
                }
                if (fire > ice)
                {
                    ic = fire;
                }
                if (ice > explosion)
                {
                    ex = ice;
                }
                if (explosion > acid)
                {
                    acd = explosion;
                }
                if (acid > electric)
                {
                    ele = acid;
                }



                if (electric < acid)
                {
                    acd = electric;
                }
                if (acid < explosion)
                {
                    ex = acid;
                }
                if (explosion < ice)
                {
                    ic = explosion;
                }
                if (ice < fire)
                {
                    fi = ice;
                }
                if (fire < normal)
                {
                    nor = fire;
                }

                normal = nor;
                fire = fi;
                ice = ic;
                explosion = ex;
                 acid = acd;
                 electric = ele;


                normalDropChance = normal + "%";
                fireDropChance = (fire - normal) + "%";
                iceBurstDropChance = (ice - fire) + "%";
                explosionDropChance = (explosion - ice) + "%";
                acidBurstDropChance = (acid - explosion) + "%";
                electricDropChance = (electric - acid) + "%";

            }

        }

        [System.Serializable]
        public struct FireModeChanceSettings
        {
            [Range(0, 100)]
            public float semiAuto;
            [Range(0, 100)]
            public float burst;
            [Range(0, 100)]
            public float fullAutoBurst;
            [Range(0, 100)]
            public float automatic;

            public string semiAutoDropChance;
            public string burstDropChance;
            public string fullAutoBurstDropChance;
            public string automaticDropChance;

            public void UpdateFireChanceSettings()
            {

                float semi = semiAuto;
                float bur = burst;
                float fAB = fullAutoBurst;
                float auto = automatic;

                if (semiAuto > burst)
                {
                    bur = semiAuto;
                }
                if (burst > fullAutoBurst)
                {
                    fAB = burst;
                }
                if (fullAutoBurst > automatic)
                {
                    auto = fullAutoBurst;
                }

                if (automatic < fullAutoBurst)
                {
                    fAB = automatic;
                }
                if (fullAutoBurst < burst)
                {
                    bur = fullAutoBurst;
                }
                if (burst < semiAuto)
                {
                    semi = burst;
                }

                semiAuto = semi;
                burst = bur;
                fullAutoBurst = fAB;
                automatic = auto;


                semiAutoDropChance = semiAuto + "%";
                burstDropChance = (burst - semiAuto) + "%";
                fullAutoBurstDropChance = (fullAutoBurst - burst) + "%";
                automaticDropChance = (automatic - fullAutoBurst) + "%";

            }

        }

        public Vector2 damageMultiplierRange = new Vector2(1, 1);
        public Vector2 damageRange, bulletVelocityRange, rateOfFireRange, burstRateOfFireRange,
            burstNumRange = new Vector2(3, 5), reloadSpeedRange, recoilRange, accuracyRange, magazineSizeRange;
        public Vector3 minBulletFallOffRange, maxBulletFallOffRange;

        public FireModeChanceSettings fireModeSettings;
        public ElementalChanceSettings elementalChanceSettings;
        private float randomMaxNum = 10000;

        private void OnGUI()
        {
            fireModeSettings.UpdateFireChanceSettings();
            elementalChanceSettings.UpdateElementalChanceSettings();
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        public void RollStats(Gun myGun)
        {
            DecideFireMode(myGun);
            DecideGunStats(myGun);
            DecideElementType(myGun);
        }
        void DecideElementType(Gun myGun)
        {
            float elementalRanNum = (Random.Range(0, randomMaxNum + 1) / randomMaxNum) * 100;

            if (elementalRanNum < elementalChanceSettings.normal)
            {
                myGun.elementType = ElementType.Normal;
            }
            else if (elementalRanNum < elementalChanceSettings.fire)
            {
                myGun.elementType = ElementType.Fire;
            }

            else if (elementalRanNum < elementalChanceSettings.ice)
            {
                myGun.elementType = ElementType.Ice;
            }
            else if (elementalRanNum < elementalChanceSettings.explosion)
            {
                myGun.elementType = ElementType.Explosion;
            }
            else if (elementalRanNum < elementalChanceSettings.acid)
            {
                myGun.elementType = ElementType.Acid;
            }
            else if (elementalRanNum < elementalChanceSettings.electric)
            {
                myGun.elementType = ElementType.Electric;
            }
        }

        void DecideFireMode(Gun myGun)
        {

            float fireModeRanNum = (Random.Range(0, randomMaxNum + 1) / randomMaxNum) * 100;

            if (fireModeRanNum < fireModeSettings.semiAuto)
            {
                myGun.fireMode = FireMode.SemiAuto;
            }
            else if (fireModeRanNum < fireModeSettings.burst)
            {
                myGun.fireMode = FireMode.Burst;
            }

            else if (fireModeRanNum < fireModeSettings.fullAutoBurst)
            {
                myGun.fireMode = FireMode.FullAutoBurst;
            }
            else if(fireModeRanNum < fireModeSettings.automatic)
            {
                myGun.fireMode = FireMode.Automatic;
            }
        }

        void DecideGunStats(Gun myGun)
        {
           myGun.damage = Random.Range((damageRange.x * myGun.weaponLevel), (damageRange.y * myGun.weaponLevel));
           myGun.damageMultiplyer = (int)Random.Range(damageMultiplierRange.x, damageMultiplierRange.y);
           myGun.bulletVelocity = Random.Range(bulletVelocityRange.x, bulletVelocityRange.y);
           myGun.rateOfFire = Random.Range(rateOfFireRange.x, rateOfFireRange.y);
           myGun.burstRateOfFire = Random.Range(burstRateOfFireRange.x, burstRateOfFireRange.y);
           myGun.burstNum = (int)Random.Range(burstNumRange.x, burstNumRange.y);
           myGun.reloadSpeed = Random.Range(reloadSpeedRange.x, reloadSpeedRange.y);
           myGun.recoil = Random.Range(recoilRange.x, recoilRange.y);
           myGun.accuracy = Random.Range(accuracyRange.x, accuracyRange.y);
           myGun.magazineSize = (int)Random.Range(magazineSizeRange.x, magazineSizeRange.y);
            myGun.bulletFallOffRange.x = Random.Range(minBulletFallOffRange.x, maxBulletFallOffRange.x);

            if (myGun.bulletFallOffRange.x > minBulletFallOffRange.y)
            {
                myGun.bulletFallOffRange.y = Random.Range(myGun.bulletFallOffRange.x, maxBulletFallOffRange.y);
            }
            else
            {
                myGun.bulletFallOffRange.y = Random.Range(minBulletFallOffRange.y, maxBulletFallOffRange.y);
            }

            if (myGun.bulletFallOffRange.y > minBulletFallOffRange.z)
            {
                myGun.bulletFallOffRange.z = Random.Range(myGun.bulletFallOffRange.y, maxBulletFallOffRange.z);
            }
            else
            {
                myGun.bulletFallOffRange.z = Random.Range(minBulletFallOffRange.z, maxBulletFallOffRange.z);
            }
            myGun.maxBulletRange = Random.Range(myGun.bulletFallOffRange.z + 50, myGun.bulletFallOffRange.z + 150);
        }

        public void ExecuteInEditor()
        {
            fireModeSettings.UpdateFireChanceSettings();
            elementalChanceSettings.UpdateElementalChanceSettings();
        }
    }
}
