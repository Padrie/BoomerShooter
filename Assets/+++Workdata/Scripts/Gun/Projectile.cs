using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileStats projectileStats;

    [HideInInspector] public int damage;
    int penetration;

    private void Awake()
    {
        damage = projectileStats.damage;
        penetration = projectileStats.penetration;
    }

    void Start()
    {
        Invoke("Disappear", 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (penetration == 0)
            {
                Disappear();
            }
            other.GetComponent<EnemyStats>().TakeDamage(damage);

            penetration--;
        }
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }
}
