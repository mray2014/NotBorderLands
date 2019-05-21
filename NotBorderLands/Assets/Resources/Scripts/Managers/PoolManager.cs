using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General.Managers
{
    public class PoolManager : MonoBehaviour {


        [Header("Instantiation Settings")]
        public GameObject object2Spawn;
        public Material objectMat;
        public int numOfInstantiatedObjects;
        public bool turnOffObjs = false;


        private bool initFinished = false;
        // Use this for initialization
        void Start()
        {
            if (!initFinished)
            {
                Initialize();
            }

        }
        void Initialize()
        {
            SpawnAmmount(numOfInstantiatedObjects, turnOffObjs);
            initFinished = true;
        }
        public void Initialize(int numToSpawn, bool turnOff)
        {
            numOfInstantiatedObjects = numToSpawn;
            turnOffObjs = turnOff;
            SpawnAmmount(numOfInstantiatedObjects, turnOffObjs);
            initFinished = true;
        }

        public GameObject GetAvailableObj()
        {
            if (!initFinished)
            {
                Initialize();
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    return transform.GetChild(i).gameObject;
                }
            }
            return null;
        }

        // Update is called once per frame
        void Update() {

        }
        public void SpawnAmmount(int numToSpawn, bool turnOff)
        {
            for (int i = 0; i < numToSpawn; i++)
            {
                GameObject obj = Instantiate(object2Spawn, this.transform);

                if (objectMat != null)
                {
                    obj.GetComponent<Renderer>().material = objectMat;
                }
                //AudioSource sfx = obj.GetComponent<AudioSource>();
                //if (sfx != null)
                //{
                //    GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManager>().AddToSFX(sfx);
                //}
                if (turnOff)
                {
                    obj.SetActive(false);
                }
            }
        }
        //public void SetSpacing(Vector3 spacing)
        //{
        //    for (int i = 0; i < transform.childCount; i++)
        //    {
        //        GameObject obj = transform.GetChild(i).gameObject;
        //        obj.transform.position = transform.position + (spacing * i);
        //    }
        //}
    }

}
