using UnityEngine;

public class Weapon_Visual : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _reloadHash = Animator.StringToHash("Reloading");

    void Start()
    {
        print("lksdjfgnklsjdhng");
        // _animator = GetComponentInChildren<Animator>();
        _animator.Play("Reloading");
    }
}
