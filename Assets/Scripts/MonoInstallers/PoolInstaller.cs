using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private PoolObject poolBullet;
    [SerializeField] private PoolObject poolEnemy;
    [SerializeField] private PoolObject poolAmmo;
    public override void InstallBindings()
    {
        Container.Bind<PoolObject>().WithId("bullet").FromInstance(poolBullet).NonLazy();
        Container.Bind<PoolObject>().WithId("enemy").FromInstance(poolEnemy).NonLazy();
        Container.Bind<PoolObject>().WithId("ammo").FromInstance(poolAmmo).NonLazy();
    }
}