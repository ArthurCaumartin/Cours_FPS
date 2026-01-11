using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int _priority;
    public int Priority => _priority;
}