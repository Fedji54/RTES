using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Pawn", menuName = "Winter Universe/Pawn/New Pawn")]
    public class LocalPawnConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField, TextArea] private string _description = "Description";
        [SerializeField] private Sprite _icon;
        [SerializeField] private List<string> _orderOfDay = new();

        public string DisplayName => _displayName;
        public string Description => _description;
        public Sprite Icon => _icon;
    }
}