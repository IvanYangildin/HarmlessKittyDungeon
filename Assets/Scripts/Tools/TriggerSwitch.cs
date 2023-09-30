using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSwitch : MonoBehaviour
{
    [SerializeField]
    Transform specificTarget;
    [SerializeField]
    LayerMask keyLayer;

    public UnityEvent Triggered = new UnityEvent();
    public UnityEvent LeftTrigger = new UnityEvent();


    private bool isRightCollision(Collider other)
    {
        bool isSpecific = (specificTarget != null) && ((other.transform.parent == specificTarget) ||
            (other.transform == specificTarget));
        return (((1 << other.gameObject.layer) & keyLayer) != 0) || isSpecific;
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRightCollision(other))
        {
            Triggered.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isRightCollision(other))
        {
            LeftTrigger.Invoke();
        }
    }
}
