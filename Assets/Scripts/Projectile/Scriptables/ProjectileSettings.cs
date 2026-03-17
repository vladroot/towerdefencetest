using UnityEngine;

namespace Game.Projectiles
{
    [CreateAssetMenu(menuName = "ScriptableObject/Game/ProjectileSettings", fileName = "ProjectileSettings")]
    public class ProjectileSettings : ScriptableObject
    {
        public ProjectileView ProjectilePrefab => _projectilePrefab;
        public float Damage => _damage;
        public float ProjectileSpeed => _projectileSpeed;

        [SerializeField] private ProjectileView _projectilePrefab;
        [SerializeField] private float _damage;
        [SerializeField] private float _projectileSpeed;
    }
}