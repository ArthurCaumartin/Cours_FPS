using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth = 100;

    public float MaxHealth { get; }
    public float CurrentHealth { get; }

    public virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    public virtual void Heal(float heal)
    {
        _currentHealth += heal;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }

    public virtual void Kill()
    {
        Destroy(gameObject);
    }
}
