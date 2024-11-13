using UnityEngine;

namespace WinterUniverse
{
    public class WorldManager : Singleton<WorldManager>
    {
        private WorldInputManager _inputManager;
        private WorldCameraManager _cameraManager;
        private WorldLayerManager _layerManager;
        private WorldPlayerManager _playerManager;
        private WorldSoundManager _soundManager;
        private WorldTimeManager _timeManager;

        public WorldInputManager InputManager => _inputManager;
        public WorldCameraManager CameraManager => _cameraManager;
        public WorldLayerManager LayerManager => _layerManager;
        public WorldPlayerManager PlayerManager => _playerManager;
        public WorldSoundManager SoundManager => _soundManager;
        public WorldTimeManager TimeManager => _timeManager;

        protected override void Awake()
        {
            base.Awake();
            // move this to loading screen
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //
            _inputManager = FindFirstObjectByType<WorldInputManager>();
            _cameraManager = FindFirstObjectByType<WorldCameraManager>();
            _layerManager = FindFirstObjectByType<WorldLayerManager>();
            _playerManager = FindFirstObjectByType<WorldPlayerManager>();
            _soundManager = FindFirstObjectByType<WorldSoundManager>();
            _timeManager = FindFirstObjectByType<WorldTimeManager>();
            //
            _soundManager.Initialize();
            _timeManager.Initialize();
            _timeManager.UnpauseGame();
            _cameraManager.ToggleState(true);
        }
    }
}