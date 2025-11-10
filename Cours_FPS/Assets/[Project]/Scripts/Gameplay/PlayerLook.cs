using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform _orientationContainer;
    [SerializeField] private float _lookSensitivity = 2f;
    [SerializeField] private float _lookSensitivityAim = 2f;
    [SerializeField] private float _maxLookAngle = 90f;
    private Vector2 _lookInput;
    private float _xRotation = 0f;
    private bool _isAiming = false;


    private void Update()
    {
        float mouseX = _lookInput.x * Time.deltaTime * (_isAiming ? _lookSensitivityAim : _lookSensitivity);
        transform.Rotate(0, mouseX, 0);

        float mouseY = _lookInput.y * _lookSensitivity * Time.deltaTime;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -_maxLookAngle, _maxLookAngle);
        _orientationContainer.localEulerAngles = new Vector3(_xRotation, 0, 0);
    }

    private void OnLook(InputValue value)
    {
        _lookInput = value.Get<Vector2>();
    }

    public async void AddRecoil(float recoilAmount, float recoilSpeed)
    {
        // print("Add recoil : " + recoilAmount);
        float totalRecoil = recoilAmount;
        while (totalRecoil > 0)
        {
            _xRotation -= Time.deltaTime * recoilAmount / recoilSpeed;
            totalRecoil -= Time.deltaTime * recoilAmount / recoilSpeed;
            await Task.Yield();
            _xRotation = Mathf.Clamp(_xRotation, -_maxLookAngle, _maxLookAngle);
        }
    }

    public Vector3 GetLookDirection()
    {
        return _orientationContainer.forward;
    }
}
