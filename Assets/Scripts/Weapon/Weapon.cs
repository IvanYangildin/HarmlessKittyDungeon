using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    private Sprite weaponIcon;
    public Sprite WeaponIcon => weaponIcon;

    public abstract bool IsAttacking { get; }

    public abstract void OnAttack();

    public abstract void OnStopAttack();

    public abstract void TargetAffect(GameObject target);

    public abstract Vector3 AttackSourcePosition { get; }
}