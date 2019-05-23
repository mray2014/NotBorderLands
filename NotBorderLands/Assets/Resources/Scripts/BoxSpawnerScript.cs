using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General
{
    public class BoxSpawnerScript : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Item newItem = transform.GetChild(0).GetComponent<SpawnerComponent>().SpawnItem();
                if (newItem != null)
                {
                    //newItem.drop
                }
            }
        }
    }
}