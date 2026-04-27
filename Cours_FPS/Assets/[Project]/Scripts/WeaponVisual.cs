using System;
using System.Collections;
using UnityEngine;

public class WeaponVisual : MonoBehaviour
{
    private Animator _animator;
    private int _reloadSpeedHash = Animator.StringToHash("ReloadSpeed");
    private int _reloadHash = Animator.StringToHash("Reloading");
    private Coroutine _reloadActionDelay;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Reload(float duration, Action toDoAfterReload)
    {
        if (_reloadActionDelay != null) return;
        _animator.SetFloat(_reloadSpeedHash, 1 / duration);
        _animator.Play(_reloadHash);
        _reloadActionDelay = StartCoroutine(ActionDelay(toDoAfterReload, duration));
    }

    private IEnumerator ActionDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
        _reloadActionDelay = null;
    }
}
