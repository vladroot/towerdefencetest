using System;
using UnityEngine;

namespace Game.Turret
{
    [Serializable]
    public class TurretView : MonoBehaviour
    {
        public event Action<GameObject> OnEnter;
        public event Action<GameObject> OnExit;

        public Transform Turret => _turret;
        public Transform Barrel => _barrel;
        public Transform SpawnLocation => _spawnLocation;

        [SerializeField] private Transform _turret;
        [SerializeField] private Transform _barrel;
        [SerializeField] private Transform _spawnLocation;

        private void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke(other.gameObject);
        }
    }
}