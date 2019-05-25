using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mike4ruls.General.Player;
using TMPro;

namespace Mike4ruls.General.Managers
{
    public enum PauseMenuState
    {
        PlayerStatsMenu = 0,
        InventoryMenu,
        SkillsMenu,
        MissionsMenu
    }
    public class UIManager : MonoBehaviour
    {
        // Public Vars 
        public GameObject gameUI;
        public GameObject onScreenUI;
        public GameObject pauseMenuUI;
        public GameObject playerStatsUI;
        public GameObject inventoryUI;
        public GameObject skillsUI;
        public GameObject missionsUI;

        public Image reticle;

        // Private Vars 
        private PauseMenuState currentPauseMenuState = PauseMenuState.PlayerStatsMenu;
        private GameManager _gameManager;
        private PlayerBase player1;
        // Use this for initialization
        void Start()
        {
            _gameManager = GetComponent<GameManager>();
            player1 = _gameManager.GetPlayer1();

            playerStatsUI.GetComponent<StatsMenuManager>().Initialize(player1);
            inventoryUI.GetComponent<InventoryMenuManager>().Initialize(player1);
            skillsUI.GetComponent<SkillsMenuManager>().Initialize(player1);
            missionsUI.GetComponent<MissionsMenuManager>().Initialize(player1);

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
                        switch (player1.GetComponent<PlayerInventory>().currentInteractType)
                        {
                            case InteractType.Open:
                                {
                                    reticle.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Press 'F' to Open";
                                    break;
                                }
                            case InteractType.PickUp:
                                {
                                    reticle.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Press 'F' to Pick up";
                                    
                                    break;
                                }
                            case InteractType.TalkTo:
                                {
                                    reticle.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Press 'F' to Talk to";
                                    break;
                                }
                            case InteractType.Use:
                                {
                                    reticle.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Press 'F' to Use";
                                    break;
                                }
                        }
                        reticle.gameObject.SetActive(player1.GetComponent<PlayerInventory>().hitInteractable);
                        
                        if (!gameUI.activeInHierarchy)
                        {
                            HidePauseMenu();
                        }
                        break;
                    }
                case GameState.GamePause:
                    {
                        if (!pauseMenuUI.activeInHierarchy)
                        {
                            DisplayPauseMenu();
                        }
                        break;
                    }
            }           
        }

        void DisplayPauseMenu()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
        void HidePauseMenu()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            gameUI.SetActive(true);
            pauseMenuUI.SetActive(false);

        }
        public void DisplayPauseMenuUI(int state)
        {
            playerStatsUI.SetActive(false);
            inventoryUI.SetActive(false);
            skillsUI.SetActive(false);
            missionsUI.SetActive(false);

            PauseMenuState newState = (PauseMenuState)state;

            switch (newState)
            {
                case PauseMenuState.PlayerStatsMenu:
                    {
                        playerStatsUI.SetActive(true);
                        break;
                    }
                case PauseMenuState.InventoryMenu:
                    {
                        inventoryUI.SetActive(true);
                        break;
                    }
                case PauseMenuState.SkillsMenu:
                    {
                        skillsUI.SetActive(true);
                        break;
                    }
                case PauseMenuState.MissionsMenu:
                    {
                        missionsUI.SetActive(true);
                        break;
                    }
            }
            currentPauseMenuState = newState;
        }
    }
}
