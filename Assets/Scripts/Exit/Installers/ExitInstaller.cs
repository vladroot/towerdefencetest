using UnityEngine;
using Zenject;

namespace Game.Exit
{
    public class ExitInstaller : MonoInstaller
    {
        [SerializeField] private ExitView _exitView;

        public override void InstallBindings()
        {
            Container.BindInstance(_exitView).AsSingle();
            Container.BindInterfacesTo<ExitController>().AsSingle();
        }
    }
}