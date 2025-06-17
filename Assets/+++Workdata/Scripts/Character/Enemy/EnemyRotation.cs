using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    GameObject player;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        HandleRotation();
    }

    public void HandleRotation()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 / Time.deltaTime);
    }
}
