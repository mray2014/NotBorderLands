using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General.Managers
{
    public class MissionsMenuManager : MonoBehaviour
    {
       private PlayerBase _playerBase;
       private bool initFinished = false;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (initFinished)
            {

            }
        }
        public void Initialize(PlayerBase player1)
        {
            _playerBase = player1;
            initFinished = true;
        }
    }
}
