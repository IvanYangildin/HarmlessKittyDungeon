using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionInvoker : MonoBehaviour
{
    public Action<Collision> CollisionEnter, CollisionStay, CollisionExit;

    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnter?.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        CollisionStay?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        CollisionExit?.Invoke(collision);
    }
}