using System.Threading.Tasks;
using UnityEngine;

public class Weapon_Sniper : Weapon
{
    [SerializeField] private AimCursor _aimCursor;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Transform _aimTransform;
    [SerializeField] private float _aimDistance = 0.5f;
    [SerializeField] private float _zoomFOV = 30f;
    private float _defaultFOV;
    private Vector3 _startLocalPos;
    private bool _isAiming;

    private void Start()
    {
        _playerCamera = Camera.main;
        _defaultFOV = _playerCamera.fieldOfView;
        _startLocalPos = transform.localPosition;
    }

    private void Update()
    {
        transform.forward = (_aimCursor.GetWorldAimPoint() - transform.position).normalized;

        Vector3 targetPos;
        if (_isAiming)
        {
            Vector3 worldAimPos = _playerCamera.transform.position + _playerCamera.transform.forward;
            targetPos = worldAimPos - _aimTransform.localPosition;
        }
        else
        {
            targetPos = transform.parent.TransformPoint(_startLocalPos);
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10);
    }

    public override void Shoot(bool isInputPressed)
    {
        if (!isInputPressed) return;
        if (!canShoot) return;

        Quaternion projectileRot;
        if (_isAiming)
        {
            projectileRot = transform.rotation;
        }
        else
        {
            Vector3 spread = new Vector3(
                Random.Range(-50, 50),
                Random.Range(-50, 50),
                0);
            projectileRot = shootPoint.rotation * Quaternion.Euler(spread);
        }

        Projectile newProj = Instantiate(projectilePrefab, shootPoint.position, projectileRot);
        newProj.Initilaze(damage, projectileSpeed, layerMask);

        StartCoroutine(CanShootDelay(1f / shootPerSecond));
    }

    public override void ShootSecondary(bool isInputPressed)
    {
        base.ShootSecondary(isInputPressed);
        _isAiming = isInputPressed;
    }


}
