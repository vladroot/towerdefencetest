using UnityEngine;
using Zenject;

namespace Game.CreepSpawner
{
    public class CreepFactory : IFactory<Creep>
    {
        private readonly CreepSpawnSettings _settings;

        public CreepFactory(CreepSpawnSettings settings)
        {
            _settings = settings;
        }

        public Creep Create()
        {
            var creepView = GameObject.Instantiate(_settings.RunnerPrefab);
            return new Creep(creepView, _settings.RunnerHealth);
        }
    }
}