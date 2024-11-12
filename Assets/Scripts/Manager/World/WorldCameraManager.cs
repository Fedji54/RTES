using UnityEngine;

namespace WinterUniverse
{
    public class WorldCameraManager : MonoBehaviour
    {
        [SerializeField] private Vector3 _raycastOffset;
        [SerializeField] private float _raycastDistance = 100f;
        [SerializeField] private Vector3 _heightOffset;
        [SerializeField] private Transform _cursor;
        [SerializeField] private Vector3 _cursorMaxOffset;
        [SerializeField] private float _moveSpeed = 8f;
        [SerializeField] private float _lookSpeed = 4f;
        [SerializeField] private float _rotateSpeed = 32f;
        [SerializeField] private float _interactionRadius = 0.5f;

        private RaycastHit _groundHit;
        private Collider[] _colliders = new Collider[5];
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private float _rotateInput;
        private bool _active;

        public void ToggleState(bool enabled)
        {
            _active = enabled;
        }

        private void Update()
        {
            if (!_active)
            {
                return;
            }
            _moveInput = WorldManager.StaticInstance.InputManager.MoveInput;
            _lookInput = WorldManager.StaticInstance.InputManager.LookInput;
            _rotateInput = WorldManager.StaticInstance.InputManager.RotateInput;
            if (_moveInput != Vector2.zero)
            {
                transform.Translate(_moveSpeed * Time.deltaTime * (Vector3.forward * _moveInput.y + Vector3.right * _moveInput.x));
            }
            if (_lookInput != Vector2.zero)
            {
                _cursor.Translate(_lookSpeed * Time.deltaTime * (Vector3.forward * _lookInput.y + Vector3.right * _lookInput.x));
                _cursor.localPosition = new(Mathf.Clamp(_cursor.localPosition.x, -_cursorMaxOffset.x, _cursorMaxOffset.x), _cursor.localPosition.y, Mathf.Clamp(_cursor.localPosition.z, -_cursorMaxOffset.z, _cursorMaxOffset.z));
            }
            if (_rotateInput != 0f)
            {
                transform.Rotate(_rotateSpeed * Time.deltaTime * Vector3.up);
            }
            if (Physics.Raycast(transform.position + _raycastOffset, Vector3.down, out _groundHit, _raycastDistance, WorldManager.StaticInstance.LayerManager.ObstacleMask))
            {
                transform.position = _groundHit.point + _heightOffset;
            }
            if (Physics.Raycast(_cursor.position + _raycastOffset, Vector3.down, out _groundHit, _raycastDistance, WorldManager.StaticInstance.LayerManager.ObstacleMask))
            {
                _cursor.position = _groundHit.point + _heightOffset;
            }
        }

        public void Interact()
        {
            if (!_active)
            {
                return;
            }
            int collidersCount = Physics.OverlapSphereNonAlloc(_cursor.position, _interactionRadius, _colliders, WorldManager.StaticInstance.LayerManager.InteractableMask);
            if (collidersCount > 0)
            {
                Interactable interactable = _colliders[0].GetComponentInParent<Interactable>();
                if (interactable != null)
                {
                    WorldManager.StaticInstance.PlayerManager.SetDestination(interactable);
                }
            }
        }

        public void ResetCamera()
        {
            if (!_active)
            {
                return;
            }
            transform.position = WorldManager.StaticInstance.PlayerManager.transform.position + _heightOffset;
        }

        private void OnTriggerEnter(Collider other)
        {
            Interactable interactable = other.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                interactable.ToggleOutline(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Interactable interactable = other.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                interactable.ToggleOutline(false);
            }
        }
    }
}