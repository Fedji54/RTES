using System;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldCameraManager : MonoBehaviour
    {
        public Action<Interactable> OnInteractableChanged;

        [Header("Input")]
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _lookSpeed = 5f;
        [SerializeField] private float _rotateSpeed = 90f;
        [SerializeField] private float _zoomSpeed = 5f;
        [Header("Zoom")]
        [SerializeField] private Transform _cameraRoot;
        [SerializeField] private Vector3 _zoomDirection = new(0f, 1f, -0.5f);
        [SerializeField] private float _currentZoom = 1f;
        [SerializeField] private float _minZoom = 0.5f;
        [SerializeField] private float _maxZoom = 10f;
        [SerializeField] private float _zoomStepAmount = 0.25f;
        [SerializeField] private float _zoomMultiplier = 20f;
        [Header("Cursor")]
        [SerializeField] private Transform _cursor;
        [SerializeField] private Vector3 _cursorMaxOffset = new(15f, 0f, 5f);
        [Header("Hovering")]
        [SerializeField] private Vector3 _raycastOffset = new(0f, 50f, 0f);
        [SerializeField] private float _raycastDistance = 100f;
        [SerializeField] private Vector3 _heightOffset = new(0f, 0.1f, 0f);
        [Header("Interaction")]
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
                transform.Translate(_moveSpeed * _currentZoom * Time.unscaledDeltaTime * (Vector3.forward * _moveInput.y + Vector3.right * _moveInput.x));
            }
            if (_lookInput != Vector2.zero)
            {
                _cursor.Translate(_lookSpeed * _currentZoom * Time.unscaledDeltaTime * (Vector3.forward * _lookInput.y + Vector3.right * _lookInput.x));
            }
            _cursor.localPosition = new(Mathf.Clamp(_cursor.localPosition.x, -_cursorMaxOffset.x * _currentZoom, _cursorMaxOffset.x * _currentZoom), _cursor.localPosition.y, Mathf.Clamp(_cursor.localPosition.z, -_cursorMaxOffset.z * _currentZoom, _cursorMaxOffset.z * _currentZoom));
            if (_rotateInput != 0f)
            {
                transform.Rotate(_rotateSpeed * _rotateInput * Time.unscaledDeltaTime * Vector3.up);
            }
            _cameraRoot.localPosition = Vector3.Lerp(_cameraRoot.localPosition, _currentZoom * _zoomMultiplier * _zoomDirection, _zoomSpeed * Time.unscaledDeltaTime);
            if (Physics.Raycast(transform.position + _raycastOffset, Vector3.down, out _groundHit, _raycastDistance, WorldManager.StaticInstance.LayerManager.GroundMask))
            {
                transform.position = _groundHit.point + _heightOffset;
            }
            if (Physics.Raycast(_cursor.position + _raycastOffset, Vector3.down, out _groundHit, _raycastDistance, WorldManager.StaticInstance.LayerManager.GroundMask))
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

        public void ChangeZoom(float value)
        {
            if (value > 0f)
            {
                _currentZoom = Mathf.Clamp(_currentZoom - _zoomStepAmount, _minZoom, _maxZoom);
            }
            else if (value < 0f)
            {
                _currentZoom = Mathf.Clamp(_currentZoom + _zoomStepAmount, _minZoom, _maxZoom);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Interactable interactable = other.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                interactable.ToggleOutline(true);
                OnInteractableChanged?.Invoke(interactable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Interactable interactable = other.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                interactable.ToggleOutline(false);
                OnInteractableChanged?.Invoke(null);
            }
        }
    }
}