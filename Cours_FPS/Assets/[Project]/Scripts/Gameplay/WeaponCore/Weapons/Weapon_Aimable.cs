using UnityEngine;

public class Weapon_Aimable : WeaponBehavior
{
    [SerializeField] protected Transform aimTransform;
    [SerializeField] protected Camera aimRenderCamera;
    protected Camera playerCamera;
    protected Vector3 startLocalPos;
    protected bool isAiming;

    protected virtual void Start()
    {
        playerCamera = Camera.main;
        startLocalPos = transform.localPosition;
    }

    public override void EnableWeapon(bool value)
    {
        base.EnableWeapon(value);
        aimRenderCamera.enabled = true;
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
        Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * 15);
    }

    public override void ShootSecondary(bool isInputPressed)
    {
        base.ShootSecondary(isInputPressed);
        isAiming = isInputPressed;
    }
}
