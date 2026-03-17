using System;
using Game.CreepSpawner;
using Game.Projectiles;
using Game.Replay;
using UnityEngine;

public class Projectile : IPoolable, IReplayable
{
    public event Action<Projectile, Creep> OnTargetHit;

    private const float minDistance = .25f;

    public bool IsActive { get; private set; }

    private readonly ProjectileView _view;

    private Creep _target;
    private float _speed;
    private float _damage;
    private EReplayStatus _replayStatus;

    public Projectile(ProjectileView view, float speed, float damage)
    {
        _view = view;
        _speed = speed;
        _damage = damage;
    }

    public void Init(Creep target)
    {
        _target = target;
    }

    public void Activate(Vector3 position, Quaternion rotation)
    {
        if (!_view)
            return;

        _view.transform.position = position;
        _view.transform.rotation = rotation;
        _view.gameObject.SetActive(true);
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
        _view.gameObject.SetActive(false);
        _view.transform.position = Vector3.zero;
        _view.transform.rotation = Quaternion.identity;
    }

    public void Move()
    {
        if (_replayStatus != EReplayStatus.Record)
            return;

        if (_target == null || !_target.IsActive)
        {
            OnTargetHit?.Invoke(this, null);
            return;
        }
        Transform transform = _view.transform;
        Vector3 normalized = (_target.View.transform.position - transform.position).normalized;
        Vector3 normDir = Vector3.Lerp(transform.forward, normalized, Time.deltaTime * _speed * 2f);

        transform.forward = normDir;
        transform.position += normDir * _speed * Time.deltaTime;
        if ((_target.View.transform.position - transform.position).magnitude <= minDistance)
        {
            _target.Damage(_damage);
            OnTargetHit?.Invoke(this, _target);
        }
    }

    public void SetReplayStatus(EReplayStatus status)
    {
        _replayStatus = status;
    }

    public SavedObject GetSaveData()
    {
        return new SavedObject(this, _view.transform.position, _view.transform.rotation, IsActive);
    }

    public void SetSaveData(SavedObject data)
    {
        if (data == null)
        {
            IsActive = false;
            _view.gameObject.SetActive(false);
            return;
        }

        IsActive = data.IsActive;
        _view.gameObject.SetActive(data.IsActive);
        _view.transform.position = data.Position;
        _view.transform.rotation = data.Rotation;
    }
}