using System.Collections.Generic;
using UnityEngine;

public class StuckDetector : MonoBehaviour
{
    public const float eps = 0.01f;
    public const float aroundFactor = 1.1f;

    public bool StuckCalculate()
    {
        List<Vector3> points = new List<Vector3>(localCollisionPoints);

        for (int i = 0; i < points.Count; ++i)
        {
            for (int j = i + 1; j < points.Count; ++j)
            {
                if (Vector3.Dot(points[i], points[j]) < eps)
                {
                    return true;
                }
            }
        }

        return false; 
    }

    Dictionary<GameObject, List<Vector3>> collisionContacts = new();
    private IEnumerable<Vector3> localCollisionPoints
    {
        get
        {
            if (collisionContacts != null)
                foreach (var contact in collisionContacts)
                {
                    foreach (var point in contact.Value)
                        yield return point - transform.position;
                }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        collisionContacts[collision.gameObject] = new List<ContactPoint>(collision.contacts).ConvertAll(cp => cp.point);
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionContacts.Remove(collision.gameObject);
    }

    private void OnDrawGizmos()
    {
        if (localCollisionPoints != null)
        {
            Gizmos.color = Color.blue;
            foreach (var point in localCollisionPoints)
            {
                Gizmos.DrawLine(transform.position, transform.position + point);
            }
        }
    }
}