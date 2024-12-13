using Enums;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public EnemyDataSO enemyDataSo;
    public UIEnemy uiEnemy;

    [Inject(Id = "ammo")] private PoolObject _poolAmmo;
    [Inject] private AudioManager _audioManager;
    
    private Animator _animator;
    private Rigidbody _rigidbody;

    private int _health;
    private int _maxHealth;
    private float _speed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        var index = Random.Range(0, 5);
        var enemyData = enemyDataSo.enemyDatas[index];

        _animator.Play(enemyData.nameClip);
        _maxHealth = _health = enemyData.health;
        _speed = enemyData.speed;

        _rigidbody.velocity = transform.right * _speed;
        
        _audioManager.PlayClip(AudioClipType.Zombie, 0.1f);
    }

    public void TakeDamage()
    {
        if (--_health <= 0)
        {
            Die();
        }

        uiEnemy.OnChangeHealthBar?.Invoke((float)_health / _maxHealth);
    }

    private void Die()
    {
        _audioManager.PlayClip(AudioClipType.DeathZombie);
        _poolAmmo.GetObject().transform.position = transform.position;
        gameObject.SetActive(false);
    }
}