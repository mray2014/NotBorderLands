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
        public float boxOpeningSpeed = 4;
        public float boxOpeningradius = 1;
        public GameObject spawners;
        public GameObject leftSide;
        public GameObject rightSide;
        public itemSpawnerChanceSettings spawnerChanceSettings;
        public bool debugON = false;
        private bool boxAnimationStarted = false;
        private bool boxAnimationFinished = false;

        private float randomMaxNum = 10000;
        private List<int> childrenSpawnersThatCanSpawn;

        private InteractType interactionType = InteractType.Open;
        private bool localCanInteract = true;
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
                        Item newItem = spawners.transform.GetChild(childrenSpawnersThatCanSpawn[i]).GetComponent<SpawnerComponent>().SpawnItem();
                    }
                    
                    if (debugON)
                    {
                        boxAnimationStarted = false;
                        boxAnimationFinished = false;
                        localCanInteract = true;
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
                localCanInteract = false;
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
            float leftSideDist = (leftSide.transform.position - transform.position).magnitude;
            float rightSideDist = (rightSide.transform.position - transform.position).magnitude;

            if (leftSideDist >= boxOpeningradius && rightSideDist >= boxOpeningradius)
            {
                boxAnimationFinished = true;
            }
            else
            {
                leftSide.transform.position += transform.right * -boxOpeningSpeed * Time.deltaTime;
                rightSide.transform.position += transform.right * boxOpeningSpeed * Time.deltaTime;
            }
           
        }

        public void ExecuteInEditor()
        {
            spawnerChanceSettings.UpdateItemSpawnerChanceSettings();
        }
        public InteractType interactType
        {
            get
            {
                return interactionType;
            }

            set
            {
                interactionType = value;
            }
        }

        public bool canInteract
        {
            get
            {
                return localCanInteract;
            }
            set
            {
                localCanInteract = value;
            }
        }
    }
}