using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.CreepSpawner
{
    public class CreepSpawnController : ICreepSpawner
    {
        private readonly Queue<Creep> _unitPool;
        private readonly ICreepStorage _creepStorage;
        private readonly IFactory<Creep> _factory;

        public CreepSpawnController(ICreepStorage creepStorage, IFactory<Creep> factory)
        {
            _creepStorage = creepStorage;
            _factory = factory;
            _unitPool = new Queue<Creep>();
        }

        public Creep Spawn(Vector3 position, Quaternion rotation)
        {
            Creep creep = _unitPool.Count == 0 ? _factory.Create() : _unitPool.Dequeue();

            Insert(creep, position, rotation);
            return creep;
        }

        public void Despawn(Creep creep)
        {
            creep.Deactivate();
            _creepStorage.Remove(creep);
            _unitPool.Enqueue(creep);
        }

        private void Insert(Creep creep, Vector3 position, Quaternion rotation)
        {
            creep.Activate(position, rotation);
            creep.OnDeath += RunnerDeath;
            _creepStorage.Add(creep);
        }

        private void RunnerDeath(Creep creep)
        {
            creep.OnDeath -= RunnerDeath;
            Despawn(creep);
        }
    }
}