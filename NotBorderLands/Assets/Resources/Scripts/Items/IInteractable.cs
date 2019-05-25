using UnityEngine;
namespace Mike4ruls.General
{
    public enum InteractType
    {
        Open,
        PickUp,
        Use,
        TalkTo
    }
    public interface IInteractable
    {
        [SerializeField]
        InteractType interactType { get; set; }
        bool canInteract { get; set; }
        void Interact(PlayerBase player);
    }
}
