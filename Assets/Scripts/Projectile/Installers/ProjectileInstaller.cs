using UnityEngine;
using Zenject;

namespace Game.Projectiles
{
    [CreateAssetMenu(menuName = "Installers/Game/ProjectileInstaller", fileName = "ProjectileInstaller")]
    public class ProjectileInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ProjectileSettings _projectileSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_projectileSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectilesController>().AsSingle();
            Container.BindInterfacesTo<ProjectileFactory>().AsSingle();
        }
    }
}