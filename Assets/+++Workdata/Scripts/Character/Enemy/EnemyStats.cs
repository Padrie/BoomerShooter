using MyBox;
using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Health")]
    [Min(0)]public float currentHealth;
    public float maxHealth;
    [Header("Attack")]
    public int attackDamage;
    public float attackSpeed;
    [SerializeField] AudioClip attackSound;
    public AudioSource audioSource;

    public static event Action OnEnemyDied;

    private void Awake()
    {
        currentHealth = maxHealth;
        audioSource.resource = attackSound;
    }

    public bool TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth == 0)
        {
            Die();
            return false;
        }

        return true;
    }

    private void Die()
    {
        OnEnemyDied?.Invoke();

        Destroy(gameObject);
    }
}
