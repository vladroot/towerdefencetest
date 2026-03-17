using UnityEngine;

namespace Game.CreepSpawner
{
    public interface ICreepStorage
    {
        void Add(Creep creep);
        void Remove(Creep creep);
        bool TryGet(GameObject gameObject, out Creep creep);
    }
}