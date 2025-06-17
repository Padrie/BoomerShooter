using UnityEngine;

[CreateAssetMenu(fileName = "Projectile")]
public class ProjectileSO : ScriptableObject
{
    public float damage;
    [Range(1, 10)]public int penetration;
}
