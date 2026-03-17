using UnityEngine;

namespace Game.Turret
{
    [CreateAssetMenu(menuName = "ScriptableObject/Game/TurretSettings", fileName = "TurretSettings")]
    public class TurretSettings : ScriptableObject
    {
        public TurretView TurretPrefab => _turretPrefab;
        public float FireDelay => _fireDelay;
        public float BarrelAngle => _barrelAngle;

        [SerializeField] private TurretView _turretPrefab;
        [SerializeField] private float _fireDelay;
        [Range(0f, 90f)]
        [SerializeField] private float _barrelAngle;
    }
}