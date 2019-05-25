using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Mike4ruls.General.UI
{
    public class ItemIconScript : MonoBehaviour
    {


        static public ItemIconScript swap1;
        static public ItemIconScript swap2;
        static public bool rdyToSwap = false;
        static public bool dragEnded = false;
        static public bool clicked = false;

        static public void SwapItemIconsContents()
        {
            if (swap1 != null && swap2 != null)
            {
                Item oldItem = swap1.myItem;

                swap1.StoreItem(swap2.myItem);
                swap2.StoreItem(oldItem);
            }

            WipeSwap();
        }

        static public void SwapItemIcons()
        {
            if (swap1 != null && swap2 != null)
            {
                Vector3 oldPos = swap1.lastPosition;

                Vector3 swap1Scale = swap1.transform.localScale;
                Vector3 swap2Scale = swap2.transform.localScale;

                swap1.SetLastPosition(swap2.lastPosition);
                swap2.SetLastPosition(oldPos);

                swap1.transform.localScale = swap2Scale;
                swap2.transform.localScale = swap1Scale;
            }

            WipeSwap();
        }
        static public void WipeSwap()
        {
            swap1 = null;
            swap1 = null;
            rdyToSwap = false;
        }

        [HideInInspector]
        public string emptyText = "NONE";

        private Item myItem = null;
        private TextMeshProUGUI myText;
        private Image buttonImage;
        private Vector3 lastPosition;
        private Vector3 offset;
        private bool followMouseCursor;

        // Use this for initialization
        void Awake()
        {
            if (lastPosition == null)
            {
                lastPosition = transform.position;
            }

            buttonImage = transform.GetChild(0).GetComponent<Image>();
            myText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (followMouseCursor)
            {
                transform.position = Input.mousePosition + offset;
            }
            else
            {
                transform.position = lastPosition;
            }

            if (myItem != null)
            {
                Color textColor = GetColor();
                myText.color = textColor;
                myText.text = myItem.ItemToString();
            }
            else
            {
                myText.color = Color.white;
                myText.text = emptyText;
            }
        }
        public void PickUpIcon()
        {
            swap1 = this;
            buttonImage.raycastTarget = false;
            offset = transform.position - Input.mousePosition;
            followMouseCursor = true;
        }
        public void DropIcon()
        {
            rdyToSwap = (swap1 != null) && (swap2 != null);
            dragEnded = true;
            //SwapItemIcons();
            buttonImage.raycastTarget = true;
            followMouseCursor = false;
        }

        public void SetLastPosition(Vector3 pos)
        {
            lastPosition = pos;
        }
        public void SetSwap2ToThis()
        {
            if (swap1 == this)
            {
                swap2 = null;
            }
            else
            {
                swap2 = this;
            }
        }
        public void SetSwap2ToNull()
        {
            swap2 = null;

        }
        public void StoreItem(Item newItem)
        {
            myItem = newItem;
        }
        public Item GetItem()
        {
            return myItem;
        }
        public GameObject GetParent()
        {
            return this.transform.parent.gameObject;
        }
        public void IsClicking()
        {
            swap1 = this;
            if (transform.position == lastPosition)
            {
                clicked = true;
            }
        }
        Color GetColor()
        {
            Color newColor = Color.white;

            switch (myItem.rarityType)
            {
                case RarityType.Common:
                    {
                        newColor = GameManager._CommonRarityColor;
                        break;
                    }
                case RarityType.Uncommon:
                    {
                        newColor = GameManager._UnCommonRarityColor;
                        break;
                    }
                case RarityType.Rare:
                    {
                        newColor = GameManager._RareRarityColor;
                        break;
                    }
                case RarityType.Epic:
                    {
                        newColor = GameManager._EpicRarityColor;
                        break;
                    }
                case RarityType.Legendary:
                    {
                        newColor = GameManager._LegendaryRarityColor;
                        break;
                    }
            }

            newColor = new Color(newColor.r, newColor.g, newColor.b, 1.0f);

            return newColor;
        }
    }
}
