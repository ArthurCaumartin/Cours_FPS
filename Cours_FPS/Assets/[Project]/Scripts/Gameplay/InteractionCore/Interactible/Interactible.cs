using UnityEngine;


public abstract class Interactible : MonoBehaviour
{
    public virtual void Interact(PlayerInteract interact)
    {
        print("Interact with : " + name);
    }
}
