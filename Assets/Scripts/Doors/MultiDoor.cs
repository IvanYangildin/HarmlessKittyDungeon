using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDoor : MonoBehaviour
{
    [SerializeField]
    private List<DoorObject> subDoors = new List<DoorObject>();

    public void Open()
    {
        foreach (DoorObject door in subDoors)
        {
            door.Open();
        }
    }

    public void Close()
    {
        foreach (DoorObject door in subDoors)
        {
            door.Close();
        }
    }

    public void Lock()
    {
        foreach (DoorObject door in subDoors)
        {
            door.Lock();
        }
    }

    public void Unlock()
    {
        foreach (DoorObject door in subDoors)
        {
            door.Unlock();
        }
    }

}
