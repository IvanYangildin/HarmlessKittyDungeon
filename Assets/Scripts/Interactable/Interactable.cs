using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void OnInteract(PlayerInteract interactor);
    public abstract void OnLook(PlayerInteract interactor);
    public abstract void OnLookAway(PlayerInteract interactor);
}