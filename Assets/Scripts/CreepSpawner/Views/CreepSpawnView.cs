using UnityEngine;

namespace Game.CreepSpawner
{
    public class CreepSpawnView : MonoBehaviour
    {
        public Transform DefaultSpawnPoint => defaultSpawnPoint;

        [SerializeField] private Transform defaultSpawnPoint;
    }
}