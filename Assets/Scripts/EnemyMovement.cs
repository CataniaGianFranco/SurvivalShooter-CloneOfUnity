using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent = null;
    
    private Transform _player = null;

    private void Awake()
    {
        _player  = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        SetDestinationToPlayer();
    }

    private void SetDestinationToPlayer()
    {
        _navMeshAgent.SetDestination(_player.position);
    }
}