using UnityEngine;

public class TestInteract : Interactable
{
    public override void OnInteract(PlayerInteract interactor)
    {
        Debug.Log("Interacted with me");
    }

    public override void OnLook(PlayerInteract interactor)
    {
        Debug.Log("Look at me");
    }

    public override void OnLookAway(PlayerInteract interactor)
    {
        Debug.Log("Don't look away");
    }
}