using System.Collections.Generic;
using Game.Turret;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class LevelController : IInitializable, ITickable
    {
        private readonly List<Transform> _defaultTurrets;
        private readonly ITurretFactory _turretFactory;

        public LevelController(List<Transform> defaultTurrets, ITurretFactory turretFactory)
        {
            _defaultTurrets = defaultTurrets;
            _turretFactory = turretFactory;
        }

        public void Initialize()
        {
            foreach (Transform defaultPlace in _defaultTurrets)
                _turretFactory.Create(defaultPlace.position, defaultPlace.rotation);
        }

        public void Tick()
        {
        }
    }
}