using System;
using Common.Input;
using Game.Exit;
using UnityEngine;

namespace Game.CreepSpawner
{
    public class ManualSpawnController : IDisposable
    {
        private readonly IPlayerInput _playerInput;
        private readonly Camera _mainCamera;
        private readonly ICreepSpawner _creepSpawner;
        private readonly IExit _exit;
        private RaycastHit[] _hits;

        public ManualSpawnController(
            IPlayerInput playerInput, Camera mainCamera, ICreepSpawner creepSpawer, IExit exit)
        {
            _playerInput = playerInput;
            _playerInput.OnFirstTouch += SpawnCreep;
            _playerInput.OnMouseDown += SpawnCreep;
            _mainCamera = mainCamera;
            _creepSpawner = creepSpawer;
            _exit = exit;
            _hits = new RaycastHit[1];
        }

        public void Dispose()
        {
            _playerInput.OnFirstTouch -= SpawnCreep;
            _playerInput.OnMouseDown -= SpawnCreep;
        }

        private void SpawnCreep(Vector2 position)
        {
            Debug.Log(position);
            Ray ray = _mainCamera.ScreenPointToRay(position);

            if (Physics.RaycastNonAlloc(ray, _hits, _mainCamera.farClipPlane) == 0)
                return;

            Creep creep = _creepSpawner.Spawn(_hits[0].point, Quaternion.identity);
            creep.DirectTo(_exit.ExitPosition);
        }
    }
}