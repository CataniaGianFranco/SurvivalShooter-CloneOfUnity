using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerThirdControllerHealth : MonoBehaviour
{
    [Header ("Player")]
    [SerializeField] PlayerThirdController _playerThirdController = null;
    [SerializeField] Animator _playerAnimator = null;
    
    [Header ("UI")]
    [SerializeField] Slider _healthSlider = null;
    [SerializeField] Image _damageImage = null;
    [SerializeField] Color _flashColour = new Color(234.0f, 81f, 113f, 0.0f);

    [Header ("Audio")]
    [SerializeField] AudioSource _playerAudioSource = null;
    [SerializeField] AudioClip _deathAudioClip = null;

    private int _startingHealth = 100;
    private int _currentHealth = 0;
    
    private float _flashSpeed = 5.0f;

    private bool _isDead = false;
    private bool _isDamaged = false;

    public float CurrentHealth { get { return _currentHealth; } }

    private void Awake()
    {
        _currentHealth = _startingHealth;
    }
    
    private void Update()
    {
        FlashDamage();
    }

    private void FlashDamage()
    {
        switch (_isDamaged)
        {
            case true: _damageImage.color = _flashColour; break;
            case false: _damageImage.color = Color.Lerp(_damageImage.color, Color.clear, _flashSpeed * Time.deltaTime); break;
        }

        _isDamaged = false;
    }

    public void TakeDamage(int amount)
    {
        _isDamaged = true;
        
        _currentHealth -= amount;
        _healthSlider.value = _currentHealth;

        _playerAudioSource.Play();

        if (!_isDead && _currentHealth <= 0.0f)
            Death();
    }

    private void Death()
    {
        _isDead = true;
        _playerThirdController.enabled = false;
        
        _playerAudioSource.clip = _deathAudioClip;
        
        _playerAnimator.SetTrigger("Die");
        _playerAudioSource.Play();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("MainGame");
    }
}