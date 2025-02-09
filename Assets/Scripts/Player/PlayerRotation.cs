using UnityEngine;

namespace WinterUniverse
{
    public class PlayerRotation : MonoBehaviour
    {
        private float _xRot;

        [SerializeField] private Transform _head;
        [SerializeField] private bool _invertHorizontalLook = false;
        [SerializeField] private bool _invertVerticalLook = false;
        [SerializeField] private float _horizontalLookSpeed = 10f;
        [SerializeField] private float _verticalLookSpeed = 5f;
        [SerializeField] private float _verticalLookAngleLimit = 90f;

        public void Rotate(Vector2 input)
        {
            if (input.x != 0f)
            {
                transform.Rotate(Vector3.up * input.x * (_invertHorizontalLook ? -1f : 1f) * _horizontalLookSpeed * Time.deltaTime);
            }
            if (input.y != 0f)
            {
                _xRot = Mathf.Clamp(_xRot - (input.y * (_invertVerticalLook ? -1f : 1f) * _verticalLookSpeed * Time.deltaTime), -_verticalLookAngleLimit, _verticalLookAngleLimit);
                _head.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
            }
        }
    }
}