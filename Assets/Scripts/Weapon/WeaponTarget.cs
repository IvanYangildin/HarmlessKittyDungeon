using UnityEngine;
using UnityEngine.Events;

public class WeaponTarget : MonoBehaviour
{
    [SerializeField]
    [Min(0f)]
    private float attackingCooldownTime = 0f;
    Coroutine attackingCooldown = null;

    public bool InCooldown { private set; get; }

    public UnityEvent<Weapon> WeaponAttacking = new UnityEvent<Weapon>();
    public UnityEvent<Weapon> WeaponZenith = new UnityEvent<Weapon>();


    private void invokeZenith(Weapon weapon) => WeaponZenith.Invoke(weapon);

    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponentInParent<Weapon>();
        if (weapon != null)
        {
            weapon.ZenithHit += invokeZenith;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Weapon weapon = other.gameObject.GetComponentInParent<Weapon>();
        if ((weapon != null) && (weapon.IsAttacking) && !InCooldown)
        {
            WeaponAttacking.Invoke(weapon);
            if (attackingCooldown != null) StopCoroutine(attackingCooldown);

            if (attackingCooldownTime > 0.001f)
            {
                InCooldown = true;
                attackingCooldown = StartCoroutine(Timer.Countdown(() => InCooldown = false, attackingCooldownTime));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Weapon weapon = other.GetComponentInParent<Weapon>();
        if (weapon != null)
        {
            weapon.ZenithHit -= invokeZenith;
        }
    }
}