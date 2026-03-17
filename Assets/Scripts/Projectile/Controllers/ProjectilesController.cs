using System.Collections.Generic;
using Game.CreepSpawner;
using Game.Replay;
using Zenject;

namespace Game.Projectiles
{
    public class ProjectilesController : IProjectilesController, ITickable
    {
        private readonly IFactory<Projectile> _factory;
        private readonly IReplayer _replayer;
        private readonly Queue<Projectile> _projectilesPool = new Queue<Projectile>();
        private readonly List<Projectile> _allProjectiles = new List<Projectile>();

        public ProjectilesController(IFactory<Projectile> factory, IReplayer replayer)
        {
            _factory = factory;
            _replayer = replayer;
            _projectilesPool = new Queue<Projectile>();
            _allProjectiles = new List<Projectile>();
        }

        public void RequestLaunchProjectile(ProjectileData data)
        {
            if (_projectilesPool.Count == 0)
            {
                _factory.Create();
                Projectile newProjectile = _factory.Create();
                _projectilesPool.Enqueue(newProjectile);
                _allProjectiles.Add(newProjectile);
                _replayer.AddObject(newProjectile);
            }

            Projectile projectile = _projectilesPool.Dequeue();
            InitProjectile(data, projectile);
        }

        public void Tick()
        {
            foreach (Projectile projectile in _allProjectiles)
                if (projectile.IsActive)
                    projectile.Move();
        }

        private void InitProjectile(ProjectileData data, Projectile projectile)
        {
            projectile.Init(data.target);
            projectile.Activate(data.position, data.rotation);
            projectile.OnTargetHit += TargetHit;
        }

        private void TargetHit(Projectile projectile, Creep target)
        {
            projectile.OnTargetHit -= TargetHit;
            projectile.Deactivate();
            _projectilesPool.Enqueue(projectile);
        }
    }
}