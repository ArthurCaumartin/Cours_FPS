using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _lookSensitivity = 2f;
    [SerializeField] private float _maxLookAngle = 90f;
    private Vector2 _lookInput;
    private float _xRotation = 0f;


    private void FixedUpdate()
    {
        float mouseX = _lookInput.x * _lookSensitivity * Time.fixedDeltaTime;
        transform.Rotate(0, mouseX, 0);


        float mouseY = _lookInput.y * _lookSensitivity * Time.fixedDeltaTime;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -_maxLookAngle, _maxLookAngle);
        _playerCamera.transform.localEulerAngles = new Vector3(_xRotation, 0, 0);
    }

    private void OnLook(InputValue value)
    {
        _lookInput = value.Get<Vector2>();
    }

    public async void AddRecoil(float recoilAmount, float recoilSpeed)
    {
        float totalRecoil = recoilAmount;
        while (totalRecoil > 0)
        {
            _xRotation += Time.deltaTime * recoilSpeed;
            totalRecoil -= Time.deltaTime * recoilSpeed;
            await Task.Yield();
            _xRotation = Mathf.Clamp(_xRotation, -_maxLookAngle, _maxLookAngle);
        }

    }
}
