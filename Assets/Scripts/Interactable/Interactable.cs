using UnityEngine;

namespace WinterUniverse
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] private Transform _entryPoint;
        [SerializeField] private GameObject _outline;

        public Transform EntryPoint => _entryPoint;

        protected virtual void Start()
        {
            ToggleOutline(false);
        }

        public void ToggleOutline(bool enabled)
        {
            _outline.SetActive(enabled);
        }

        public abstract void Interact();
    }
}