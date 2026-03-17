using System;
using Game.Exit;
using Game.Replay;
using UnityEngine;
using Zenject;

namespace Game.CreepSpawner
{
    public class AutoSpawnController : ITickable, IDisposable
    {
        private readonly CreepSpawnView _view;
        private readonly ICreepSpawner _creepSpawner;
        private readonly IExit _exit;
        private readonly IReplayer _replayer;
        private readonly CreepSpawnSettings _settings;

        private float _timer;
        private EReplayStatus _replayStatus;

        public AutoSpawnController(
            CreepSpawnView view, ICreepSpawner creepSpawer, IExit exit, IReplayer replayer, CreepSpawnSettings settings)
        {
            _view = view;
            _creepSpawner = creepSpawer;
            _exit = exit;
            _replayer = replayer;
            _replayer.OnStatusChange += SetReplayStatus;
            _settings = settings;
            _timer = 0f;
            _replayStatus = EReplayStatus.Record;
        }

        public void Dispose()
        {
            _replayer.OnStatusChange -= SetReplayStatus;
        }

        public void Tick()
        {
            if (!_settings.AutomaticSpawn || _replayStatus != EReplayStatus.Record)
                return;

            _timer -= Time.deltaTime;

            if (_timer > 0f)
                return;

            _timer = _settings.SpawnDelay;
            Creep creep = _creepSpawner.Spawn(_view.DefaultSpawnPoint.position, _view.DefaultSpawnPoint.rotation);
            creep.DirectTo(_exit.ExitPosition);
        }

        private void SetReplayStatus(EReplayStatus status)
        {
            _replayStatus = status;
        }
    }
}