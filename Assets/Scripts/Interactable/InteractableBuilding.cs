using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class InteractableBuilding : Interactable
    {
        private List<LocalPawnManager> _enteredPawns = new();

        public void Enter(LocalPawnManager pawn)
        {
            _enteredPawns.Add(pawn);
        }

        public void Exit(LocalPawnManager pawn)
        {
            _enteredPawns.Remove(pawn);
        }

        public override void Interact()
        {
            Debug.Log("Interacted!");
        }
    }
}