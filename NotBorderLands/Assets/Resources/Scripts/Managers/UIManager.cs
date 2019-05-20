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
        public GameObject gameUI;
        public GameObject onScreenUI;
        public GameObject pauseMenuUI;
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
            switch (_gameManager.GetGameState())
            {
                case GameState.GameStart:
                    {
                        break;
                    }
                case GameState.Game:
                    {
                        reticle.gameObject.SetActive(player1.GetComponent<PlayerInventory>().hitInteractable);
                        if (!gameUI.activeInHierarchy)
                        {
                            gameUI.SetActive(true);
                            pauseMenuUI.SetActive(false);
                        }
                        break;
                    }
                case GameState.GamePause:
                    {
                        if (!pauseMenuUI.activeInHierarchy)
                        {
                            gameUI.SetActive(false);
                            pauseMenuUI.SetActive(true);
                        }
                        break;
                    }
            }
            
        }
    }
}
