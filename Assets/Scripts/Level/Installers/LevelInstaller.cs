using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private List<Transform> _defaultTurretPlaces;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LevelController>().AsSingle().WithArguments(_defaultTurretPlaces);
        }
    }
}