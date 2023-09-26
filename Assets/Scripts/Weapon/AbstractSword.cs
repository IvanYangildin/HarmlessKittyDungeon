using UnityEngine;

public abstract class AbstractSword : Weapon
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource audioSource;

    public override bool IsAttacking => animator.GetBool("IsAttacking");

    public override Vector3 AttackSourcePosition => gameObject.transform.parent.position;

    public override void OnAttack()
    {
        animator.SetBool("ContinueAttacking", true);
    }
    public override void OnStopAttack()
    {
        animator.SetBool("ContinueAttacking", false);
    }

    public void OnHit()
    {
        audioSource.Play();
    }

}