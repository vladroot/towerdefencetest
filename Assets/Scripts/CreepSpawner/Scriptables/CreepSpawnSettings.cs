using UnityEngine;

namespace Game.CreepSpawner
{
    [CreateAssetMenu(menuName = "ScriptableObject/Game/CreepSpawnSettings", fileName = "CreepSpawnSettings")]
    public class CreepSpawnSettings : ScriptableObject
    {
        public bool AutomaticSpawn => automaticSpawn;
        public float SpawnDelay => spawnDelay;
        public float RunnerHealth => runnerHealth;
        public CreepView RunnerPrefab => runnerPrefab;

        [SerializeField] private bool automaticSpawn;
        [SerializeField] private float spawnDelay;
        [SerializeField] private float runnerHealth;
        [SerializeField] private CreepView runnerPrefab;
    }
}