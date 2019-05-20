using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mike4ruls.General.Player;

namespace Mike4ruls.General.Managers
{
    public class InventoryMenuManager : MonoBehaviour
    {
       private PlayerBase _playerBase;
       private PlayerInventory _playeyInventory;
        bool initFinished = false;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (initFinished)
            {

            }
        }
        public void Initialize(PlayerBase player1)
        {
            _playerBase = player1;
            _playeyInventory = _playerBase.GetComponent<PlayerInventory>();
            initFinished = true;
        }
    }
}
