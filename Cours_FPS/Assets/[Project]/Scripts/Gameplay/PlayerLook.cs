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

    public async void AddRecoil(float recoilAmount, float recoilDuration, AnimationCurve recoilCurve)
    {
        print("Add recoil : " + recoilAmount + " | duration : " + recoilDuration);

        //TODO ajouter une animation curve en parametre + faire le recoil avec un lerp et conditioner avec un time
        // print("Add recoil : " + recoilAmount);
        float xSnap = _xRotation;
        float time = 0;
        while (time < 1)
        {
            // print("Recoiling | time : " + time + " / value : " + recoilCurve.Evaluate(time));
            float toAdd = recoilAmount * recoilCurve.Evaluate(time) * (Time.deltaTime / recoilDuration);
            _xRotation -= toAdd;
            time += Time.deltaTime / recoilDuration;

            await Task.Yield();
            _xRotation = Mathf.Clamp(_xRotation, -_maxLookAngle, _maxLookAngle);
        }

        print("RecoilEnd | from :" + _xRotation + " to : " + xSnap + " | total added : " + (xSnap - _xRotation));
    }

    public Vector3 GetLookDirection()
    {
        return _orientationContainer.forward;
    }
}
