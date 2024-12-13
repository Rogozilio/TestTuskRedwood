using Cysharp.Threading.Tasks;
using Enums;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public InputPlayer input;
    public Animator animator;
    public Transform pointStartFire;
    public float speed = 1f;
    public int rateOnFire = 200;
    public int ammo = 30;

    [Inject] private UI _ui;
    [Inject] private AudioManager _audioManager;
    [Inject(Id = "bullet")] private PoolObject _poolBullet;

    private bool _isEndDelayRateOnFire = true;
    private bool _isMovePlayer;
    private float _hozizontalSpeed => input.horizontalValue * speed;

    private void Awake()
    {
        ChangeAmmo(ammo);
    }

    private void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        transform.position += transform.right * _hozizontalSpeed;
        animator.SetBool("IsMove", _hozizontalSpeed != 0f);
        transform.localScale =
            new Vector3(_hozizontalSpeed != 0 ? Mathf.Sign(_hozizontalSpeed) : transform.localScale.x, 1, 1);
    }

    private async void Fire()
    {
        if (!input.isFire) return;

        await DelayRateOfFire();
    }

    private async UniTask DelayRateOfFire()
    {
        if (_isEndDelayRateOnFire)
        {
            _isEndDelayRateOnFire = false;
            EnableBullet();
            ChangeAmmo(--ammo);
            await UniTask.Delay(rateOnFire);
            _isEndDelayRateOnFire = true;
        }
    }

    private void EnableBullet()
    {
        _audioManager.PlayClip(AudioClipType.Shot);
        _poolBullet.OnBeforeEnable.AddListener((t) =>
        {
            t.forward = pointStartFire.forward;
            t.position = pointStartFire.position;
        });
        _poolBullet.GetObject();
    }

    private void ChangeAmmo(int value)
    {
        ammo = value;
        _ui.ammo.text = value.ToString();
        if (ammo <= 0)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            GameOver();
        if (other.CompareTag("Ammo"))
        {
            other.gameObject.SetActive(false);
            _audioManager.PlayClip(AudioClipType.TakeAmmo);
            ChangeAmmo(ammo + Random.Range(5, 11));
        }
    }

    private void GameOver()
    {
        _audioManager.PlayClip(AudioClipType.DeathPlayer);
        _ui.EnableMenu();
        gameObject.SetActive(false);
    }

    public void AudioStep()
    {
        _audioManager.PlayClip(AudioClipType.Step);
    }
}