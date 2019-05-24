using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General
{
    [RequireComponent(typeof(EditorUpdater))]
    public class BoxSpawnerScript : MonoBehaviour, IInteractable, IUniqueEditorElement
    {
        [System.Serializable]
        public struct itemSpawnerChanceSettings
        {
            [Range(0, 100)]
            public float _4Spawners;
            [Range(0, 100)]
            public float _2Spawners;
            [Range(0, 100)]
            public float _1Spawners;

            public string _4SpawnersDropChance;
            public string _2SpawnersDropChance;
            public string _1SpawnersDropChance;

            public void UpdateItemSpawnerChanceSettings()
            {

                float _4 = _4Spawners;
                float _2 = _2Spawners;
                float _1 = _1Spawners;

                if (_4Spawners > _2Spawners)
                {
                    _2 = _4Spawners;
                }
                if (_2Spawners > _1Spawners)
                {
                    _1 = _2Spawners;
                }

                if (_1Spawners < _2Spawners)
                {
                    _2 = _1Spawners;
                }
                if (_2Spawners < _4Spawners)
                {
                    _4 = _2Spawners;
                }

                _4Spawners = _4;
                _2Spawners = _2;
                _1Spawners = _1;



                _4SpawnersDropChance = _4Spawners + "%";
                _2SpawnersDropChance = (_2Spawners - _4Spawners) + "%";
                _1SpawnersDropChance = (_1Spawners - _2Spawners) + "%";

            }

        }

        public itemSpawnerChanceSettings spawnerChanceSettings;
        public bool debugON = false;
        private bool boxAnimationStarted = false;
        private bool boxAnimationFinished = false;

        private float randomMaxNum = 10000;
        private List<int> childrenSpawnersThatCanSpawn;
        // Use this for initialization
        void Start()
        {
            childrenSpawnersThatCanSpawn = new List<int>();
        }

        // Update is called once per frame
        void Update()
        {
            if (boxAnimationStarted && !boxAnimationFinished)
            {
                BoxOpeningAnim();
                if (boxAnimationFinished)
                {
                    for (int i = 0; i < childrenSpawnersThatCanSpawn.Count; i++)
                    {
                        Item newItem = transform.GetChild(childrenSpawnersThatCanSpawn[i]).GetComponent<SpawnerComponent>().SpawnItem();
                    }
                    
                    if (debugON)
                    {
                        boxAnimationStarted = false;
                        boxAnimationFinished = false;
                    }
                }
            }
        }
        public void Interact(PlayerBase player)
        {
            if (!boxAnimationStarted)
            {
                DecideSpawnAmmount();
                boxAnimationStarted = true;
            }
            
        }
        void DecideSpawnAmmount()
        {
            childrenSpawnersThatCanSpawn.Clear();

            int startPos = 0;
            int endPos = 1;
            float spawnRanNum = (Random.Range(0, randomMaxNum + 1) / randomMaxNum) * 100;

            if (spawnRanNum < spawnerChanceSettings._4Spawners)
            {
                startPos = 1;
                endPos = 4;
            }
            else if (spawnRanNum < spawnerChanceSettings._2Spawners)
            {
                startPos = 1;
                endPos = 2;
            }
            else if (spawnRanNum <= spawnerChanceSettings._1Spawners)
            {
                startPos = 0;
                endPos = 0;
            }

            for (int i = startPos; i <= endPos; i++)
            {
                childrenSpawnersThatCanSpawn.Add(i);
            }
        }
        void BoxOpeningAnim()
        {
            boxAnimationFinished = true;
        }

        public void ExecuteInEditor()
        {
            spawnerChanceSettings.UpdateItemSpawnerChanceSettings();
        }
    }
}