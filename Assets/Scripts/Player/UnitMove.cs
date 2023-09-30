using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitMove : MonoBehaviour
{
    Rigidbody rb;

    [Min(0f)]
    public float speedFactor, maxSpeed, maxFallSpeed;

    [Min(0f)]
    public float horizontalDrag;

    Vector3 forceVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        DragHorVelocity(Time.deltaTime);
    }

    private void LateUpdate()
    {
        AdjustVelocity();
    }

    // move in direction v; input in local space
    public void MoveLocal(Vector2 v)
    {
        Vector3 forceNormal = transform.right * v.x + transform.forward * v.y;
        forceNormal = (forceNormal.magnitude > 0f) ? forceNormal.normalized : Vector3.zero; 

        rb.AddForce(forceNormal * speedFactor);
    }

    // move in direction v; input in world space
    public void MoveWorld(Vector3 v)
    {
        Vector3 forceNormal = (v.magnitude > 0f) ? v.normalized : Vector3.zero;

        rb.AddForce(forceNormal * speedFactor);
    }

    void DragHorVelocity(float dt)
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float vertSpeed = rb.velocity.y;

        float adjustSpeed = flatVelocity.magnitude - horizontalDrag * dt;
        flatVelocity = (adjustSpeed > 0f)? Vector3.ClampMagnitude(flatVelocity, adjustSpeed) : Vector3.zero;

        rb.velocity = new Vector3(flatVelocity.x, vertSpeed, flatVelocity.z);
    }

    void AdjustVelocity()
    {        
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float vertSpeed = rb.velocity.y;

        flatVelocity = Vector3.ClampMagnitude(flatVelocity, maxSpeed);
        vertSpeed = Mathf.Clamp(vertSpeed, -maxFallSpeed, maxFallSpeed);

        rb.velocity = new Vector3(flatVelocity.x, vertSpeed, flatVelocity.z);
    }
}
