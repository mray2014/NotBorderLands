using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mike4ruls.General.Items
{
    public class RarityParticleEffectScript : MonoBehaviour {
        private ParticleSystem particleSystem;
        private Item myItem;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update()
        {
            if (myItem != null)
            {
                Color myRarity = GameManager._CommonRarityColor;

                switch (myItem.rarityType)
                {
                    case RarityType.Uncommon:
                        {
                             myRarity = GameManager._UnCommonRarityColor;
                            break;
                        }
                    case RarityType.Rare:
                        {
                            myRarity = GameManager._RareRarityColor;
                            break;
                        }
                    case RarityType.Epic:
                        {
                            myRarity = GameManager._EpicRarityColor;
                            break;
                        }
                    case RarityType.Legendary:
                        {
                            myRarity = GameManager._LegendaryRarityColor;
                            break;
                        }
                }

                transform.position = myItem.transform.position;
                 ParticleSystem.ColorOverLifetimeModule mod = particleSystem.colorOverLifetime;

                Gradient grad = new Gradient();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(myRarity, 0.0f), new GradientColorKey(myRarity, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

                mod.color = grad;

                if (myItem.HasItemBeenPickedUp())
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
        public void ActivateRarityEffect(Item newItem)
        {
            myItem = newItem;
            particleSystem = GetComponent<ParticleSystem>();
            this.gameObject.SetActive(true);
        }
    }
}
