using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField]
    GameObject item;
    [SerializeField]
    DoorObject chestLid;
    [SerializeField]
    AudioSource openAudio;

    bool isEmpty = false;
    public string LookChest => isEmpty ? "" : "E to open";

    [SerializeField]
    string openMessage;
    [SerializeField]
    float openMessageDuration;

    public void GiveItem(PlayerInteract interactor)
    {
        Weapon weapon = Instantiate(item,
                interactor.GetComponent<PlayerAttack>().Arsenal.transform).GetComponent<Weapon>();
        interactor.GetComponent<PlayerAttack>().AddNewWeapon(weapon);
        isEmpty = true;
    }

    public override void OnInteract(PlayerInteract interactor)
    {
        if (!isEmpty)
        {
            PlayerMessage message = interactor.GetComponent<PlayerMessage>();
            message?.ShowMessage(openMessage, openMessageDuration);

            chestLid.Open();
            openAudio.Play();
            GiveItem(interactor);
        }
    }

    public override void OnLook(PlayerInteract interactor)
    {
        if (!isEmpty)
        {
            PlayerMessage message = interactor.GetComponent<PlayerMessage>();
            message?.ShowMessage(LookChest, 0);
        }

    }

    public override void OnLookAway(PlayerInteract interactor)
    {
        if (!isEmpty)
        {
            PlayerMessage message = interactor.GetComponent<PlayerMessage>();
            message?.HideMessage(LookChest);
        }
    }

}
