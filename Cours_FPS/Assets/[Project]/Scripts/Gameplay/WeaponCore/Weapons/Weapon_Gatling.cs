using UnityEngine;

public class Weapon_Gatling : WeaponBehavior
{
    [Header("Gatling Gun Stats : ")]
    [SerializeField] private float _maxAttackSpeedBonus = 3f;
    [SerializeField] private float _bonusDuration = 5f;
    private float _timeSinceLastShoot;
    private float _currentAttackSpeedBonus;

    private void Update()
    {
        _timeSinceLastShoot += Time.deltaTime;
        if (_timeSinceLastShoot >= _bonusDuration)
            _currentAttackSpeedBonus = 1f;
    }

    public override void Shoot(bool isInputPressed)
    {
        if (!isInputPressed) return;
        if (!canShoot) return;
        if (currentAmmo <= 0) return;

        Projectile newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        newProjectile.Initilaze(damage, projectileSpeed, layerMask);

        _timeSinceLastShoot = 0f;
        _currentAttackSpeedBonus = Mathf.Clamp(_currentAttackSpeedBonus + 0.1f, 1f, _maxAttackSpeedBonus);

        canShoot = false;
        StartCoroutine(CanShootDelay(1f / (shootPerSecond * _currentAttackSpeedBonus)));
        playerLook?.AddRecoil(recoilAmout, recoilDuration, recoilCurve);
        currentAmmo--;
    }
}