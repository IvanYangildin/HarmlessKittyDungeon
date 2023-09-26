using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform cameraHold;

    [SerializeField]
    float minAngle, maxAngle;
    float xRotation, yRotation;

    private void Awake()
    {
        xRotation = cameraHold.rotation.eulerAngles.x;
        yRotation = cameraHold.rotation.eulerAngles.y;
    }

    private void LateUpdate()
    {
        cameraHold.position = player.position;
    }

    public void Look(Vector2 input, float sensitivity)
    {
        xRotation -= (input.y * Time.deltaTime) * sensitivity;
        yRotation += (input.x * Time.deltaTime) * sensitivity;
        xRotation = Mathf.Clamp(xRotation, minAngle, maxAngle);

        cameraHold.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
