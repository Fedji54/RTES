using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Interactable", menuName = "Winter Universe/Interactable/New Interactable")]
    public class InteractableConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField, TextArea] private string _description = "Description";
        [SerializeField] private string _actionName = "Enter in";
        [SerializeField] private Sprite _icon;
        [SerializeField] private List<LocalPawnManager> _pawns = new();
        [SerializeField] private List<string> _actions = new();

        public string DisplayName => _displayName;
        public string Description => _description;
        public string ActionName => _actionName;
        public Sprite Icon => _icon;
    }
}