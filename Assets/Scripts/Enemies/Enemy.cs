using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float invulnerableCooldown,
                stunnedCooldown,
                attackCooldown;

    [SerializeField]
    private float repulsionStrength, 
                repulsionAngle;

    [SerializeField]
    private UnitMove enemyMove;
    [SerializeField]
    private AudioSource enemyAudio;
    
    private Rigidbody rb;

    public bool stunned = false;
    public bool IsInvulnerable { private set; get; }
    public bool CanAttack { private set; get; }

    [SerializeField]
    private float rotSpeed;
    // where object will look
    Vector3 finalDirection = Vector3.forward;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        IsInvulnerable = false;
        CanAttack = true;
        finalDirection = transform.forward;
    }


    Coroutine invulnerableCor = null, stunnedCor = null;
    private void HitReaction(Weapon weapon)
    {
        IsInvulnerable = true;
        stunned = true;

        Repulsion(weapon.AttackSourcePosition);
        enemyAudio.Play();

        if (invulnerableCor != null) StopCoroutine(invulnerableCor);
        if (stunnedCor != null) StopCoroutine(stunnedCor);

        invulnerableCor = StartCoroutine(Timer.Countdown(() => IsInvulnerable = false, invulnerableCooldown));
        stunnedCor = StartCoroutine(Timer.Countdown(() => stunned = false, stunnedCooldown));
    }

    private void Attacking(PlayerProfile player)
    {
        CanAttack = false;
        player.TakeDamage(damage);
        StartCoroutine(Timer.Countdown(() =>
        {
            CanAttack = true;
        }, attackCooldown));
    }

    private void Repulsion(Vector3 repulsionSource)
    {
        Vector3 localSource = repulsionSource - transform.position;
        Vector3 force = new Vector3(-localSource.x, 0, -localSource.z).normalized;

        Vector3 help = Vector3.Cross(force, Vector3.up);
        Quaternion rot = Quaternion.AngleAxis(repulsionAngle, help);
        force = rot * force;

        rb.velocity = Vector3.zero;
        rb.AddForce(force * repulsionStrength, ForceMode.Impulse);

    }

    private void OnTriggerStay(Collider other)
    {
        Weapon weapon = other.gameObject.GetComponentInParent<Weapon>();
        if (!IsInvulnerable && (weapon != null) && (weapon.IsAttacking))
        {
            weapon.TargetAffect(gameObject);
            HitReaction(weapon);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        PlayerProfile player = collision.gameObject.GetComponentInParent<PlayerProfile>();
        if (CanAttack && !stunned && (player != null))
        {
            Attacking(player);
        }
    }

    public void moveToPoint(Vector3 pos)
    {
        if (!stunned)
        {
            finalDirection = pos - transform.position;
            finalDirection.y = 0;
            if (finalDirection.sqrMagnitude > 0.1f)
            {
                enemyMove.MoveWorld(finalDirection);
                finalDirection = finalDirection.normalized;
            }
            else
            {
                finalDirection = transform.forward;
            }
        }
    }

    private void FixedUpdate()
    {
        float s = Mathf.Sign(Vector3.Cross(transform.forward, finalDirection).y);
        float angle = Vector3.Angle(transform.forward, finalDirection);
        float deltaAngle = Mathf.Clamp(s * Time.deltaTime * rotSpeed, -angle, angle);

        transform.Rotate(Vector3.up, deltaAngle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + finalDirection);
    }

}