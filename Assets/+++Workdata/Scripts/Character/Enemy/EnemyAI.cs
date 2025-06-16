using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    EnemyStats enemyStats;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyStats = GetComponent<EnemyStats>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(UpdateDestination());
    }

    IEnumerator UpdateDestination()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            agent.destination = player.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit()
    {
        if (CompareTag("Player"))
        {
            StopCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        enemyStats.audioSource.Play();
        player.GetComponent<PlayerTestScript>().TakeDamage(enemyStats.attackDamage);
        yield return new WaitForSeconds(enemyStats.attackSpeed);
    }
}
