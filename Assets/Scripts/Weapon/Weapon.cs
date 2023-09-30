using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    private Sprite weaponIcon;
    [SerializeField]
    private Vector3 localHead;

    // invokes in the "main" moment of hit
    public Action<Weapon> Hit, ZenithHit, OutHit;

    public Vector3 Head => transform.localToWorldMatrix.MultiplyPoint3x4(localHead);

    public Sprite WeaponIcon => weaponIcon;

    public abstract bool IsAttacking { get; }

    public abstract void OnAttack();

    public abstract void OnStopAttack();

    public abstract void TargetAffect(GameObject target);

    public abstract Vector3 AttackSourcePosition { get; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.localToWorldMatrix.MultiplyPoint3x4(localHead), 0.1f);
    }

}