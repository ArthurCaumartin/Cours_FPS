using UnityEngine;

public class WeaponAimable : Weapon
{
    [SerializeField] protected AimCursor aimCursor;
    [SerializeField] protected Camera playerCamera;
    [SerializeField] protected Transform aimTransform;
    [SerializeField] protected float aimDistance = 0.5f;
    [SerializeField] protected float zoomFOV = 30f;
    protected float defaultFOV;
    protected Vector3 startLocalPos;
    protected bool isAiming;

    protected virtual void Start()
    {
        playerCamera = Camera.main;
        defaultFOV = playerCamera.fieldOfView;
        startLocalPos = transform.localPosition;
    }

    protected virtual void Update()
    {
        Vector3 targetLocalPos;
        if (isAiming)
        {
            Vector3 worldAimPos = playerCamera.transform.position;
            targetLocalPos = transform.parent.InverseTransformPoint(worldAimPos);
            targetLocalPos -= aimTransform.localPosition;
        }
        else
        {
            targetLocalPos = startLocalPos;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocalPos, Time.deltaTime * 10);
    }

    public override void ShootSecondary(bool isInputPressed)
    {
        base.ShootSecondary(isInputPressed);
        isAiming = isInputPressed;
    }
}