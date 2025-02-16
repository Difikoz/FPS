using UnityEngine;

namespace WinterUniverse
{
    public class WorldCameraManager : MonoBehaviour
    {
        public float TurnAngle;

        [SerializeField] private bool _invertHorizontalLook = false;
        [SerializeField] private bool _invertVerticalLook = false;
        [SerializeField, Range(1f, 100f)] private float _horizontalLookSpeed = 10f;
        [SerializeField, Range(1f, 100f)] private float _verticalLookSpeed = 10f;
        [SerializeField, Range(30f, 90f)] private float _horizontalLookAngleLimit = 60f;
        [SerializeField, Range(60f, 90f)] private float _verticalLookAngleLimit = 90f;

        private Camera _camera;
        private Vector2 _input;
        private float _xRot;

        public Camera Camera => _camera;

        public void Initialize()
        {
            _camera = GetComponentInChildren<Camera>();
        }

        public void OnLateUpdate()
        {
            transform.position = WorldManager.StaticInstance.Player.PawnAnimator.Eyes.position;
            //transform.rotation = WorldManager.StaticInstance.Player.PawnAnimator.Eyes.rotation;
            _input = WorldManager.StaticInstance.InputManager.LookInput;
            if (_input.x != 0f)
            {
                TurnAngle = Mathf.Abs(Vector3.SignedAngle(transform.forward, WorldManager.StaticInstance.Player.transform.forward, Vector3.up));
                if (TurnAngle <= _horizontalLookAngleLimit)
                {
                    transform.Rotate(Vector3.up * _input.x * (_invertHorizontalLook ? -1f : 1f) * _horizontalLookSpeed * Mathf.InverseLerp(_horizontalLookAngleLimit, 0f, TurnAngle) * Time.deltaTime);
                }
            }
            if (_input.y != 0f)
            {
                _xRot = Mathf.Clamp(_xRot - (_input.y * (_invertVerticalLook ? -1f : 1f) * _verticalLookSpeed * Time.deltaTime), -_verticalLookAngleLimit, _verticalLookAngleLimit);
                _camera.transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
            }
        }
    }
}