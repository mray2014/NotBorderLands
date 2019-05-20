using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General
{

    public enum GameState
    {
        GameStart,
        Game,
        GamePause
    }

    public class GameManager : MonoBehaviour
    {
        // Public Vars 
        public bool pauseEntireGame = false;

        // Private Vars 
        private GameState currentGameState = GameState.GameStart;
        private PlayerBase player1;

        // Use this for initialization
        void Start()
        {
            player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBase>();

        }

        // Update is called once per frame
        void Update()
        {

            switch (currentGameState)
            {
                case GameState.GameStart:
                    {
                        currentGameState = GameState.Game;
                        break;
                    }
                case GameState.Game:
                    {
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            if (pauseEntireGame)
                            {
                                Time.timeScale = 0;
                            }
                            player1.SetPlayerActive(false);
                            currentGameState = GameState.GamePause;
                        }
                        break;
                    }
                case GameState.GamePause:
                    {
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            if (pauseEntireGame)
                            {
                                Time.timeScale = 1;
                            }
                            player1.SetPlayerActive(true);
                            currentGameState = GameState.Game;
                        }
                        break;
                    }
            }

        }
        public PlayerBase GetPlayer1()
        {
            if (player1 == null)
            {
                player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBase>();
            }
            return player1;
        }
        public GameState GetGameState()
        {
            return currentGameState;
        }
    }
}
