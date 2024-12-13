using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace MonoInstallers
{
    public class AudioInstaller : MonoInstaller
    {
        [FormerlySerializedAs("audioClip")] public AudioManager audioManager;
        public override void InstallBindings()
        {
            Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle().NonLazy();
        }
    }
}