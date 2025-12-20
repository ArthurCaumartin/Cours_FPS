using UnityEngine;

public class Weapon_Sniper : Weapon_Aimable
{
    public override void Shoot(bool isInputPressed)
    {
        if (!isInputPressed) return;
        if (!canShoot) return;

        Vector3 projDirection = aimCursor.GetWorldAimPoint() - shootPoint.position;
        Quaternion projectileRot = Quaternion.LookRotation(projDirection.normalized);

        Debug.DrawRay(shootPoint.position, projDirection, Color.yellow, 0.5f);

        if (!isAiming)
        {
            Vector3 spread = new Vector3(
                Random.Range(-50, 50),
                Random.Range(-50, 50),
                0);
            projectileRot = projectileRot * Quaternion.Euler(spread);
        }

        Projectile newProj = Instantiate(projectilePrefab, shootPoint.position, projectileRot);
        newProj.Initilaze(damage, projectileSpeed, layerMask);

        playerLook.AddRecoil(recoilAmout, recoilDuration, recoilCurve);
        StartCoroutine(CanShootDelay(1f / shootPerSecond));
    }
}
