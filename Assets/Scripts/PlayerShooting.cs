using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private ParticleSystem _gunParticles = null;
    [SerializeField] private LineRenderer _gunLine = null;
    [SerializeField] private Light _gunLight = null;
    [SerializeField] private AudioSource _gunAudio = null;

    private Ray _shootRay;
    private RaycastHit _shootHit;

    private int _shootableMask = 0;
    private int _damagePerShot = 20;

    private float _timeBetWeenBullets = 0.15f;
    private float _timer = 0.0f;
    private float _effectsDisplayTime = 0.20f;
    private float _range = 100.0f;


    private void Awake()
    {
        _shootableMask = LayerMask.GetMask("Shootable");
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        MapController();
        DisableEffects();
    }

    private void MapController()
    {
        if (Input.GetButton("Fire1") && _timer >= _timeBetWeenBullets && Time.timeScale != 0.0f)
            Shoot();
    }

    private void Shoot()
    {

            _timer = 0.0f;
            _gunAudio.Play();
            _gunLight.enabled = true;
            _gunLine.enabled = true;
            _gunLine.SetPosition(0, this.transform.position);

        
    }

    private void DisableEffects()
    {
        if (_timer >= _timeBetWeenBullets * _effectsDisplayTime)
        {
            _gunLine.enabled = false;
            _gunLight.enabled = false;
        }

        _shootRay.origin = this.transform.position;
        _shootRay.direction = this.transform.forward;

        if (Physics.Raycast(_shootRay, out _shootHit, _range, _shootableMask))
        {
            /*EnemyHealth enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
                enemyHealth.TakeDamage(_damagePerShot, _shootHit.point);*/
            
            _gunLine.SetPosition(1, _shootHit.point);
        }
        /*else
            _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * _range);*/
    }
}
