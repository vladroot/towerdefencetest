using UnityEngine;
using Zenject;

namespace Common.Input
{
    [CreateAssetMenu(menuName = "Installers/Common/InputInstaller", fileName = "InputInstaller")]
    public class InputInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.BindInterfacesTo<PlayerInputController>().AsSingle();
        }
    }
}