using System;
using System.Collections;
using UnityEngine;

public class WeaponVisual : MonoBehaviour
{
    private Animator _animator;
    private int _reloadSpeedHash = Animator.StringToHash("ReloadSpeed");
    private int _reloadHash = Animator.StringToHash("Reloading");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Reload(float duration, Action toDoAfterReload)
    {
        _animator.SetFloat(_reloadSpeedHash, 1 / duration);
        _animator.Play(_reloadHash);
        StartCoroutine(ActionDelay(toDoAfterReload, duration));
    }

    private IEnumerator ActionDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
}
