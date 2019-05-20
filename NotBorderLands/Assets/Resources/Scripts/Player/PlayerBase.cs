using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mike4ruls.Player
{
    public class PlayerBase : MonoBehaviour
    {
        // PrivateVars 
        private PlayerMovement _playerMovement;
        private bool playerActive = true;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {

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
    }
}
