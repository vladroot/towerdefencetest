using UnityEngine;
using Zenject;

namespace Game.Projectiles
{
    public class ProjectileFactory : IFactory<Projectile>
    {
        private readonly ProjectileSettings _settings;

        public ProjectileFactory(ProjectileSettings settings)
        {
            _settings = settings;
        }

        public Projectile Create()
        {
            ProjectileView view = GameObject.Instantiate<ProjectileView>(_settings.ProjectilePrefab);
            return new Projectile(view, _settings.ProjectileSpeed, _settings.Damage);
        }
    }
}