using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class WorldInputManager : MonoBehaviour
    {
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private float _rotateInput;

        public Vector2 MoveInput => _moveInput;
        public Vector2 LookInput => _lookInput;
        public float RotateInput => _rotateInput;

        public void OnMove(InputValue value) => _moveInput = value.Get<Vector2>();
        public void OnLook(InputValue value) => _lookInput = value.Get<Vector2>();
        public void OnRotate(InputValue value) => _rotateInput = value.Get<float>();
        public void OnZoom(InputValue value) => WorldManager.StaticInstance.CameraManager.ChangeZoom(value.Get<float>());
        public void OnInteract() => WorldManager.StaticInstance.CameraManager.Interact();
        public void OnResetCamera() => WorldManager.StaticInstance.CameraManager.ResetCamera();
        public void OnToggleGamePause() => WorldManager.StaticInstance.TimeManager.ToggleGamePause();
        public void OnDecelerateGameSpeed() => WorldManager.StaticInstance.TimeManager.DecelerateGameSpeed();
        public void OnNormalizeGameSpeed() => WorldManager.StaticInstance.TimeManager.NormalizeGameSpeed();
        public void OnAccelerateGameSpeed() => WorldManager.StaticInstance.TimeManager.AccelerateGameSpeed();
    }
}