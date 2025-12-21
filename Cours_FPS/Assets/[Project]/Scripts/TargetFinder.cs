
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private Transform _currentTarget;

    public Transform Target => _currentTarget;
}