using UnityEngine;


public class PlayerProfile : MonoBehaviour
{
    public StateSaver LastCheckpoint;

    [SerializeField]
    ShakyObject hittedHead;

    [SerializeField]
    [Min(0f)]
    private float maxHealth;

    public float MaxHealth => maxHealth;
    private float health;
    public float Health { 
        set { health = Mathf.Clamp(value, 0f, MaxHealth); } 
        get => health; 
    }

    private void Awake()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0f)
        {
            LastCheckpoint.LoadState();
        }
        Health = Mathf.Clamp(Health, 0f, MaxHealth);
        hittedHead.Shake();

    }
}