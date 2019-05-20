using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mike4ruls.Items;

namespace Mike4ruls.Player
{
    public class PlayerBase : MonoBehaviour
    {
        // Public Vars 
        public int playerLvl = 1;
        public float health = 100;
        public float maxHealth = 100;
        public int money = 0;

        // Private Vars 
        private PlayerInventory _playerInventory;
        private PlayerMovement _playerMovement;
        private bool playerActive = true;
        private bool isDead = false;

        private void Start()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {

        }

        #region Money Functionality

        public void AddMoney(int ammount)
        {
            money += ammount;
        }
        public bool SpendMoney(int cost)
        {
            int finalMoney = money - cost;
            if (finalMoney < 0)
            {
                return false;
            }
            money = finalMoney;
            return true;
        }

        #endregion

        #region Health Functionality
        public void Heal(float healAmmount)
        {
            health += healAmmount;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
        public void ChargeSheild(float chargeAmmount)
        {
            if (_playerInventory.IsWearingASheild())
            {
                _playerInventory.GetCurrentSheild().ChargeSheild(chargeAmmount);
            }
        }
        public void TakeDamage(float damage)
        {
            if (!isDead)
            {
                float rollOverDamage = damage;
                if (_playerInventory.IsWearingASheild())
                {
                    rollOverDamage = _playerInventory.GetCurrentSheild().TakeDamage(damage);
                }

                health -= rollOverDamage;
                if (health < 0)
                {
                    health = 0;
                    isDead = true;
                }
            }
        }
        #endregion

        #region Revive Functionality
        public void Revive()
        {
            if (_playerInventory.IsWearingASheild())
            {
                _playerInventory.GetCurrentSheild().FullyRechargeSheild();
            }
            Heal(maxHealth / 6);
            isDead = false;
        }
        public void FullHealRevive()
        {
            if (_playerInventory.IsWearingASheild())
            {
                _playerInventory.GetCurrentSheild().FullyRechargeSheild();
            }
            health = maxHealth;
            isDead = false;
        }
        public void FullHealRevive(Vector3 spawnPos)
        {
            if (_playerInventory.IsWearingASheild())
            {
                _playerInventory.GetCurrentSheild().FullyRechargeSheild();
            }
            health = maxHealth;
            transform.position = spawnPos;
            isDead = false;
        }
        #endregion

        #region Getters and Setters
        public int GetPlayerMoney()
        {
            return money;
        }
        public bool IsPlayerDead()
        {
            return isDead;
        }
        public bool GetPlayerActive()
        {
            return playerActive;
        }
        public void SetPlayerActive(bool active)
        {
            playerActive = active;
            _playerMovement.enabled = active;
        }
        #endregion
    }
}
