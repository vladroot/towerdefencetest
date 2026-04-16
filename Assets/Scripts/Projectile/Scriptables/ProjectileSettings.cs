using UnityEngine;

namespace Game.Projectiles
{
    [CreateAssetMenu(menuName = "ScriptableObject/Game/ProjectileSettings", fileName = "ProjectileSettings")]
    public class ProjectileSettings : ScriptableObject
    {
        public ProjectileView ProjectilePrefab => _projectilePrefab;
        public float MinDamage => _minDamage;
        public float MaxDamage => _maxDamage;
        public float ProjectileSpeed => _projectileSpeed;

        [SerializeField] private ProjectileView _projectilePrefab;
        [SerializeField] private float _minDamage;
        [SerializeField] private float _maxDamage;
        [SerializeField] private float _projectileSpeed;
    }
}