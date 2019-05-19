using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DropReward(float dropChance)
    {
        float ranNum = Random.Range(0, 101);
        if (ranNum < dropChance)
        {
            GenerateReward();
        }
    }

    void GenerateReward()
    {

    }
}
