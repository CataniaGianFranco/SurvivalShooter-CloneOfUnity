using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private EnemyHealth _enemyHealth;

    [Header("Player Third Controller")]
    [SerializeField] private GameObject _playerThirdController = null;
    [SerializeField] private PlayerThirdControllerHealth _playerHealth = null;

    [Header("Animation")]
    [SerializeField] private Animator _enemyAnimator = null;
    

    private bool _playerInRange = false;

    private int _attackDamage = 10;
    
    private float _timeBetweenAttacks = 0.5f;
    private float _timer = 0.0f;

    private void Update()
    {
        HandlePlayerInteraction();
    }

    private void HandlePlayerInteraction()
    {
        _timer += Time.deltaTime;

        if (_playerInRange && _enemyHealth.CurrentHealth > 0f && _timer >= _timeBetweenAttacks)
            Attack();
        
        if (_playerHealth.CurrentHealth <= 0.0f)
            _enemyAnimator.SetTrigger("PlayerDead");
    }

    private void Attack()
    {
        _timer = 0.0f;

        if (_playerHealth.CurrentHealth > 0.0f)
            _playerHealth.TakeDamage(_attackDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _playerInRange = false;
    }
}