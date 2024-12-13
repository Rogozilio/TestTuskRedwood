using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private UI ui;
    public override void InstallBindings()
    {
        Container.Bind<UI>().ToSelf().FromComponentsInHierarchy().AsSingle().NonLazy();
    }
}