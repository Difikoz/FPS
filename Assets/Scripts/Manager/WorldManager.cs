using UnityEngine;

namespace WinterUniverse
{
    public class WorldManager : Singleton<WorldManager>
    {
        private PlayerController _player;
        private WorldInputManager _inputManager;
        private WorldCameraManager _cameraManager;
        private WorldLayerManager _layerManager;

        public PlayerController Player => _player;
        public WorldInputManager InputManager => _inputManager;
        public WorldCameraManager CameraManager => _cameraManager;
        public WorldLayerManager LayerManager => _layerManager;

        protected override void Awake()
        {
            base.Awake();
            GetComponents();
            InitializeComponents();
        }

        private void GetComponents()
        {
            _player = FindFirstObjectByType<PlayerController>();
            _inputManager = GetComponentInChildren<WorldInputManager>();
            _cameraManager = GetComponentInChildren<WorldCameraManager>();
            _layerManager = GetComponentInChildren<WorldLayerManager>();
        }

        private void InitializeComponents()
        {
            _inputManager.Initialize();
            _cameraManager.Initialize();
        }

        private void Update()
        {
            _inputManager.OnUpdate();
        }

        private void LateUpdate()
        {
            _cameraManager.OnLateUpdate();
        }
    }
}