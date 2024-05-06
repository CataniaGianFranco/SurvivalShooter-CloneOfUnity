using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Rigidbody")]
    [SerializeField] private Rigidbody _rigidbody = null;

    [Header("Nave Mesh Agent")]
    [SerializeField] private NavMeshAgent _navMeshAgent = null;

    [Header("Animation")]
    [SerializeField] private Animator _enemyAnimator = null;

    [Header("Audio")]
    [SerializeField] private AudioSource _enemyAudioSource = null;
    [SerializeField] private AudioClip _deathAudioClip = null;

    [Header("Particle")]
    [SerializeField] private ParticleSystem _hitParticle = null;

    [Header("Collider")]
    [SerializeField] private CapsuleCollider _capsuleCollider = null;

    private int _startingHealth = 100;
    private int _currentHealth = 0;
    private int _score = 10;

    private float _sinkSpeed = 2.50f;

    private bool _isDead = false;
    private bool _isSinking = false;

    public int CurrentHealth { get { return _currentHealth; } }

    private void Awake()
    {
        _currentHealth = _startingHealth;
    }

    private void Update()
    {
        SinkSelf();
    }

    private void Death()
    {
        _isDead = true;

        _capsuleCollider.enabled = false;

        _enemyAudioSource.clip = _deathAudioClip;
        
        _enemyAnimator.SetTrigger("Dead");
        _enemyAudioSource.Play();
    }

    private void SinkSelf()
    {
        switch (_isSinking)
        {
            case true: transform.Translate(Vector3.down * _sinkSpeed * Time.deltaTime); break;
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (_isDead) return;

        _currentHealth -= amount;

        _enemyAudioSource.Play();

        _hitParticle.transform.position = hitPoint;
        _hitParticle.Play();

        if (_currentHealth <= 0.0f)
            Death();
    }

    public void StartSinking()
    {
        _navMeshAgent.enabled = false;
        _rigidbody.isKinematic = false;

        _isSinking = true;

        Destroy(this.gameObject, 2.0f);
    }
}
