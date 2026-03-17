using UnityEngine;
using Zenject;

namespace Game.Replay
{
    public class ReplayInstaller : MonoInstaller
    {
        [SerializeField] private ReplayUIView _view;

        public override void InstallBindings()
        {
            Container.BindInstance(_view).AsSingle();
            Container.BindInterfacesTo<ReplayController>().AsSingle();
        }
    }
}