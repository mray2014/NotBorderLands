using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General.Items
{
    public class SheildBase : Item
    {
        public ElementType elementType = ElementType.Normal;
        public float sheildHealth = 100;
        public float sheildCapacity = 100;
        public float rechargeWaitTimeAfterDepeletion = 5;
        public float sheildRechargeRate = 1;
        public float sheildRechargeAmmount = 1;

        protected bool isDepleted = false;
        protected bool isRecharging = false;
        protected bool isFullyCharged = true;

        private float rechargeRateTimer = 0;
        private float rechargeWaitTimer = 0;

        public void GenerateSheild()
        {

        }

        protected void RechargeUpdate()
        {
            if (isRecharging)
            {
                rechargeRateTimer += Time.deltaTime;
                if (rechargeRateTimer >= sheildRechargeRate)
                {
                    ChargeSheild(sheildRechargeAmmount);
                    rechargeRateTimer = 0;
                }
            }
            else
            {
                if (!isFullyCharged)
                {
                    if (rechargeWaitTimer >= rechargeWaitTimeAfterDepeletion)
                    {
                        rechargeRateTimer = 0;
                        isRecharging = true;
                    }
                    else
                    {
                        rechargeWaitTimer += Time.deltaTime;
                    }
                }
            }
        }
        public float TakeDamage(float damage)
        {
            float finalizedDamage = damage;
            rechargeWaitTimer = 0;
            if (!isDepleted)
            {
                sheildHealth -= damage;
                finalizedDamage = 0;
                isFullyCharged = false;

                if (sheildHealth < 0)
                {
                    finalizedDamage = (sheildHealth * -1);
                    sheildHealth = 0;
                    isDepleted = true;
                }
            }

            return finalizedDamage;
        }
        #region Recharge Functionality

        public void ChargeSheild(float chargeAmmount)
        {
            if (!isFullyCharged)
            {
                sheildHealth += chargeAmmount;
                isDepleted = false;

                if (sheildHealth > sheildCapacity)
                {
                    sheildHealth = sheildCapacity;
                    isFullyCharged = true;
                    isRecharging = false;
                }
            }
        }
        public void FullyRechargeSheild()
        {
            sheildHealth = sheildCapacity;

            isDepleted = false;
            isRecharging = false;
            isFullyCharged = true;
        }
        #endregion

    }
}
