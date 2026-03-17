using System.Collections.Generic;
using Game.CreepSpawner;
using Game.Projectiles;
using Game.Replay;
using UnityEngine;

namespace Game.Turret
{
    public class Turret : ITurret, IReplayable
    {
        private readonly TurretView _view;
        private readonly ITurretsController _turretsController;
        private readonly IProjectilesController _projectilesController;
        private readonly ICreepStorage _creepStorage;
        private readonly IReplayer _replayer;
        private readonly TurretSettings _settings;

        private float _timer = 0f;
        private List<Creep> _unitsInRange = new List<Creep>();
        private Creep _target = null;

        private EReplayStatus _replayStatus;

        public Turret(
            TurretView view,
            ITurretsController turretsController,
            IProjectilesController projectilesController,
            ICreepStorage creepStorage,
            IReplayer replayer,
            TurretSettings settings)
        {
            _view = view;
            _view.OnEnter += TargetEnter;
            _view.OnExit += TargetExit;
            _turretsController = turretsController;
            _projectilesController = projectilesController;
            _creepStorage = creepStorage;
            _replayer = replayer;
            _settings = settings;
            _replayStatus = EReplayStatus.Record;
        }

        public void Initialize()
        {
            _replayer.AddObject(this);
            _turretsController.Register(this);
            _view.Barrel.eulerAngles = new Vector3(-_settings.BarrelAngle, 0f, 0f);
        }

        public void Dispose()
        {
            _view.OnEnter -= TargetEnter;
            _view.OnExit -= TargetExit;
        }

        public void SetReplayStatus(EReplayStatus status)
        {
            _replayStatus = status;
        }

        public SavedObject GetSaveData()
        {
            return new SavedObject(this, _view.transform.position, _view.transform.rotation, true);
        }

        public void SetSaveData(SavedObject data)
        {
            _view.transform.rotation = data.Rotation;
        }

        public void Update()
        {
            if (_replayStatus != EReplayStatus.Record)
                return;

            _timer -= Time.deltaTime;

            AdjustTurretRotation();

            if (_target != null && _target.IsActive && _timer <= 0f)
            {
                Shoot();
            }
            else if (_target == null && _unitsInRange.Count > 0)
            {
                _target = _unitsInRange[0];
                _target.OnDeath += OnRunnerDeath;
                _unitsInRange.RemoveAt(0);
            }
        }

        private void Shoot()
        {
            var projectileData = new ProjectileData
            {
                target = _target,
                position = _view.SpawnLocation.position,
                rotation = _view.SpawnLocation.rotation
            };

            _projectilesController.RequestLaunchProjectile(projectileData);
            _timer = _settings.FireDelay;
        }

        private void TargetEnter(GameObject gameObject)
        {
            if (!_creepStorage.TryGet(gameObject, out Creep creep))
                return;

            _unitsInRange.Add(creep);
        }

        private void TargetExit(GameObject gameObject)
        {
            Creep quited = null;

            if (_target == null)
                return;

            if (_target.View.gameObject == gameObject)
            {
                _target.OnDeath -= OnRunnerDeath;
                _target = null;
                return;
            }

            foreach (Creep unit in _unitsInRange)
            {
                if (unit.View.gameObject != gameObject)
                    continue;

                quited = unit;
                break;
            }

            if (quited != null)
                _unitsInRange.Remove(quited);
        }

        private void OnRunnerDeath(Creep runner)
        {
            runner.OnDeath -= OnRunnerDeath;
            if (runner == _target)
                _target = null;
        }

        private void AdjustTurretRotation()
        {
            if (_target == null)
                return;

            _view.Turret.LookAt(_target.View.transform.position);
            Vector3 euler = _view.Turret.eulerAngles;
            euler.x = 0f;
            euler.z = 0f;
            _view.Turret.eulerAngles = euler;
        }
    }
}