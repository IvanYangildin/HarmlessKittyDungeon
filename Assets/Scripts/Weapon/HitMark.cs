using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum HitReactionType { InMoment, InZenith};

public enum OriginType { Self, Head};

public class HitMark : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    OriginType origin = OriginType.Head;

    LayerMask hitMask;

    private void Awake()
    {
        hitMask = (~0) ^ 
            LayerMask.GetMask("CameraFront") ^ 
            LayerMask.GetMask("Player");
    }

    protected virtual Vector3 HitOrigin(Weapon weapon)
    {
        switch (origin)
        {
            case OriginType.Head:
                return weapon.Head;
            default:
                return transform.position;
        }
    }

    public void Hitting(Weapon weapon)
    {
        Vector3 origin = HitOrigin(weapon);

        Vector3 dir = origin - weapon.AttackSourcePosition;
        float distMax = dir.magnitude * 2;

        RaycastHit hitInfo;
        if (Physics.SphereCast(weapon.AttackSourcePosition, 0.3f, dir, out hitInfo, distMax, hitMask))
        {
            if ((hitInfo.collider.gameObject == gameObject) ||
                (hitInfo.collider.transform.parent?.gameObject == gameObject))
            MakeMark(hitInfo);
        }

    }

    private void MakeMark(RaycastHit hitInfo)
    {
        ParticleSystem hit = Instantiate(particles, hitInfo.point, Quaternion.identity);
        hit.transform.forward = hitInfo.normal;
        hit.Play();
    }
}