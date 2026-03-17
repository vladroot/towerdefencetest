using UnityEngine;
using Zenject;

namespace Game.Turret
{
    [CreateAssetMenu(menuName = "Installers/Game/TurretInstaller", fileName = "TurretInstaller")]
    public class TurretInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private TurretSettings _turretSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_turretSettings).AsSingle();
            Container.BindInterfacesTo<TurretsController>().AsSingle();
            Container.BindInterfacesTo<TurretFactory>().AsSingle();
        }
    }
}