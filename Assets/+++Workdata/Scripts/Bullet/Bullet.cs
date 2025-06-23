using UnityEngine;

public enum BulletEffectType
{
    None,
    Normal,
    Explosion,
    Lightning,
}

public class Bullet : MonoBehaviour
{
    public BulletEffectType bulletEffectType = BulletEffectType.Normal;
    public ScriptableObject effectAsset;

    public float bulletSpeed = 1000f;
    public float bulletDamage = 50f;
    public int penetration = 1;
    public float lifeTime = 2f;

    public ParticleSystem spawnEffect;

    private IBulletEffect bulletEffect;
    private int currentPenetration;

    private void Start()
    {
        bulletEffect = effectAsset as IBulletEffect;
        currentPenetration = penetration;
        Invoke("Disappear", lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (penetration <= 0)
            {
                Disappear();
            }
            other.GetComponent<EnemyStats>().TakeDamage(bulletDamage);
            bulletEffect?.ApplyEffect(this, other.transform);
            penetration--;
        }
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }
}