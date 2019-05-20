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
            for (int i = 0; i < numOfInstantiatedObjects; i++)
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
                if (turnOffObjs)
                {
                    obj.SetActive(false);
                }
            }
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
    }
}
