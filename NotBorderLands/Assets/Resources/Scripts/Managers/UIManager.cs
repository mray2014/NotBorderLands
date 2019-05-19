using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    GameManager _gameManager;
    PlayerBase player1;

    public Image reticle;

	// Use this for initialization
	void Start () {
        _gameManager = GetComponent<GameManager>();
        player1 = _gameManager.GetPlayer1();

    }
	
	// Update is called once per frame
	void Update ()
    {
         reticle.gameObject.SetActive(player1.hitInteractable);
	}
}
