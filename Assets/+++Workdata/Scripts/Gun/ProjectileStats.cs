using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile")]
public class ProjectileStats : ScriptableObject
{
    public enum ProjectileType
    {
        Fire,
        Gasoline
    }

    public ProjectileType projectileType;

    public int damage;
    public int penetration;
    public float projectileForce = 1000f;
}
