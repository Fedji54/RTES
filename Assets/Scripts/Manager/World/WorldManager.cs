using UnityEngine;

namespace WinterUniverse
{
    public class WorldManager : Singleton<WorldManager>
    {
        private WorldCameraManager _cameraManager;
        private WorldInputManager _inputManager;
        private WorldLayerManager _layerManager;
        private WorldPlayerManager _playerManager;
        private WorldSoundManager _soundManager;
        private WorldTimeManager _timeManager;
        private WorldUIManager _UIManager;

        public WorldCameraManager CameraManager => _cameraManager;
        public WorldInputManager InputManager => _inputManager;
        public WorldLayerManager LayerManager => _layerManager;
        public WorldPlayerManager PlayerManager => _playerManager;
        public WorldSoundManager SoundManager => _soundManager;
        public WorldTimeManager TimeManager => _timeManager;
        public WorldUIManager UIManager => _UIManager;

        protected override void Awake()
        {
            base.Awake();
            // move this to loading screen
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //
            _cameraManager = FindFirstObjectByType<WorldCameraManager>();
            _inputManager = FindFirstObjectByType<WorldInputManager>();
            _layerManager = FindFirstObjectByType<WorldLayerManager>();
            _playerManager = FindFirstObjectByType<WorldPlayerManager>();
            _soundManager = FindFirstObjectByType<WorldSoundManager>();
            _timeManager = FindFirstObjectByType<WorldTimeManager>();
            _UIManager = FindFirstObjectByType<WorldUIManager>();
            //
            _UIManager.Initialize();
            _soundManager.Initialize();
            _timeManager.Initialize();
            //
            _cameraManager.ToggleState(true);
        }
    }
}