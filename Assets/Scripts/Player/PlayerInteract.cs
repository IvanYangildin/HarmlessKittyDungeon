using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    Transform interactEye;
    
    [SerializeField]
    float distance;
    [SerializeField]
    LayerMask interactMask;

    RaycastHit InteractInfo;
    Interactable interaction;

    public void SetInteraction()
    {
        Ray ray = new Ray(interactEye.position, interactEye.forward);

        Interactable nextInteraction = null;
        if (Physics.Raycast(ray, out InteractInfo, distance, interactMask))
        {
            nextInteraction = InteractInfo.collider.gameObject.GetComponent<Interactable>();
        }

        if (interaction != nextInteraction)
        {
            interaction?.OnLookAway(this);
            nextInteraction?.OnLook(this);
            interaction = nextInteraction;
        }

    }

    private void FixedUpdate()
    {
        SetInteraction();
    }

    public void Interact()
    {
        interaction?.OnInteract(this);
    }

    private void OnDrawGizmos()
    {
        Ray ray = new Ray(interactEye.position, interactEye.forward);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(ray.origin, interactEye.position + ray.direction * distance);
    }
}