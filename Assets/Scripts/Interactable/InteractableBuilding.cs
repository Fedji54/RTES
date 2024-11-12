using UnityEngine;

namespace WinterUniverse
{
    public class InteractableBuilding : Interactable
    {
        public override void Interact()
        {
            Debug.Log("Interacted!");
        }
    }
}