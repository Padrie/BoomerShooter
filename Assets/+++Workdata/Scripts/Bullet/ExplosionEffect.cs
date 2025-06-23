using UnityEngine;

[CreateAssetMenu(menuName = "BulletEffects/Explosion")]
public class ExplosionEffect : ScriptableObject, IBulletEffect
{
    public float damage = 20f;
    public float radius = 5f;
    public ParticleSystem particleSystem;

    public void ApplyEffect(Bullet bullet, Transform transform)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in hits)
        {
            if (collider.TryGetComponent<EnemyStats>(out var enemy))
            {
                enemy.TakeDamage(damage);

                Instantiate(particleSystem, transform.position, Quaternion.identity);
                particleSystem.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
            }
        }
    }
}
