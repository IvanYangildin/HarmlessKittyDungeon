using UnityEngine;


public abstract class AbstractSword : Weapon
{
    Vector3 origin_position;
    Quaternion origin_rotation;


    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource audioSource;

    public override bool IsAttacking => animator.GetBool("IsAttacking");
    bool adjust = false;

    public override Vector3 AttackSourcePosition => gameObject.transform.parent.position;

    private void Awake()
    {
        origin_position = transform.localPosition;
        origin_rotation = transform.localRotation;
    }

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
        Hit?.Invoke(this);
    }

    public void OnZenithHit()
    {
        ZenithHit?.Invoke(this);
    }

    public void OnOutHit()
    {
        adjust = true;
        OutHit?.Invoke(this);
    }

    private void LateUpdate()
    {
        if (adjust)
        {
            transform.localPosition = origin_position;
            transform.localRotation = origin_rotation;
            adjust = false;
        }
    }

}