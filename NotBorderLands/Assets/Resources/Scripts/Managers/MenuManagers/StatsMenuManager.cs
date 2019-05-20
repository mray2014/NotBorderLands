using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mike4ruls.General.Player;

namespace Mike4ruls.General.Managers
{
    public class StatsMenuManager : MonoBehaviour
    {
        public TextMeshProUGUI playerLevelText;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI sheildText;
        public TextMeshProUGUI moneyText;

        private PlayerBase _playerBase;
       private PlayerMovement _playerMovement;
       private PlayerInventory _playeyInventory;
       private bool initFinished = false;
        // Use this for initialization
        void Awake()
        {
            UpdateText();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateText();
        }
        void UpdateText()
        {
            if (initFinished)
            {
                playerLevelText.text = "Player Lvl: " + _playerBase.playerLvl;
                healthText.text = "Health: " + _playerBase.health + "/" + _playerBase.maxHealth;
                sheildText.text = "Sheild: ";
                if (_playeyInventory.IsWearingASheild())
                {
                    sheildText.text += "" + _playeyInventory.GetCurrentSheild().sheildHealth + "/" + _playeyInventory.GetCurrentSheild().sheildCapacity;
                }
                else
                {
                    sheildText.text += "NONE";
                }

                moneyText.text = "Money: " + _playerBase.money;
            }
        }
        public void Initialize(PlayerBase player1)
        {
            _playerBase = player1;
            _playerMovement = _playerBase.GetComponent<PlayerMovement>();
            _playeyInventory = _playerBase.GetComponent<PlayerInventory>();
            initFinished = true;
        }
    }
}
