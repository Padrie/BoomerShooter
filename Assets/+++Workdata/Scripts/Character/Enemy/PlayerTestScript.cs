using UnityEngine;

public class PlayerTestScript : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;

        print(currentHealth);

        if (currentHealth <= 0)
        {
            return false;
        }

        return true;
    }
}
