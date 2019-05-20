using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mike4ruls.Player;

namespace Mike4ruls.Managers
{

    public class UIManager : MonoBehaviour
    {
        // Public Vars 
        public Image reticle;

        // Private Vars 
        GameManager _gameManager;
        PlayerBase player1;


        // Use this for initialization
        void Start()
        {
            _gameManager = GetComponent<GameManager>();
            player1 = _gameManager.GetPlayer1();

        }

        // Update is called once per frame
        void Update()
        {
            reticle.gameObject.SetActive(player1.GetComponent<PlayerInventory>().hitInteractable);
        }
    }
}
