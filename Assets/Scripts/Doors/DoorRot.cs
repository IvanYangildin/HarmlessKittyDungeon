using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRot : DoorObject
{
    [SerializeField]
    // seeting object itself will be unstable for movement
    Transform doorBody;
    [SerializeField]
    float rotSpeed;

    [SerializeField]
    float minAngle, maxAngle;
    float angle = 0;

    [SerializeField]
    Vector3 pointOfRot;
    [SerializeField]
    Vector3 axisOfRot;

    Vector3 worldPoint;
    Vector3 worldAxis;

    private void LateUpdate()
    {
        worldPoint = transform.localToWorldMatrix.MultiplyPoint3x4(pointOfRot);
        worldAxis = transform.rotation * axisOfRot;
    }

    protected override bool opening(float dt)
    {
        bool isFinished = false;
        float prevAngle = angle;

        angle += dt * rotSpeed;
        if (angle >= maxAngle)
        {
            angle = maxAngle;
            isFinished = true;
        }

        float deltaAngle = angle - prevAngle;
        doorBody.transform.RotateAround(worldPoint, worldAxis, deltaAngle);

        return isFinished;
    }

    protected override bool closing(float dt)
    {
        bool isFinished = false;
        float prevAngle = angle;

        angle -= dt * rotSpeed;
        if (angle <= minAngle)
        {
            angle = minAngle;
            isFinished = true;
        }

        float deltaAngle = angle - prevAngle;
        doorBody.transform.RotateAround(worldPoint, worldAxis, deltaAngle);

        return isFinished;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(worldPoint - worldAxis * 1, worldPoint + worldAxis * 1);
        worldPoint = transform.localToWorldMatrix.MultiplyPoint3x4(pointOfRot);
        worldAxis = transform.rotation * axisOfRot;
    }
}
