using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General
{
    public enum ItemType
    {
        Weapon,
        Sheild,
        HealthPickUp,
        SheildBattery
    }
    public enum RarityType
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    public enum ElementType
    {
        Normal,
        Fire,
        Ice,
        Explosion,
        Acid,
        Electric
    }
    public class Item : MonoBehaviour
    {
        // Public Vars 
        public string name = "";
        public RarityType rarityType = RarityType.Common;
        public ItemType itemType = ItemType.Weapon;
        public GameObject itemCollider;
        public bool isEquippable = false;

        // Protected Vars
        protected PlayerBase _playerBase;

        // Private Vars
        private Managers.PoolManager rarityParticleEffectPool;
        private GameObject enviornment;
        private Rigidbody myRigidbody;
        private bool itemPickedUp = false;
        private bool pullIn = false;

        // Use this for initialization
        public void Awake()
        {
            rarityParticleEffectPool = GameObject.FindGameObjectWithTag("RarityParticleEffectPool").GetComponent<Managers.PoolManager>();
            _playerBase = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBase>();
            myRigidbody = GetComponent<Rigidbody>();
            enviornment = GameObject.FindGameObjectWithTag("Enviornment");


            Items.RarityParticleEffectScript particleEffect = rarityParticleEffectPool.GetAvailableObj().GetComponent<Items.RarityParticleEffectScript>();
            if (particleEffect != null)
            {
                particleEffect.ActivateRarityEffect(this);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void PickUp(GameObject target)
        {
            Vector3 dirToTarget = (target.transform.position - transform.position).normalized;

            StartCoroutine(PullInLogic(target));
            itemCollider.SetActive(false);
            pullIn = true;
        }
        public void PickUp(Vector3 pos)
        {
            itemPickedUp = true;
            pullIn = false;
            myRigidbody.useGravity = false;
            myRigidbody.angularDrag = 0;
            myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            itemCollider.SetActive(false);
            transform.position = pos;
            this.gameObject.SetActive(false);
        }
        public void ThrowAway(Vector3 pos, Vector3 throwDirection)
        {
            itemPickedUp = false;
            myRigidbody.useGravity = true;
            myRigidbody.constraints = RigidbodyConstraints.None;
            itemCollider.SetActive(true);
            transform.position = pos;
            transform.parent = enviornment.transform;
            float twistX = Random.Range(-1, 1);
            float twistZ = Random.Range(-1, 1);
            this.gameObject.SetActive(true);

            myRigidbody.AddForceAtPosition(throwDirection, pos - new Vector3(twistX, -1, twistZ), ForceMode.Force);

            Items.RarityParticleEffectScript particleEffect = rarityParticleEffectPool.GetAvailableObj().GetComponent<Items.RarityParticleEffectScript>();
            if (particleEffect != null)
            {
                particleEffect.ActivateRarityEffect(this);
            }
            //myRigidbody.AddExplosionForce(200, pos, 2);
        }
        IEnumerator PullInLogic(GameObject target)
        {
            Vector3 distToTarget = Vector3.zero;

            distToTarget = (target.transform.position + new Vector3(0, 0.3f, 0)) - transform.position;
            Vector3 dirToTarget = distToTarget.normalized;

            float twistX = Random.Range(-2, 2);
            float twistZ = Random.Range(-2, 2);
            //myRigidbody.useGravity = false;
            myRigidbody.AddForceAtPosition(dirToTarget * 50f, transform.position - new Vector3(twistX, -1, twistZ), ForceMode.Force);
            myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, 10);
            yield return new WaitForSeconds(0);

            if (distToTarget.magnitude < 1)
            {
                PickUp(new Vector3(0, 99999, 0));
                this.gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(PullInLogic(target));
            }


        }
        public bool HasItemBeenPickedUp()
        {
            return itemPickedUp;
        }
        public bool IsPullingIn()
        {
            return pullIn;
        }
        public string ItemToString()
        {
            string finalText = "";

            switch (itemType)
            {
                case ItemType.Weapon:
                    {
                        finalText += "Weapon: ";
                        break;
                    }
                case ItemType.Sheild:
                    {
                        finalText += "Sheild: ";
                        break;
                    }
                default:
                    {
                        finalText += "Item: ";
                        break;
                    }
            }

            return finalText + name;
        }
    }
}
