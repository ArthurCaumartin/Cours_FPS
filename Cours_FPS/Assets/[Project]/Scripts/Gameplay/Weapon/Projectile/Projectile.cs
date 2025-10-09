using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float lifeTime = 5f;
    protected float damage;
    protected float speed;
    protected LayerMask layerMask;

    public virtual void Initilaze(float damage, float speed, LayerMask layerMask)
    {
        this.damage = damage;
        this.speed = speed;
        this.layerMask = layerMask;
        Destroy(gameObject, lifeTime);
    }
}
