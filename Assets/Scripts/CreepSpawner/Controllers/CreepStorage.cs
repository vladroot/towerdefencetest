using System.Collections.Generic;
using UnityEngine;

namespace Game.CreepSpawner
{
    public class CreepStorage : ICreepStorage
    {
        private Dictionary<GameObject, Creep> _map;

        public CreepStorage()
        {
            _map = new Dictionary<GameObject, Creep>();
        }

        public void Add(Creep creep)
        {
            _map.Add(creep.View.gameObject, creep);
        }

        public void Remove(Creep creep)
        {
            _map.Remove(creep.View.gameObject);
        }

        public bool TryGet(GameObject gameObject, out Creep creep)
        {
            return _map.TryGetValue(gameObject, out creep);
        }
    }
}