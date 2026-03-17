using Game.CreepSpawner;
using Game.Projectiles;
using Game.Replay;
using UnityEngine;

namespace Game.Turret
{
    public class TurretFactory : ITurretFactory
    {
        private readonly IProjectilesController _projectilesController;
        private readonly ICreepStorage _creepStorage;
        private readonly IReplayer _replayer;
        private readonly TurretSettings _settings;

        public TurretFactory(
            IProjectilesController projectilesController,
            ICreepStorage creepStorage,
            IReplayer replayer,
            TurretSettings settings)
        {
            _projectilesController = projectilesController;
            _creepStorage = creepStorage;
            _replayer = replayer;
            _settings = settings;
        }

        public ITurret Create(Vector3 position, Quaternion rotation)
        {
            var view = GameObject.Instantiate(_settings.TurretPrefab, position, rotation);
            Turret turret = new(view, _projectilesController, _creepStorage, _replayer, _settings);
            turret.Initialize();
            return turret;
        }
    }
}