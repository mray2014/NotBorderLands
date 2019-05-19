using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    GameStart,
    Game,
    GamePause
}

public class GameManager : MonoBehaviour {

    public GameState currentGameState = GameState.GameStart;
    PlayerBase player1;

	// Use this for initialization
	void Start () {
        player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBase>();

    }
	
	// Update is called once per frame
	void Update ()
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
                        player1.SetPlayerActive(false);
                        currentGameState = GameState.GamePause;
                    }
                    break;
                }
            case GameState.GamePause:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
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
}
