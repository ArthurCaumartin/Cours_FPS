using UnityEngine;

public class WeaponAimable : Weapon
{
    [SerializeField] protected AimCursor aimCursor;
    [SerializeField] protected Camera playerCamera;
    [SerializeField] protected Transform aimTransform;
    protected Vector3 startLocalPos;
    protected bool isAiming;

    protected virtual void Start()
    {
        playerCamera = Camera.main;
        startLocalPos = transform.localPosition;
    }

    protected virtual void Update()
    {
        Vector3 targetPos;
        if (isAiming)
        {
            Vector3 worldAimPos = playerCamera.transform.position;
            targetPos = transform.parent.InverseTransformPoint(worldAimPos);
            targetPos -= aimTransform.localPosition;
        }
        else
        {
            targetPos = startLocalPos;
        }

        transform.localPosition =
        Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * 50);
    }

    public override void ShootSecondary(bool isInputPressed)
    {
        base.ShootSecondary(isInputPressed);
        isAiming = isInputPressed;
    }
}