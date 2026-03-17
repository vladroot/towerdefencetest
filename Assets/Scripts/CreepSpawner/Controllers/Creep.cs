using System;
using Game.Replay;
using UnityEngine;
using UnityEngine.AI;

namespace Game.CreepSpawner
{
    public class Creep
    {
        public event Action<Creep> OnDeath;

        public float Health { get; private set; }
        public bool IsActive { get; private set; }
        public CreepView View => _view;

        private readonly CreepView _view;
        private readonly float _maxHealth;
        private readonly NavMeshAgent _navAgent;

        private EReplayStatus _replayStatus;

        public Creep(CreepView view, float maxHealth)
        {
            _view = view;
            _navAgent = view.NavAgent;
            _maxHealth = maxHealth;
        }

        public void Activate(Vector3 position, Quaternion rotation)
        {
            if (_replayStatus != EReplayStatus.Record)
                return;

            _view.transform.position = position;
            _view.transform.rotation = rotation;
            _view.gameObject.SetActive(true);
            IsActive = true;

            _navAgent.Warp(position);
            Health = _maxHealth;
        }

        public virtual void Deactivate()
        {
            IsActive = false;
            _view.gameObject.SetActive(false);
            _view.transform.position = Vector3.zero;
            _view.transform.rotation = Quaternion.identity;
        }


        public void DirectTo(Vector3 position)
        {
            if (_replayStatus != EReplayStatus.Record)
                return;

            _navAgent.SetDestination(position);
        }

        public void Damage(float damage)
        {
            Health -= damage;

            if (Health <= 0f)
                OnDeath?.Invoke(this);
        }

        public void SetReplayStatus(EReplayStatus status)
        {
            _replayStatus = status;
            if (_view.gameObject.activeInHierarchy)
                _navAgent.isStopped = _replayStatus != EReplayStatus.Record;
        }
    }
}