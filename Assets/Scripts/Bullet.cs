using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    
    private Rigidbody _rigidbody;

    private CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private async void OnEnable()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        
        _rigidbody.velocity = transform.forward * speed;

        DelayDeath(_cancellationTokenSource.Token).Forget();
    }

    private async UniTaskVoid DelayDeath(CancellationToken cancellationToken)
    {
        await UniTask.Delay(2000, DelayType.DeltaTime, PlayerLoopTiming.Update, cancellationToken);

        gameObject.SetActive(false);
    }
    
    public void CancelTask()
    {
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage();
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        CancelTask();
    }
}