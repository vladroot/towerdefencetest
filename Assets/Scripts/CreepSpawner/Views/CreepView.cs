using UnityEngine;
using UnityEngine.AI;

namespace Game.CreepSpawner
{
    public class CreepView : MonoBehaviour
    {
        public NavMeshAgent NavAgent => _navAgent;

        [SerializeField] private NavMeshAgent _navAgent;
    }
}