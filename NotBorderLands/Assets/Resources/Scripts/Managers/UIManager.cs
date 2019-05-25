using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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

        public GameObject statsButton;
        public GameObject inventoryButton;
        public GameObject skillsButton;
        public GameObject missionsButton;

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
                        //statsButton.
                        if (Input.GetButtonDown("L1"))
                        {
                            int state = (int)currentPauseMenuState - 1;
                            if (state < 0)
                            {
                                DisplayPauseMenuUI(PauseMenuState.MissionsMenu);
                            }
                            else
                            {
                                DisplayPauseMenuUI((PauseMenuState)state);
                            }
                        }
                        if (Input.GetButtonDown("R1"))
                        {
                            int state = (int)currentPauseMenuState + 1;
                            if (state > (int)PauseMenuState.MissionsMenu)
                            {
                                DisplayPauseMenuUI(PauseMenuState.PlayerStatsMenu);
                            }
                            else
                            {
                                DisplayPauseMenuUI((PauseMenuState)state);
                            }
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
            SetSelectedButton();


        }
        void SetSelectedButton()
        {
            switch (currentPauseMenuState)
            {
                case PauseMenuState.PlayerStatsMenu:
                    {
                        GameObject.FindGameObjectWithTag("UIEventSystem").GetComponent<EventSystem>().SetSelectedGameObject(statsButton);
                        break;
                    }
                case PauseMenuState.InventoryMenu:
                    {
                        GameObject.FindGameObjectWithTag("UIEventSystem").GetComponent<EventSystem>().SetSelectedGameObject(inventoryButton);
                        break;
                    }
                case PauseMenuState.SkillsMenu:
                    {
                        GameObject.FindGameObjectWithTag("UIEventSystem").GetComponent<EventSystem>().SetSelectedGameObject(skillsButton);
                        break;
                    }
                case PauseMenuState.MissionsMenu:
                    {
                        GameObject.FindGameObjectWithTag("UIEventSystem").GetComponent<EventSystem>().SetSelectedGameObject(missionsButton);
                        break;
                    }
            }
        }
        void HidePauseMenu()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            gameUI.SetActive(true);
            pauseMenuUI.SetActive(false);

        }
        void DisplayPauseMenuUI(PauseMenuState newState)
        {
            playerStatsUI.SetActive(false);
            inventoryUI.SetActive(false);
            skillsUI.SetActive(false);
            missionsUI.SetActive(false);

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
            SetSelectedButton();
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
            SetSelectedButton();
        }
    }
}
