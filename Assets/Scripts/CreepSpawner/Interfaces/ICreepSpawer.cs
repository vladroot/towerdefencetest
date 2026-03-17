using UnityEngine;

namespace Game.CreepSpawner
{
    public interface ICreepSpawner
    {
        Creep Spawn(Vector3 position, Quaternion rotation);
        void Despawn(Creep creep);
    }
}