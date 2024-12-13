using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    [Inject(Id = "enemy")] private PoolObject _poolObject;
    public List<Transform> spawnPoints;

    private async void Awake()
    {
        _poolObject.OnBeforeEnable.AddListener((t) =>
        {
            var index = Random.Range(0, 2);
            t.position = spawnPoints[index].position;
            t.right = spawnPoints[index].right;
        });
        
        await LoopSpawnEnemy();
    }

    private async UniTask LoopSpawnEnemy()
    {
        while (true)
        {
            await UniTask.Delay(Random.Range(1, 11) * 1000);
            RespawnEnemy();
        }
    }

    private void RespawnEnemy()
    {
        _poolObject.GetObject();
    }
}