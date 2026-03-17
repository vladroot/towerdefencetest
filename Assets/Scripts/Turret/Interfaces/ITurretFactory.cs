using UnityEngine;
using Zenject;

namespace Game.Turret
{
    public interface ITurretFactory : IFactory<Vector3, Quaternion, ITurret>
    {
    }
}