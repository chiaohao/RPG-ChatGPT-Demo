using UnityEngine;

namespace RpgChatGPTDemo.Character
{
    public class Protagonist : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Cinemachine.CinemachineFreeLook _vCam;
        [SerializeField] private float _moveSpeed = 1f;

        public void Move(Vector2 inputValue)
        {
            if (this == null)
                return;
            var forward2d = new Vector2(_cameraTransform.forward.x, _cameraTransform.forward.z).normalized;
            var right2d = new Vector2(_cameraTransform.right.x, _cameraTransform.right.z).normalized;
            var move2d = forward2d * inputValue.y + right2d * inputValue.x;
            transform.position += new Vector3(move2d.x, 0f, move2d.y) * _moveSpeed * Time.deltaTime;
        }

        public void Rotate(Vector2 inputValue)
        {
            if (this == null)
                return;
            _vCam.m_XAxis.m_InputAxisValue = inputValue.x;
            _vCam.m_YAxis.m_InputAxisValue = inputValue.y;
        }
    }
}