using UnityEngine;
using Zenject;

namespace Game.CreepSpawner
{
    public class CreepSpawnInstaller : MonoInstaller
    {
        [SerializeField] private CreepSpawnView _creepSpawnView;
        [SerializeField] private CreepSpawnSettings _creepSpawnSettings;
        [SerializeField] private Camera _mainCamera;

        public override void InstallBindings()
        {
            Container.BindInstance(_creepSpawnView).AsSingle();
            Container.BindInstance(_creepSpawnSettings).AsSingle();
            Container.BindInstance(_mainCamera).AsSingle();
            Container.BindInterfacesTo<CreepSpawnController>().AsSingle();
            Container.BindInterfacesTo<AutoSpawnController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ManualSpawnController>().AsSingle();
            Container.BindInterfacesTo<CreepFactory>().AsSingle();
            Container.BindInterfacesTo<CreepStorage>().AsSingle();
        }
    }
}