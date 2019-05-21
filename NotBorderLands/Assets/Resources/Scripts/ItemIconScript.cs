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

        private Item myItem;
        private TextMeshProUGUI myText;
        private Image buttonImage;
        private Vector3 lastPosition;
        private Vector3 offset;
        private bool followMouseCursor;

        private ItemIconScript hitItemIcon = null;

        // Use this for initialization
        void Awake()
        {
            lastPosition = transform.position;
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
                myText.text = myItem.ItemToString();
            }
            else
            {
                myText.text = "NONE";
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
    }
}
