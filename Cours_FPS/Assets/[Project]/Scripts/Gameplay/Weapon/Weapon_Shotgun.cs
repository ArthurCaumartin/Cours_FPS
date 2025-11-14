using UnityEngine;

public class Weapon_Shotgun : Weapon
{
    [SerializeField] private int _projectileCount = 6;
    [SerializeField, Range(0f, 60)] private float _spreadAngle = 0.1f;

    public override void Shoot(bool isInputPressed)
    {
        if (!isInputPressed) return;
        if (!canShoot) return;

        for (int i = 0; i < _projectileCount; i++)
        {
            Vector3 spread = new Vector3(
                Random.Range(-_spreadAngle, _spreadAngle),
                Random.Range(-_spreadAngle, _spreadAngle),
                0);
            Quaternion newRotation = shootPoint.rotation * Quaternion.Euler(spread);

            Projectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, newRotation);
            newProjectile.Initilaze(damage, projectileSpeed, layerMask);
        }

        playerLook.AddRecoil(recoilAmout, recoilDuration, recoilCurve);
        canShoot = false;
        StartCoroutine(CanShootDelay(1f / shootPerSecond));
    }
}