using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractTrigger : Interactable
{
    public UnityEvent<PlayerInteract> OnInteractEvent;
    public UnityEvent<PlayerInteract> OnLookEvent;
    public UnityEvent<PlayerInteract> OnLookAwayEvent;


    public override void OnInteract(PlayerInteract interactor)
    {
        OnInteractEvent.Invoke(interactor);
    }

    public override void OnLook(PlayerInteract interactor)
    {
        OnLookEvent.Invoke(interactor);
    }

    public override void OnLookAway(PlayerInteract interactor)
    {
        OnLookAwayEvent.Invoke(interactor);
    }

}
