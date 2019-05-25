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
        [SerializeField]
        public static Color _CommonRarityColor;
        public static Color _UnCommonRarityColor;
        public static Color _RareRarityColor;
        public static Color _EpicRarityColor;
        public static Color _LegendaryRarityColor;


        public Color commonRarityColor;
        public Color unCommonRarityColor;
        public Color rareRarityColor;
        public Color epicRarityColor;
        public Color legendaryRarityColor;
        // Public Vars 
        public bool pauseEntireGame = false;

        // Private Vars 
        private GameState currentGameState = GameState.GameStart;
        private PlayerBase player1;

        // Use this for initialization
        void Start()
        {
            _CommonRarityColor = commonRarityColor;
            _UnCommonRarityColor = unCommonRarityColor;
            _RareRarityColor = rareRarityColor;
            _EpicRarityColor = epicRarityColor;
            _LegendaryRarityColor = legendaryRarityColor;
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
                        if (Input.GetButtonDown("Cancel"))
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
                        if (Input.GetButtonDown("Cancel"))
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
