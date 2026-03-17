using System;
using Game.CreepSpawner;
using UnityEngine;

namespace Game.Exit
{
    public class ExitController : IExit, IDisposable
    {
        public Vector3 ExitPosition => _exitView.transform.position;

        private readonly ExitView _exitView;
        private readonly ICreepSpawner _creepSpawner;
        private readonly ICreepStorage _storage;

        public ExitController(ExitView exitView, ICreepSpawner creepSpawer, ICreepStorage storage)
        {
            _exitView = exitView;
            _exitView.OnTrigger += OnTriggered;
            _creepSpawner = creepSpawer;
            _storage = storage;
        }

        public void Dispose()
        {
            _exitView.OnTrigger -= OnTriggered;
        }

        private void OnTriggered(GameObject gameObject)
        {
            if (_storage.TryGet(gameObject, out Creep creep))
                _creepSpawner.Despawn(creep);
        }
    }
}